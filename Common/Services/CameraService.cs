using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using CanonSDKTutorial;
using CanonSDKWorker;
using Common.Types;
using EDSDKLib;
using FotoRoom;

namespace Common.Services
{
    /// <summary>
    /// Сервис по работе с камерами
    /// </summary>
    public class CameraService : IService
    {
        private SDKHandler _cameraHandler;
        private List<Camera> _camList;

        private GetImageHanlder _viewerHandler;
        public bool IsActivated;

        public void Activate(IWorkSpace controller)
        {
            if (!IsActivated)
            {
                _cameraHandler = new SDKHandler();
                //CameraHandler.CameraAdded += new SDKHandler.CameraAddedHandler(SDK_CameraAdded);
                //CameraHandler.FrameRateUpdated += new SDKHandler.FloatUpdate(SDK_FrameRateUpdated);
                _cameraHandler.LiveViewUpdated += new SDKHandler.ImageUpdate(SDK_LiveViewUpdated);
                //CameraHandler.ProgressChanged += new SDKHandler.ProgressHandler(SDK_ProgressChanged);
                _cameraHandler.CameraHasShutdown += CameraHandler_CameraHasShutdown;

                CloseSession();

                IsActivated = OpenSession();
            }
            else
            {
                throw new Exception("Повторная активация сервиса камеры не возможна");
            }
        }

        public void Deactivate()
        {
            if (IsActivated)
            {
                StopLiveView();
                CloseSession();

                IsActivated = false;
            }
            else
            {
                throw new Exception("Диактивация сервиса не возможна, т.к. не была произведена активация");
            }
        }

        /// <summary>
        /// Добавить подписчика на трансляцию кадров с фотокамеры
        /// </summary>
        /// <param name="handler"></param>
        public void AddSubscriber(GetImageHanlder handler)
        {
            if (_cameraHandler.MainCamera != null)
            {
                if (IsActivated)
                {
                    _viewerHandler = handler;
                    StartLiveView();
                }
                else
                {
                    throw new Exception("Сервис не был активирован");
                }
            }
            else
                throw new Exception("Список камер пуст, активация кадрового потока не возможна");
        }

        /// <summary>
        /// Убрать подписчиков на получение кадров с камеры
        /// </summary>
        public void RemoveSubscriber()
        {
            _viewerHandler = null;
        }

        /// <summary>
        /// получить фото с камеры
        /// </summary>
        /// <returns></returns>
        public void TakePhoto(GetImageHanlder handler,Action actionIfException)
        {

            try
            {
                _cameraHandler.SetSetting(EDSDK.PropID_SaveTo, (uint)EDSDK.EdsSaveTo.Host);
                _cameraHandler.SetCapacity();

                _cameraHandler.ImageHandler = handler;
                _cameraHandler.TakePhoto(actionIfException);
            }
            catch (Exception)
            {
                //Если прилетает ошибка от СДК о невозможности сделать фото
                throw new FocusGetExceptions();

            }
            
        }

        /// <summary>
        /// Посмотреть, закончился ли поток съемки и выставления фотки в форму
        /// </summary>
        /// <returns></returns>
        public bool IsPhotoThreadAlive()
        {
            return _cameraHandler.IsPhotoThreadAlive();
        }

        /// <summary>
        /// Закрыть сессию
        /// </summary>
        private void CloseSession()
        {
            _cameraHandler.CloseSession();
        }

        /// <summary>
        /// открыть сессию обмена с устройством
        /// </summary>
        private bool OpenSession()
        {
            GetCamersList();

            if (_camList.Count > 0)
                _cameraHandler.OpenSession(_camList[0]);
            else
                return false;

            return true;
        }

        /// <summary>
        /// получить список всех подключенных камер
        /// </summary>
        private void GetCamersList()
        {
            _camList = _cameraHandler.GetCameraList();

            if (_camList.Count == 0)
                throw new Exception("Ошибка: камера не подключена, дальнейшая работа приложения не возможна. Подключите камера и запустите приложение заново");

        }

        /// <summary>
        /// Начать трансляцию с камеры, если еще не начата
        /// </summary>
        private void StartLiveView()
        {
            if (!_cameraHandler.IsEvfFilming)
            {
                if (!_cameraHandler.IsLiveViewOn)
                    _cameraHandler.StartLiveView();
            }
        }

        /// <summary>
        /// Закончить трансляцию с камеры, если она активирована
        /// </summary>
        private void StopLiveView()
        {
            if (!_cameraHandler.IsEvfFilming)
            {
                if (_cameraHandler.IsLiveViewOn)
                    _cameraHandler.StopLiveView();

            }
        }

        /// <summary>
        /// обработчик, стреляющий при готовности кадра из камеры
        /// </summary>
        /// <param name="img"></param>
        private void SDK_LiveViewUpdated(Image img)
        {
            if (_viewerHandler != null)
                    _viewerHandler.BeginInvoke(img, null, null);
                
        }

        /// <summary>
        /// обработчик если камера уснула
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CameraHandler_CameraHasShutdown(object sender, EventArgs e)
        {
            MessageBox.Show("Камера перешла в спящий режим, измените настройки камеры");
            throw new Exception();
        }
    }
}
