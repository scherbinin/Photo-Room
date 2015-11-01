using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using Common.Stuff;

namespace FotoRoom.ControlUpdater
{
    /// <summary>
    /// Обертка для PictureBox, которая превращает его в кнопку, с альфа каналом
    /// </summary>
    public class TransparentButton
    {
        private readonly Control _parent;
        private readonly PictureBox _pictureBox;
        private readonly Image _imgDown;
        private readonly Action _actionOnClick;
        private readonly Timer _timerToActionStart;
        private const int TimeInterval = 500;

        public TransparentButton(Control parent, PictureBox pictureBox, Image imgDefault, Image imgDown, Action actionOnClick)
        {
            //try
            //{
                _parent = parent;
                _pictureBox = pictureBox;
                _imgDown = imgDown;
                _actionOnClick = actionOnClick;

                pictureBox.MouseDown += pictureBox_MouseDown;
                parent.Disposed += parent_Disposed;

                _timerToActionStart = new Timer { Interval = TimeInterval };
                _timerToActionStart.Tick += _timerToActionStart_Tick;

                TransparentAdder.SetTransparentControl(_parent, _pictureBox, imgDefault);

            //}
            //catch (Exception ex)
            //{
            //    Logger.ExceptionSaveAndThrow(ex);
            //}
        }

        void parent_Disposed(object sender, EventArgs e)
        {
            //try
            //{
                _timerToActionStart.Enabled = false;
                _timerToActionStart.Tick -= _timerToActionStart_Tick;
                _pictureBox.MouseDown -= pictureBox_MouseDown;

                _timerToActionStart.Dispose();
            //}
            //catch (Exception ex)
            //{
            //    Logger.ExceptionSaveAndThrow(ex);
            //}
        }

        /// <summary>
        /// Событие истечение интервала времени, установленого в _timerToActionStart.Tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _timerToActionStart_Tick(object sender, EventArgs e)
        {

            //try
            //{
                _timerToActionStart.Enabled = false;

                if (_actionOnClick != null)
                    _actionOnClick.Invoke();
                //else
                //    throw new Exception("Для кнопки не назначено событие клика, что недопустимо");

            //}
            //catch (Exception ex)
            //{
            //    Logger.ExceptionSaveAndThrow(ex);
            //}
        }

        /// <summary>
        /// Событие нажатия на кнопку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            //try
            //{
                //Поменяем картинку, выждем время что бы изменения увидил юзер, после чего событие таймера выполнит действие клика
                TransparentAdder.SetTransparentControl(_parent, _pictureBox, _imgDown);

                _timerToActionStart.Enabled = true;

            //}
            //catch (Exception ex)
            //{
            //    Logger.ExceptionSaveAndThrow(ex);
            //}
        }

        /// <summary>
        /// Отписатья от объкта контрола и ждать пока коллектор соберет этот объект
        /// </summary>
        public void Dispose()
        {
            //try
            //{
                _timerToActionStart.Enabled = false;
                _timerToActionStart.Tick -= _timerToActionStart_Tick;
                _pictureBox.MouseDown -= pictureBox_MouseDown;

                _timerToActionStart.Dispose();
            //}
            //catch (Exception ex)
            //{
            //    Logger.ExceptionSaveAndThrow(ex);
            //}
        }
    }
}
