using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CanonSDKTutorial;
using CanonSDKWorker;
using Common.Services;
using Common.Stuff;
using Common.Types;
using FotoRoom.ControlUpdater;
using FotoRoom.Stuff;

namespace FotoRoom.View
{
    public partial class FotoMakeView : UserControl
    {
        private object _lock = new object();

        private readonly Image _btnRowUp = Properties.Resources.btnRowUp;
        private readonly Image _btnRowDown = Properties.Resources.btnRowDown;
        private readonly Image _btnStartNotActive = Properties.Resources.btnStartNotActive;
        private readonly Image _btnStartActive = Properties.Resources.btnStart;
        private readonly Image _btnStartDown = Properties.Resources.btnStartDown;
        private readonly Image _btnNextActive = Properties.Resources.btnNextActive;
        private readonly Image _btnNextDown = Properties.Resources.btnNextDown;
        private readonly Image _imgOne = Properties.Resources.imgOne;
        private readonly Image _imgTwo = Properties.Resources.imgTwo;
        private readonly Image _imgThree = Properties.Resources.imgThree;
        private readonly Image _imgChoose = Properties.Resources.imgChoose;
        private readonly Image _imgTextBottomSlide3 = Properties.Resources.imgTextBottomSlide3;
        private readonly Image _imgTextTitleSlide3 = Properties.Resources.imgTextTitleSlide3;

        private TransparentButton _transpBtnStart = null;

        private readonly int _fotoVulae;
        private int _timer = 0;
        private readonly IWorkSpace _serviceProvider;
        private readonly CameraService _cameraService;
        //список соответствий, просто пронумерованы pictureBox'ы
        private readonly Dictionary<PhotoCadrNumber, PictureBoxCoverage> _imagesControlList = new Dictionary<PhotoCadrNumber, PictureBoxCoverage>();
        private int _currentFotoIndex = 0;//Номер следующего pictureBox для вывода фоток
        private bool _isFinshedFoting = false;//Если все отфоткано и все нравится, то выставим этот флаг, которой разрешает уйти с формы
        private readonly PhotoShotMode _photoMode;

        public FotoMakeView(IWorkSpace provider, object[] arg)
            : this()
        {
            _serviceProvider = provider;

            if (arg.Length > 0)
            {
                _fotoVulae = (int)arg[0];
                _photoMode = (PhotoShotMode)arg[1];
            }
            else
                throw new Exception("При вызове представления FotoMakeView  в конструторе ожидается в коллекции значений целое число.");

            //получить и активировать сервис
            _cameraService = _serviceProvider.GetService<CameraService>();
        }

        public FotoMakeView()
        {
            InitializeComponent();


        }

        private void FotoMakeView_Load(object sender, EventArgs e)
        {
            pbOne.Visible = false;
            pbTwo.Visible = false;
            pbThree.Visible = false;

            //Доавить подписчика на источник фотографий (фотки отправлять в эту форму, делегат SetLiveViewCadr)
            if (_cameraService.IsActivated)
                _cameraService.AddSubscriber(SetLiveViewCadr);

            _imagesControlList.Add(PhotoCadrNumber.FistCadr, new PictureBoxCoverage(pbGotFoto1, _serviceProvider));
            _imagesControlList.Add(PhotoCadrNumber.SecondCadr, new PictureBoxCoverage(pbGotFoto2, _serviceProvider));
            _imagesControlList.Add(PhotoCadrNumber.ThirdCadr, new PictureBoxCoverage(pbGotFoto3, _serviceProvider));
            _imagesControlList.Add(PhotoCadrNumber.FourthCadr, new PictureBoxCoverage(pbGotFoto4, _serviceProvider));
            _imagesControlList.Add(PhotoCadrNumber.FifthCadr, new PictureBoxCoverage(pbGotFoto5, _serviceProvider));
            _imagesControlList.Add(PhotoCadrNumber.SixthCadr, new PictureBoxCoverage(pbGotFoto6, _serviceProvider));
            _imagesControlList.Add(PhotoCadrNumber.SeventhCadr, new PictureBoxCoverage(pbGotFoto7, _serviceProvider));
            _imagesControlList.Add(PhotoCadrNumber.EighthCadr, new PictureBoxCoverage(pbGotFoto8, _serviceProvider));

            //Скрыть текст сверху и снизу, он должен появиться только после окончания фотографирования
            pbTextBottom.Hide();
            pbTextTitle.Hide();

            //Сделать кнопку Далее
            _transpBtnStart = new TransparentButton(this, pbStartBtn, _btnStartActive, _btnStartDown, ClickOnMainButton);

            //Приляпаем картинки на стрелки вверх/вниз
            TransparentAdder.SetTransparentControl(this, pbUp, _btnRowUp);
            TransparentAdder.SetTransparentControl(this, pbDown, _btnRowDown);

            // Сделать прозрачными PictureBox с фотками
            SetTransparentToPictBox();

            //Сделать прозрачными PictureBox с цифрами
            TransparentAdder.SetTransparentControl(this, pbOne, _imgOne);
            TransparentAdder.SetTransparentControl(this, pbTwo, _imgTwo);
            TransparentAdder.SetTransparentControl(this, pbThree, _imgThree);

            //Сделать прозрачным Панель с фотками
            TransparentAdder.SetTransparentControl(this, panel1, null);

        }

