using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Common.Stuff;

namespace FotoRoom.ControlUpdater
{
    public static class TransparentAdder
    {
        /// <summary>
        /// Поставить прозрачность + картинка
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="usedControl"></param>
        /// <param name="img"></param>
        public static void SetTransparentControl(Control parent, PictureBox usedControl, Image img)
        {
            //try
            //{
                //Получить координаты контрола
                var point = new Point(usedControl.Location.X, usedControl.Location.Y);
                var width = usedControl.Width;
                var height = usedControl.Height;

                var destRect = new Rectangle(0, 0, width, height);//куда вставляем
                var srcRect = new Rectangle(point.X, point.Y, width, height);//Что вырезаем

                var buff = new Bitmap(parent.Width, parent.Height, PixelFormat.Format32bppArgb);
                var dest = new Bitmap(width, height, PixelFormat.Format32bppArgb);

                using (var gr = Graphics.FromImage(dest))
                {
                    parent.DrawToBitmap(buff, new Rectangle(0, 0, parent.Width, parent.Height)); 
                    gr.DrawImage(buff, destRect, srcRect, GraphicsUnit.Pixel);

                    if (img != null)
                        gr.DrawImage(img, 0, 0, width, height);
                }

                usedControl.Image = dest;
            //}
            //catch (Exception ex)
            //{
            //    Logger.ExceptionSaveAndThrow(ex);
            //}


        }

        /// <summary>
        /// Поставить прозрачность + картинка
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="usedControl"></param>
        /// <param name="img"></param>
        public static void SetTransparentControl(Control parent, Panel usedControl, Image img)
        {
            //try
            //{
                //Получить координаты контрола
                var point = new Point(usedControl.Left, usedControl.Top);
                var width = usedControl.Width;
                var height = usedControl.Height;

                var destRect = new Rectangle(0, 0, width, height);//куда вставляем
                var srcRect = new Rectangle(point.X, point.Y, width, height);//Что вырезаем

                var buff = new Bitmap(parent.Width, parent.Height, PixelFormat.Format32bppArgb);
                var dest = new Bitmap(width, height, PixelFormat.Format32bppArgb);

                using (var gr = Graphics.FromImage(dest))
                {
                    parent.DrawToBitmap(buff, new Rectangle(0, 0, parent.Width, parent.Height));
                    gr.DrawImage(buff, destRect, srcRect, GraphicsUnit.Pixel);

                    if (img != null)
                        gr.DrawImage(img, 0, 0, width, height);
                }

                usedControl.BackgroundImage = dest;

            //}
            //catch (Exception ex)
            //{
            //    Logger.ExceptionSaveAndThrow(ex);
            //}
        }

        /// <summary>
        /// Поставить прозрачность + картинка
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="usedControl"></param>
        /// <param name="img"></param>
        public static void SetTransparentControl(Control parent, TextBox usedControl, Image img)
        {
            //try
            //{
                //Получить координаты контрола
                var point = new Point(usedControl.Left, usedControl.Top);
                var width = usedControl.Width;
                var height = usedControl.Height;

                var destRect = new Rectangle(0, 0, width, height);//куда вставляем
                var srcRect = new Rectangle(point.X, point.Y, width, height);//Что вырезаем

                var buff = new Bitmap(parent.Width, parent.Height, PixelFormat.Format32bppArgb);
                var dest = new Bitmap(width, height, PixelFormat.Format32bppArgb);

                using (var gr = Graphics.FromImage(dest))
                {
                    parent.DrawToBitmap(buff, new Rectangle(0, 0, parent.Width, parent.Height));
                    gr.DrawImage(buff, destRect, srcRect, GraphicsUnit.Pixel);

                    if (img != null)
                        gr.DrawImage(img, 0, 0, width, height);
                }

                usedControl.BackgroundImage = dest;

            //}
            //catch (Exception ex)
            //{
            //    Logger.ExceptionSaveAndThrow(ex);
            //}
        }

