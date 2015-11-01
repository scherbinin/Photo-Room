using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FotoRoom.ControlUpdater;
using FotoRoom.Stuff;

namespace FotoRoom.View
{
    public partial class FotoTypeSelectView : UserControl
    {
        private readonly IWorkSpace _serviceProvider;
        private int _fotoNumber;
        private PhotoShotMode _photoMode;
        private readonly Image _1BigPhoto = Properties.Resources.Big1Photo;//Одна большая фотка
        private readonly Image _1Photo4 = Properties.Resources.Photo1Parts4;//Большая фотка из 4-х фоток
        private readonly Image _photo1WithBigPhoto = Properties.Resources.Photo1WithBigPhoto;//Большая фотка с одной большой фоткой и 3-мя мелкими
        private readonly Image _strips3Photo = Properties.Resources.Strips3Photo;//2 полоски по 3 фотки на каждой
        private readonly Image _strips4Photo = Properties.Resources.Strips4Photo;//2 полоски по 3 фотки на каждой
        private readonly Image _choosingFormatText = Properties.Resources.ChoosingFormat;

        public FotoTypeSelectView(IWorkSpace provider, object[] arg)
            : this()
        {
            _serviceProvider = provider;
        }

        public FotoTypeSelectView()
        {
            InitializeComponent();
        }

        private void ChangeScreen()
        {
            _serviceProvider.ChangeView<FotoMakeView>(_fotoNumber, _photoMode);
        }

        #region Обработчики Чекбоксов

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            _photoMode = PhotoShotMode.SingleFoto;
            _fotoNumber = 1;

            ChangeScreen();
        }


        private void rbFoto1_CheckedChanged(object sender, EventArgs e)
        {
            _photoMode = PhotoShotMode.Strips4Foto;
            _fotoNumber = 8;

            ChangeScreen();
        }

        private void rbFoto2_CheckedChanged(object sender, EventArgs e)
        {
            _photoMode = PhotoShotMode.Single4Foto1Big;
            _fotoNumber = 4;

            ChangeScreen();
        }

        private void rbFoto4_CheckedChanged(object sender, EventArgs e)
        {
            _photoMode = PhotoShotMode.Strips3Foto;
            _fotoNumber = 6;

            ChangeScreen();
        }
        
        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            _photoMode = PhotoShotMode.Single4Foto;
            _fotoNumber = 4;

            ChangeScreen();
        }

        #endregion

        private void FotoTypeSelectView_Load(object sender, EventArgs e)
        {
            TransparentAdder.SetTransparentControl(this, pb1BigPhoto, _1BigPhoto);//Одно большое фото
            TransparentAdder.SetTransparentControl(this, pb1Photo4, _1Photo4);//Одна большая фотка из 4-х фоток
            TransparentAdder.SetTransparentControl(this, pb1PhotoBigPhoto, _photo1WithBigPhoto); //Одна большая фотка с одной большой фоткой и 3-мя мелкими
            TransparentAdder.SetTransparentControl(this, pb2Strips3Photo, _strips3Photo);//2 полоски по 3 фотки на каждой
            TransparentAdder.SetTransparentControl(this, pb2Strips4Photo, _strips4Photo);//2 полоски по 4 фотки на каждой

            TransparentAdder.SetTransparentControl(this, pbTitleText, _choosingFormatText);//2 полоски по 4 фотки на каждой
        }

    }
}