        /// <summary>
        /// Сделать прозрачными PictureBox с фотками
        /// </summary>
        private void SetTransparentToPictBox()
        {
            //Сделать прозрачными PictureBox с фотками
            var list = new List<Control>() { panel1, pbGotFoto1 };
            TransparentAdder.SetTransparentControlIsEmpty(this, list, null);
            list = new List<Control>() { panel1, pbGotFoto2 };
            TransparentAdder.SetTransparentControlIsEmpty(this, list, null);
            list = new List<Control>() { panel1, pbGotFoto3 };
            TransparentAdder.SetTransparentControlIsEmpty(this, list, null);
            list = new List<Control>() { panel1, pbGotFoto4 };
            TransparentAdder.SetTransparentControlIsEmpty(this, list, null);
            list = new List<Control>() { panel1, pbGotFoto5 };
            TransparentAdder.SetTransparentControlIsEmpty(this, list, null);
            list = new List<Control>() { panel1, pbGotFoto6 };
            TransparentAdder.SetTransparentControlIsEmpty(this, list, null);
            list = new List<Control>() { panel1, pbGotFoto7 };
            TransparentAdder.SetTransparentControlIsEmpty(this, list, null);
            list = new List<Control>() { panel1, pbGotFoto8 };
            TransparentAdder.SetTransparentControlIsEmpty(this, list, null);
        }

        /// <summary>
        /// Деактивировать элементы интерфейса, когда начинаем фотографирование
        /// </summary>
        private void UiElementDiactivateThenPhotingStart()
        {
            pbStartBtn.Enabled = false;

            if (_transpBtnStart != null) _transpBtnStart.Dispose();
            _transpBtnStart = new TransparentButton(this, pbStartBtn, _btnStartNotActive, null, ClickOnMainButton);

            pbUp.Enabled = false;
            pbDown.Enabled = false;
        }

        /// <summary>
        /// Активировать элементы интерфейса, когда закончили фоткать
        /// </summary>
        private void UiElementsActivatesThenPhotingFinished()
        {
            pbStartBtn.Enabled = true;

            if (_transpBtnStart != null) _transpBtnStart.Dispose();
            _transpBtnStart = new TransparentButton(this, pbStartBtn, _btnNextActive, _btnNextDown, ClickOnMainButton);

            pbUp.Enabled = true;
            pbDown.Enabled = true;

            //Включить 2 текса сверху и снизу
            pbTextBottom.Show();
            pbTextTitle.Show();
            TransparentAdder.SetTransparentControl(this, pbTextBottom, _imgTextBottomSlide3);
            TransparentAdder.SetTransparentControl(this, pbTextTitle, _imgTextTitleSlide3);
        }

        #region Вывод фоток из другого потока

        /// <summary>
        /// Для синхронизации GUI и потока съемки: Вывод liveView
        /// </summary>
        /// <param name="img"></param>
        public void SetLiveViewCadr(Image img)
        {
            if (pbLiveView.InvokeRequired)
            {
                try
                {
                    var d = new GetImageHanlder(SetLiveViewCadr);
                    this.Invoke(d, new object[] { img });
                }
                catch (Exception)
                {

                }

            }
            else
            {
                pbLiveView.Image = img;
            }

        }