        /// <summary>
        /// Поставить прозрачность + картинка
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="usedControls"></param>
        /// <param name="img"></param>
        public static void SetTransparentControl(Control parent, List<Control> usedControls, Image img)
        {
            //try
            //{

                //Получить координаты контрола
                int x = 0;
                int y = 0;

                foreach (Control usedControl in usedControls)
                {
                    x += usedControl.Left;
                    y += usedControl.Top;
                }

                var point = new Point(x, y);
                var width = usedControls.Last().Width;
                var height = usedControls.Last().Height;

                var destRect = new Rectangle(0, 0, width, height);//куда вставляем
                var srcRect = new Rectangle(point.X, point.Y, width, height);//Что вырезаем

                var buff = new Bitmap(parent.Width, parent.Height, PixelFormat.Format32bppArgb);
                var dest = new Bitmap(width, height, PixelFormat.Format32bppArgb);

                using (var gr = Graphics.FromImage(dest))
                {
                    parent.DrawToBitmap(buff, new Rectangle(0, 0, parent.Width, parent.Height));
                    gr.DrawImage(buff, destRect, srcRect, GraphicsUnit.Pixel);

                    if (img != null)
                        gr.DrawImage(img, 0, 0, width, height);
                }

                usedControls.Last().BackgroundImage = dest;

            //}
            //catch (Exception ex)
            //{
            //    Logger.ExceptionSaveAndThrow(ex);
            //}
        }

        /// <summary>
        /// Поставить прозрачность + картинка
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="usedControls"></param>
        /// <param name="img"></param>
        public static void SetTransparentControlIsEmpty(Control parent, List<Control> usedControls, Image img)
        {
            //try
            //{
                //Получить координаты контрола
                int x = 0;
                int y = 0;

                //Если PictureBox уже емеет в себе картинку - не трогаем
                if (((PictureBox)usedControls.Last()).Image != null) return;

                foreach (Control usedControl in usedControls)
                {
                    x += usedControl.Left;
                    y += usedControl.Top;
                }

                var point = new Point(x, y);
                var width = usedControls.Last().Width;
                var height = usedControls.Last().Height;

                var destRect = new Rectangle(0, 0, width, height);//куда вставляем
                var srcRect = new Rectangle(point.X, point.Y, width, height);//Что вырезаем

                var buff = new Bitmap(parent.Width, parent.Height, PixelFormat.Format32bppArgb);
                var dest = new Bitmap(width, height, PixelFormat.Format32bppArgb);

                using (var gr = Graphics.FromImage(dest))
                {
                    parent.DrawToBitmap(buff, new Rectangle(0, 0, parent.Width, parent.Height));
                    gr.DrawImage(buff, destRect, srcRect, GraphicsUnit.Pixel);

                    if (img != null)
                        gr.DrawImage(img, 0, 0, width, height);
                }

                usedControls.Last().BackgroundImage = dest;

            //}
            //catch (Exception ex)
            //{
            //    Logger.ExceptionSaveAndThrow(ex);
            //}
        }

        /// <summary>
        /// Поставить прозрачность + картинка
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="usedControl"></param>
        /// <param name="img"></param>
        public static void SetTransparentControl(Control parent, Label usedControl, Image img)
        {
            //try
            //{
                //Получить координаты контрола
                var point = new Point(usedControl.Left, usedControl.Top);
                var width = usedControl.Width;
                var height = usedControl.Height;

                var destRect = new Rectangle(0, 0, width, height);//куда вставляем
                var srcRect = new Rectangle(point.X, point.Y, width, height);//Что вырезаем

                var buff = new Bitmap(parent.Width, parent.Height, PixelFormat.Format32bppArgb);
                var dest = new Bitmap(width, height, PixelFormat.Format32bppArgb);

                using (var gr = Graphics.FromImage(dest))
                {
                    parent.DrawToBitmap(buff, new Rectangle(0, 0, parent.Width, parent.Height));
                    gr.DrawImage(buff, destRect, srcRect, GraphicsUnit.Pixel);

                    if (img != null)
                        gr.DrawImage(img, 0, 0, width, height);
                }

                usedControl.Image = dest;

            //}
            //catch (Exception ex)
            //{
            //    Logger.ExceptionSaveAndThrow(ex);
            //}
        }
    }
}
