using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Services;
using FotoRoom.Stuff;

namespace FotoRoom
{
    public partial class Adminka : Form, IWorkSpace
    {
        private SettingsService _settingsService;
        private SettingsEntity _settingsEntity;

        public Adminka()
        {
            InitializeComponent();
        }

        private void Adminka_Load(object sender, EventArgs e)
        {
            _settingsService = new SettingsService();
            _settingsService.Activate(this);

            _settingsEntity = _settingsService.GetSettings();

            //Выставить значения
            pbLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            pbLogo.Image = _settingsEntity.PhotoLogo;

            pbProgramBackground.SizeMode = PictureBoxSizeMode.StretchImage;
            pbProgramBackground.Image = _settingsEntity.ProgramBackground;

            if (this._settingsEntity.PaperType != PaperTypeEnume.Motovay)
            {
                this.rbtGlanec.Checked = true;
            }
            else
            {
                this.rbtMotovay.Checked = true;
            }

            if (_settingsEntity.PhotoBackground == null)//Если у нас не выбрана текстура, то выставить цвет
                pbPhotoColor.BackColor = _settingsEntity.BackgroundPhotoColor;
            else
            {
                pbPhotoColor.Image = _settingsEntity.PhotoBackground;
                pbPhotoColor.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            lblPageValue.Text = _settingsEntity.PagePrinted.ToString();
        }

        /// <summary>
        /// Сбросить настройки на настройки по умолчанию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetSettingsByDefault_Click(object sender, EventArgs e)
        {
            _settingsEntity = _settingsService.InitializeEntityByDefault();

            _settingsService.SaveSettings(_settingsEntity);

            //Выставить значения
            pbLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            pbLogo.Image = _settingsEntity.PhotoLogo;

            pbProgramBackground.SizeMode = PictureBoxSizeMode.StretchImage;
            pbProgramBackground.Image = _settingsEntity.ProgramBackground;

            pbPhotoColor.BackColor = _settingsEntity.BackgroundPhotoColor;
        }


        #region IWorkSpace

        public void ChangeView<T>(params object[] args) where T : UserControl
        {
            throw new NotImplementedException();
        }

        public T GetService<T>() where T : class
        {
            throw new NotImplementedException();
        }

        
        #endregion

        private void btnGetPhotoFon_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                _settingsEntity.BackgroundPhotoColor = colorDialog1.Color;

                pbPhotoColor.BackColor = _settingsEntity.BackgroundPhotoColor;
            }
        }

        private void btnGetBackgrImage_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.GIF) )|*.BMP;*.JPG;*.GIF";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _settingsEntity.ProgramBackground = new Bitmap(openFileDialog1.FileName);
                pbProgramBackground.SizeMode = PictureBoxSizeMode.StretchImage;
                pbProgramBackground.Image = _settingsEntity.ProgramBackground;
            }
        }

        private void btnGetPhotoBackground_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.GIF) )|*.BMP;*.JPG;*.GIF";
            const int minWidth = 1600;
            const int minHeight = 1600;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image selectedFile = new Bitmap(openFileDialog1.FileName);
                
                if (selectedFile.Width < minWidth || selectedFile.Height < minHeight)
                {
                    MessageBox.Show(
                        "Выбранное изображение меньше минимально допустимого размера. Минимально дпустимый размер " + minWidth + " на " + minHeight + ". Размер вышего изображения: "
                        + selectedFile.Width + " на " + selectedFile.Height, 
                        "Вы загрузили изображение меньшего размера",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                    return;
                }

                _settingsEntity.PhotoBackground = selectedFile;
                pbPhotoColor.SizeMode = PictureBoxSizeMode.StretchImage;
                pbPhotoColor.Image = _settingsEntity.PhotoBackground;
            }
        }

        private void btnGetLogo_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.GIF) )|*.BMP;*.JPG;*.GIF";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var image = new Bitmap(openFileDialog1.FileName);

                //валидация логотипа: длинна должна относиться к ширина в диапазоне 1.8 - 2.2
                var koyf = ((float)image.Width)/image.Height;

                if( 1.8 <= koyf && koyf <= 2.2)
                {
                    _settingsEntity.PhotoLogo = image;
                    pbLogo.SizeMode = PictureBoxSizeMode.StretchImage;
                    pbLogo.Image = _settingsEntity.ProgramBackground;
                }
                else
                {
                    MessageBox.Show("Размеры логотипа не соответствуют требованиям, придерживайтесь что бы длина была примерно в 2 раза больше чем ширина");
                }
            }
        }

        /// <summary>
        /// Выйти без сохранения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Сохранить насйтроки и запустить фотобутку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            _settingsService.SaveSettings(_settingsEntity);

            Hide();

            try
            {
                var form = Activator.CreateInstance<SinglePartScreen>();
                form.Closed += new EventHandler(form_Closed);
                form.Show();
            }
            catch(Exception ex)
            {
                
            }
        }

        void form_Closed(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnPageValReset_Click(object sender, EventArgs e)
        {
            lblPageValue.Text = "0";
            _settingsEntity.PagePrinted = 0;
            _settingsService.SaveSettings(_settingsEntity);
        }

        private void rbtGlanec_CheckedChanged(object sender, EventArgs e)
        {
            _settingsEntity.PaperType = PaperTypeEnume.Glynec;
        }

        private void rbtMotovay_CheckedChanged(object sender, EventArgs e)
        {
            _settingsEntity.PaperType = PaperTypeEnume.Motovay;
        }
    }
}
