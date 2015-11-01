using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Services;
using FotoRoom.ControlUpdater;
using FotoRoom.Stuff;

namespace FotoRoom.View
{
    public partial class PrintValueSetView : UserControl
    {
        private readonly Image _imgBtnUp = Properties.Resources.imgBtnRight;
        private readonly Image _imgBtnDown = Properties.Resources.imgBtnLeft;
        private readonly Image _textImage = Properties.Resources.imgTextValue;
        private readonly Image _btnPrintActive = Properties.Resources.imgBtnPrintActive;
        private readonly Image _btnPrintDown = Properties.Resources.imgBtnPrintDown;

        private readonly IWorkSpace _serviceProvider = null;
        private readonly Image _image = null;
        private readonly PhotoShotMode _photoMode;
        private int _copyValue = 1;

        public PrintValueSetView(IWorkSpace provider, object[] arg)
            : this()
        {
            _serviceProvider = provider;

            if (arg.Length != 0)
            {
                _image = arg[0] as Image;
                _photoMode = (PhotoShotMode)arg[1];
            }
            else
            {
                throw new Exception("Отсутствует список необходимых параметров.");
            }
        }

        public PrintValueSetView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Увеличить счетчик
        /// </summary>
        private void ValueUp()
        {
            _copyValue++;
            lblValue.Text = _copyValue.ToString();

            if (_copyValue > 3)
                pbBtnUp.Enabled = false;

            pbBtnDown.Enabled = true;
        }

        /// <summary>
        /// Уменьшить счетчик
        /// </summary>
        private void ValueDown()
        {
            _copyValue--;
            lblValue.Text = _copyValue.ToString();

            if (_copyValue < 2)
                pbBtnDown.Enabled = false;

            pbBtnUp.Enabled = false;
        }

        private void DoPrint()
        {
            _serviceProvider.GetService<PrintService>().Print(_image, GetPrinterSettingsByPhotoType(_photoMode), _copyValue);

            var settingsEntity = _serviceProvider.GetService<SettingsService>().GetSettings();
            settingsEntity.PagePrinted += _copyValue;
            _serviceProvider.GetService<SettingsService>().SaveSettings(settingsEntity);

            //Вернуться в начало
            _serviceProvider.ChangeView<LastView>();
        }



        /// <summary>
        /// Получить настройки принтера по типу фотки
        /// </summary>
        /// <param name="photoMode"></param>
        /// <returns></returns>
        private byte[] GetPrinterSettingsByPhotoType(PhotoShotMode photoMode)
        {
            byte[] numArray;
            SettingsEntity settings = this._serviceProvider.GetService<SettingsService>().GetSettings();
            if (this._photoMode == PhotoShotMode.Strips4Foto || this._photoMode == PhotoShotMode.Strips3Foto)
            {
                if (settings.PaperType == PaperTypeEnume.Glynec)
                    numArray = settings.SettingsForPrintGlanectStripsFoto;
                else
                    numArray = settings.SettingsForPrintMatovoeStripsFoto;
            }
            else
            {
                if (this._photoMode != PhotoShotMode.Single4Foto && this._photoMode != PhotoShotMode.Single4Foto1Big && this._photoMode != PhotoShotMode.SingleFoto)
                {
                    throw new Exception("Неизвестный режим печати, при задании настроек печати принтеру");
                }
                numArray = (settings.PaperType != PaperTypeEnume.Glynec ? settings.SettingsForPrintMatovoeSingleFoto : settings.SettingsForPrintGlanecSingleFoto);
            }
            return numArray;
        }

        private void PrintValueSetView_Load(object sender, EventArgs e)
        {
            TransparentAdder.SetTransparentControl(this, lblValue, null);
            TransparentAdder.SetTransparentControl(this, pbText, _textImage);

            new TransparentButton(this, pbBtnUp, _imgBtnUp, _imgBtnUp, ValueUp);
            new TransparentButton(this, pbBtnDown, _imgBtnDown, _imgBtnDown, ValueDown);
            new TransparentButton(this, pbBtnPrint, _btnPrintActive, _btnPrintDown, DoPrint);
        }
    }
}
