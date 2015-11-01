using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CanonSDKTutorial;
using Common.Services;
using Common.Stuff;
using Common.Types;
using FotoRoom;
using FotoRoom.Stuff;
using FotoRoom.View;

namespace test
{
    public partial class Form1 : Form, IWorkSpace
    {
        //private readonly CameraService _cameraService = new CameraService();
        private List<Image> _imageList = new List<Image>(){Properties.Resources.foto1};
        private UserControl _currentView;
        private readonly List<IService> _serviceList = new List<IService>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Наполнить коллекцию сервисами
            _serviceList.Add(new ImageEditorService());
            //_serviceList.Add(new CameraService());
            _serviceList.Add(new GuiHelperService());
            _serviceList.Add(new PrintService());
            _serviceList.Add(new SettingsService());
            _serviceList.Add(new NetworkService());
            _serviceList.Add(new PhotoSaverService());

            //Активировать сервисы
            _serviceList.ForEach(service => service.Activate(this));

            var img = new Bitmap(300, 400);

            GetService<PrintService>().Print(img,GetService<SettingsService>().GetSettings().SettingsForPrintSingleFoto, 1);
            //ChangeView<MailSendView>(_imageList, PhotoShotMode.SingleFoto);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangeView<MailSendView>(_imageList, PhotoShotMode.SingleFoto);
        }
        public void ChangeView<T>(params object[] args) where T : UserControl
        {
            if (_currentView != null)
                _currentView.Dispose();

            var settings = GetService<SettingsService>().GetSettings();

            var view = Activator.CreateInstance(typeof(T), this, args) as T;
            view.Location = new Point(0,0);
            //view.Dock = DockStyle.Fill;

            //Назначим на форму фон: создадим PictureBox, назначим ему масштабирование и фон, добавим в список контролов
            var pb = new PictureBox();
            pb.Location = new Point(0,0);
            pb.Image = settings.ProgramBackground;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.Dock = DockStyle.Fill;
            pb.Name = "pbFormBacground";
            view.Controls.Add(pb);

            Controls.Add(view);

            _currentView = view;

        }

        public T GetService<T>() where T : class
        {
            var res = _serviceList.Find(ent => (ent is T));

            if (res != null)
                return res as T;

            return null;
        }
    }
}