        /// <summary>
        /// Для синхронизации GUI и потока съемки: Вывод каждого снятого кадра в свое место
        /// </summary>
        /// <param name="img"></param>
        public void SetCadr(Image img)
        {
            //Получить контрол
            var index = GetNextFotoIndex();
            var control = _imagesControlList[index];

            if (control.InvokeRequired)
            {
                var d = new GetImageHanlder(SetCadr);
                this.Invoke(d, new object[] { img });
            }
            else
            {
                control.Image = _serviceProvider.GetService<ImageEditorService>().ScaleImage(img, 1920, 1280);

                Debug.WriteLine("wefwef" + Thread.CurrentThread.Name);

                //Если стояло выделение - убрать его
                if (control.IsNeedReFoting)
                    control.ChooseForRefoting(_imgChoose);

                ButtonValidate();
            }

        }

        #endregion

        #region Логика которая фоткает

        /// <summary>
        /// Обработчик таймера, реализация счетчика перед фотографированием для всего цикла фотографирования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Выполнять ли таймер (лучше управлять флагом разрешения на эту логику, чем мучаться управлять самим таймером)
            if(!_timerActionEnabled)
                return;

            if (_cameraService.IsPhotoThreadAlive())//Ждем пока завершится потом съемки + выставления картинки. Таймеру дадут работать, как только отфоткает, но не выставит картинку
                return;

            lock (_lock)
            {
                if (_foucesSDKError) //Если ошибка из сдк при наведении фокуса
                {

                    _foucesSDKError = false;
                    
                    _timerActionEnabled = false;
                    new MessageFocusError().ShowDialog();
                    _timerActionEnabled = true;
                }
            }
            switch (_timer)
            {
                case 1:
                    {
                        pbOne.Visible = true;
                        pbTwo.Visible = false;
                        pbThree.Visible = false;
                    }
                    break;
                case 2:
                    {
                        pbOne.Visible = false;
                        pbTwo.Visible = true;
                        pbThree.Visible = false;
                    }
                    break;
                case 3:
                    {
                        pbOne.Visible = false;
                        pbTwo.Visible = false;
                        pbThree.Visible = true;
                    }
                    break;
                case 4:
                    {
                        pbOne.Visible = false;
                        pbTwo.Visible = false;
                        pbThree.Visible = true;
                    }
                    break;
                case 5:
                    {
                        pbOne.Visible = false;
                        pbTwo.Visible = false;
                        pbThree.Visible = false;

                        _timer = 0;

                        MakeNextFoto();

                        //Если фотоцикл завершен
                        if (_currentFotoIndex >= _fotoVulae - 1) 
                            _timerActionEnabled = false;
                            //timer1.Stop();

                        return;
                    }
            }

            _timer++;
        }

        private bool _foucesSDKError = false;//Ошибка ловли фокуса
        private bool _timerActionEnabled = false;//Флаг выполняется ли экшн цикла таймера

        /// <summary>
        /// Запросить фотку из сервиса
        /// </summary>
        private void MakeNextFoto()
        {

            Action _actionFocusLost = () =>
                                          {
                                              lock (_lock)
                                              {
                                                  _foucesSDKError = true;
                                                  _timerActionEnabled = true;
                                              }
                                          };

            try
            {
                _cameraService.TakePhoto(SetCadr, _actionFocusLost);
            }
            catch (Exception)
            {

            }

        }

        #endregion

        /// <summary>
        /// Кнопка Фоткать/ Кнопка перехода дальше
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickOnMainButton()
        {
            if (!_isFinshedFoting)
            {//Если мы еще не закончили фоткать - фоткаем
                _timerActionEnabled = true;
                if(timer1.Enabled == false )timer1.Enabled = true;

                UiElementDiactivateThenPhotingStart();
            }
            else
            {//уйти с этой формы, т.к. фоткание закончено
                _cameraService.RemoveSubscriber();

                var imageList = (_imagesControlList.Values.Select(imageControl => imageControl.Image).ToList()).GetRange(0, _fotoVulae);

                //Скинем в контейнер сервиса отфотканные фотки, что бы потом отправить их на сохранение
                foreach (var image in imageList)
                    _serviceProvider.GetService<PhotoSaverService>().PushToPhotoContainer(image);

                //Получить развертку и отдать далее
                var totalImage = GetDisplayImage(imageList);


                _serviceProvider.ChangeView<MailSendView>(totalImage, _photoMode);
            }
        }

