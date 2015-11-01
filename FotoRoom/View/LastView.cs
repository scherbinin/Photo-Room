using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Services;
using Common.Stuff;
using FotoRoom.ControlUpdater;
using FotoRoom.Stuff;

namespace FotoRoom.View
{
    public partial class LastView : UserControl
    {
        private readonly IWorkSpace _serviceProvider = null;
        private readonly Image _pbTextUp = Properties.Resources.pbTextUp;
        private readonly Image _pbTextDown = Properties.Resources.pbTextDown;
        private readonly Image _pbMustache = Properties.Resources.pbMustache;

        private int _index = 0;

        public LastView(IWorkSpace provider, object[] arg)
            : this()
        {
            _serviceProvider = provider;
        }

        public LastView()
        {
            InitializeComponent();
        }

        private void GoToStartPage()
        {
            _serviceProvider.ChangeView<StartView>();
        }

        private void LastView_MouseClick(object sender, MouseEventArgs e)
        {
            GoToStartPage();
        }

        private void LastView_Load(object sender, EventArgs e)
        {
            TransparentAdder.SetTransparentControl(this,pbTextUp,_pbTextUp);
            TransparentAdder.SetTransparentControl(this, pbTextDown, _pbTextDown);
            TransparentAdder.SetTransparentControl(this, pbMustache, _pbMustache);

            timer1.Interval = 1000;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _index++;

            if(_index > 10)
            {
                timer1.Enabled = false;
                _serviceProvider.ChangeView<StartView>();
            }
        }
    }
}
