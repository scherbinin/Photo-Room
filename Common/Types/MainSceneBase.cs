using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using CanonSDKTutorial;
using Common.Services;
using Common.Types;

namespace FotoRoom
{
    public abstract partial class MainSceneBase : Form, IWorkSpace
    {
        private UserControl _currentView;
        private readonly List<IService> _serviceList;

        protected MainSceneBase()
        {
            InitializeComponent();

            _serviceList = new List<IService>();

            ServiceInit();
        }

        private void ServiceInit()
        {
            try
            {
                //Наполнить коллекцию сервисами
                _serviceList.Add(new ImageEditorService());
                _serviceList.Add(new CameraService());
                _serviceList.Add(new GuiHelperService());
                _serviceList.Add(new PrintService());
                _serviceList.Add(new SettingsService());
                _serviceList.Add(new NetworkService());
                _serviceList.Add(new PhotoSaverService());

                //Активировать сервисы
                _serviceList.ForEach(service => service.Activate(this));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Исключение при активации сервисов: " + ex.Message);

                Close();
            }

        }

        public void ChangeView<T>(params object[] args) where T : UserControl
        {
            if (_currentView != null)
                _currentView.Dispose();

            var settings = GetService<SettingsService>().GetSettings();
            
            var view = Activator.CreateInstance(typeof(T), this, args) as T;

            //view.Dock = DockStyle.Fill;
            view.Location = new Point(0, 0);

            //Назначим на форму фон: создадим PictureBox, назначим ему масштабирование и фон, добавим в список контролов
            var pb = new PictureBox();
            pb.Image = settings.ProgramBackground;
            pb.Dock = DockStyle.Fill;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
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

        public virtual void StartScene_Load(object sender, EventArgs e)
        {
            throw new Exception("Метод требуется перегрузить для задания стартового view");
        }
    }
}