        /// <summary>
        /// Отправить фотки на создание развертки в рамках контекста
        /// </summary>
        /// <param name="imageList"></param>
        /// <returns></returns>
        private Image GetDisplayImage(List<Image> imageList)
        {
            var imageService = _serviceProvider.GetService<ImageEditorService>();
            var settingsEntity = _serviceProvider.GetService<SettingsService>().GetSettings();

            var photosDescriber = new PhotoContainer()
            {
                PhotoMode = _photoMode,
                Photos = imageList,
                PhotoWidthForScale = 480,
                PhotoHeightForScale = 320,
                LogoImage = settingsEntity.PhotoLogo,
                BackColor = settingsEntity.BackgroundPhotoColor,
                Background = settingsEntity.PhotoBackground
            };

            return imageService.GetImageForPrint(photosDescriber);
        }

        /// <summary>
        /// Валидация для кнопки интерфейса
        /// </summary>
        private void ButtonValidate()
        {
            //Если все контролы не выделены пользователем и все отфотканы
            if (_imagesControlList.Values.FirstOrDefault(pb => (pb.IsNeedReFoting)) == null && _fotoVulae == _currentFotoIndex)
            {
                //Все отфоткано, можно валить отсюда
                _isFinshedFoting = true;

                UiElementsActivatesThenPhotingFinished();
            }
            else
            {
                //Либо еще не все фотки сделаны, либо пользователь пометил что то на перефотографирование 
                _isFinshedFoting = false;

                if (_imagesControlList.Values.FirstOrDefault(pb => (pb.IsNeedReFoting)) != null)
                {
                    //Пользователь пометил фотку на перефотографирование
                    pbStartBtn.Enabled = true;

                    if (_transpBtnStart != null) _transpBtnStart.Dispose();
                    _transpBtnStart = new TransparentButton(this, pbStartBtn, _btnStartActive, _btnStartDown, ClickOnMainButton);
                }
                else
                {
                    if (pbStartBtn.Enabled)//Что бы попали сюда только раз 
                        UiElementDiactivateThenPhotingStart();
                }

            }
        }
        /// <summary>
        /// Понять в какой контрол выводить фотку
        /// </summary>
        /// <returns></returns>
        private PhotoCadrNumber GetNextFotoIndex()
        {
            //Если есть какие то контролы которые не нравятся, то вернуть индекс первого
            var obj = (int)_imagesControlList.FirstOrDefault(pb => (pb.Value.IsNeedReFoting)).Key;

            if (obj > 0)
                return (PhotoCadrNumber)obj;

            //Если таких контролов нет, то сделать следующее фото
            if (_currentFotoIndex <= _fotoVulae)
                _currentFotoIndex++;

            return (PhotoCadrNumber)_currentFotoIndex;
        }

        /// <summary>
        /// Клик по одному из 4-х сделанных фото
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbGotFotoAll_Click(object sender, EventArgs e)
        {
            //Если мы фотали, то разрешаем выделение
            //найти нужный pictureBox и сказать его обертке что он выбран для перефотографирования
            var keyValuePair = _imagesControlList.FirstOrDefault(obj => (obj.Value.PictureBox == sender));

            if (keyValuePair.Value.Image == null)
                return;

            if (_fotoVulae >= (int)keyValuePair.Key)
                keyValuePair.Value.ChooseForRefoting(_imgChoose);

            ButtonValidate();
        }

        /// <summary>
        /// Клик на стрелку вверх
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbUp_Click(object sender, EventArgs e)
        {
            //Найдем контрол, который самый верхний
            var firstControl = panel1.Controls[0];

            foreach (Control control in panel1.Controls.Cast<Control>().Where(control => control.Location.Y < firstControl.Location.Y))
                firstControl = control;

            //Посмотрим, появился ли он полностью в области видимости
            if (firstControl.Location.Y > 10)
                return;

            //Если контрол все еще не появился, то можем мотать дальше
            foreach (Control control in panel1.Controls)
            {
                var location = new Point(control.Location.X, control.Location.Y + 165);

                control.Location = location;
            }

            SetTransparentToPictBox();
        }

        /// <summary>
        /// Клик на стрелку вниз
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbDown_Click(object sender, EventArgs e)
        {
            //Найдем контрол, который самый нижний
            var lastControl = panel1.Controls[0];

            foreach (Control control in panel1.Controls.Cast<Control>().Where(control => control.Location.Y > lastControl.Location.Y))
                lastControl = control;

            //Посмотрим, появился ли он полностью в области видимости
            if (lastControl.Location.Y + lastControl.Height + 10 < panel1.Height)
                return;

            //Если контрол все еще не появился, то можем мотать дальше
            foreach (Control control in panel1.Controls)
            {
                var location = new Point(control.Location.X, control.Location.Y - 165);

                control.Location = location;
            }

            SetTransparentToPictBox();

        }
    }
}
