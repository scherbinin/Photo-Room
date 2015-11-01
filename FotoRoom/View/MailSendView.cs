using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Common.Services;
using FotoRoom.ControlUpdater;
using FotoRoom.Stuff;

namespace FotoRoom.View
{
    public partial class MailSendView : UserControl
    {
        private readonly Image _btnNextActive = Properties.Resources.btnNextMailSlideActive;
        private readonly Image _btnNextDown = Properties.Resources.btnNextMailSlideDown;
        private readonly Image _btnSendActive = Properties.Resources.btnSendActive;
        private readonly Image _btnSendDown = Properties.Resources.btnSendDown;
        private readonly Image _btnSendNotActive = Properties.Resources.btnSendNotActive;
        private readonly Image _imgMailBackground = Properties.Resources.imgMailBackground;
        private readonly Image _textSendToEmail = Properties.Resources.textSendToEmail;

        private TransparentButton _transBtnSend = null;

        private readonly IWorkSpace _serviceProvider;
        private readonly NetworkService _networkService;
        private readonly Image _image = null;
        private readonly PhotoShotMode _photoMode;

        public MailSendView(IWorkSpace provider, object[] arg)
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
                throw new Exception("Отсутствует список изображений в списке параметров при активации PrintView.");
            }

            _networkService = _serviceProvider.GetService<NetworkService>();
        }

        public MailSendView()
        {
            InitializeComponent();

        }

        private void MailSendView_Load(object sender, EventArgs e)
        {
            //Проинициализировать кнопки
            _transBtnSend = new TransparentButton(this, pbSend, _btnSendActive, _btnSendDown, SendEmail);

            new TransparentButton(this, pbNext, _btnNextActive, _btnNextDown, ClickNextButton);

            TransparentAdder.SetTransparentControl(this, pbFone, _imgMailBackground);//Фон под текстбоксом ввода емейла
            TransparentAdder.SetTransparentControl(this, pbNote, _textSendToEmail);//Текст над полем ввода эмейла
        }

        private void SendEmail()
        {
            string mailAdress = txbMail.Text;

            if (IsMailValid(mailAdress))
            {
                pbSend.Enabled = false;
                TransparentAdder.SetTransparentControl(this, pbSend, _btnSendNotActive);//Фон под текстбоксом ввода емейла

                _networkService.EmailSend(_image,mailAdress);
                MessageBox.Show("Ваше письмо на адресс: " + mailAdress + " отправлено", "Письмо отправлено");
            }
            else
            {
                TransparentAdder.SetTransparentControl(this, pbSend, _btnSendActive);//Фон под текстбоксом ввода емейла
                MessageBox.Show("Неправильно введен адрес электроной почты", "Ошибка, письмо не может быть послано!");
            }

        }

        /// <summary>
        /// Валидация через регулярные выражения адреса мыла
        /// </summary>
        /// <param name="mailAdress"></param>
        /// <returns></returns>
        private bool IsMailValid(string mailAdress)
        {
            var regex = new Regex(@"^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$");

            return regex.IsMatch(mailAdress);
        }

        /// <summary>
        /// Кнопка Далее
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickNextButton()
        {
            //Скинем последнее фото-эскиз и отправим контейнер на сохранение на диск и потом чистку 
            _serviceProvider.GetService<PhotoSaverService>().PushToPhotoContainer((Image)_image.Clone());
            _serviceProvider.GetService<PhotoSaverService>().SaveAndClearContainer();

            _serviceProvider.ChangeView<PrintValueSetView>(_image, _photoMode);
        }

        /// <summary>
        /// Обработчик нажатия на кнопку в клаве
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Keyboard_Click(object sender, EventArgs e)
        {
            string current = txbMail.Text;

            txbMail.Text = current + (((Button) sender).Text).ToLower();
        }

        /// <summary>
        /// Клавиша бэкспейс
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackspace_Click(object sender, EventArgs e)
        {
            string current = txbMail.Text;
            
            if(string.IsNullOrEmpty(current))
                return;

            txbMail.Text = current.Substring(0, current.Length - 1);
        }
    }
}
