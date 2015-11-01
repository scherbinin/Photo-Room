using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FotoRoom.ControlUpdater;

namespace FotoRoom.View
{
    public partial class StartView : UserControl
    {
        private readonly IWorkSpace _serviceProvider;
        private readonly Image _btnStartImg = Properties.Resources.btnStart;
        private readonly Image _btnStartDown = Properties.Resources.btnStartDown;
        private readonly Image _photoAparat = Properties.Resources.photoaparat_slide1;

        public StartView(IWorkSpace provider, object[] arg)
            : this()
        {
            _serviceProvider = provider;
        }

        public StartView()
        {
            InitializeComponent();
        }

        private void StartView_Load(object sender, EventArgs e)
        {
            new TransparentButton(this, pictureBox1, _btnStartImg, _btnStartDown,
                () => _serviceProvider.ChangeView<FotoTypeSelectView>());

            TransparentAdder.SetTransparentControl(this, pictureBox2, _photoAparat);
        }

    }
}
