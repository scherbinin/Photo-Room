using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using Common.Stuff;
using Common.Types;
using FotoRoom;
using FotoRoom.Stuff;

namespace Common.Services
{
    /// <summary>
    /// Класс по работе с изображениями
    /// </summary>
    public class ImageEditorService : IService
    {
        //private const int ImageValueOnX = 4; //Число фоток в высоту (по оси Y)


        public Image GetImageForPrint(PhotoContainer container)
        {
            switch (container.PhotoMode)
            {
                case PhotoShotMode.Strips4Foto:
                    return GetDisplayImageForStrips4Foto(container);
                case PhotoShotMode.Strips3Foto:
                    return GetDisplayImageForStrips3Foto(container);
                case PhotoShotMode.Single4Foto1Big:
                    return GetDisplayImageSingleWithOneBigPhoto(container);
                case PhotoShotMode.Single4Foto:
                    return GetDisplayImageSinglePhoto(container);
                case PhotoShotMode.SingleFoto:
                    return GetDisplayImageSingleBigPhoto(container);
                default:
                    throw new Exception("Новый неизвестный тип стиля фотографии, функция-адресат для выполнения стиля фото не определен");
            }
        }

        private Image CutOffImage(Image source, int newWidth, int newHeight)
        {
            Image dest = new Bitmap(newWidth - 1, newHeight - 1, PixelFormat.Format32bppPArgb);

            int offsetLeftRight = (source.Width - newWidth) / 2;
            int offsetUpDown = (source.Height - newHeight) / 2;

            using (Graphics gr = Graphics.FromImage(dest))
            {
                gr.DrawImage(source, new Rectangle(0, 0, newWidth, newHeight), offsetLeftRight, offsetUpDown, newWidth, newHeight, GraphicsUnit.Pixel);
            }

            return dest;
        }

        /// <summary>
        /// Масштабирование изображения с сохранением пропорций
        /// </summary>
        /// <param name="source"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public Image ScaleImage(Image source, int width, int height)
        {

            Image dest = new Bitmap(width - 1, height - 1, PixelFormat.Format32bppPArgb);
            using (Graphics gr = Graphics.FromImage(dest))
            {
                gr.FillRectangle(Brushes.White, 0, 0, width, height); // Очищаем экран
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                float srcwidth = source.Width;
                float srcheight = source.Height;
                float dstwidth = width;
                float dstheight = height;

                if (srcwidth <= dstwidth && srcheight <= dstheight) // Исходное изображение меньше целевого
                {
                    //int left = (width - source.Width) / 2;
                    //int top = (height - source.Height) / 2;
                    gr.DrawImage(source, 0, 0, dstwidth, dstheight);
                }
                else if (srcwidth / srcheight > dstwidth / dstheight) // Пропорции исходного изображения более широкие
                {
                    float cy = srcheight / srcwidth * dstwidth;
                    float top = ((float)dstheight - cy) / 2.0f;
                    if (top < 1.0f) top = 0;
                    gr.DrawImage(source, 0, top, dstwidth, cy);
                }
                else // Пропорции исходного изображения более узкие
                {
                    float cx = srcwidth / srcheight * dstheight;
                    float left = ((float)dstwidth - cx) / 2.0f;
                    if (left < 1.0f) left = 0;
                    gr.DrawImage(source, left, 0, cx, dstheight);
                }

                return dest;
            }
        }

        public void Activate(IWorkSpace controller)
        {

        }

        public void Deactivate()
        {

        }

        /// <summary>
        /// ПОлучить развертку для печати на сплошной фотке, где 4 равных фото
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        private Image GetDisplayImageSinglePhoto(PhotoContainer container)
        {
            if (container.PhotosValue < 4)
                throw new Exception("Для данного типа развертки необзодимо 4 фото в коллекции, сейчас в коллекции " + container.PhotosValue + " фото");

            List<Image> imageList = container.Photos;
            int imageWidth = container.PhotoWidthForScale;
            int imageHeight = container.PhotoHeightForScale;

            const int offsetBetweenX = 20;
            const int offsetBetweenY = 20;
            const int offsetLeft = 37;//отступ слева
            const int offsetRight = 23;//отступ справа
            const int offsetUp = 40;
            const int offsetBottom = 220;
            const int newImageWidth = 251;//Размер по горизонтали, до которого будем обрезатиь картинку, когда она примет размер imageWidth и imageHeight
            const float multiplier = 1.5f;//Коэффициент масштабирования к указанному размеру
            var imgLogo = (Image)container.LogoImage.Clone();

            int width = offsetLeft + (int)(newImageWidth * multiplier * 2) + offsetBetweenX + offsetRight;
            int height = offsetUp + (int)(imageHeight * multiplier * 2) + offsetBetweenY + offsetBottom;

            Image dest = new Bitmap(width, height, PixelFormat.Format32bppPArgb);

            int x = 0;
            int y = 0;

            using (Graphics gr = Graphics.FromImage(dest))
            {
                if (container.Background != null)
                    gr.DrawImage(container.Background, 0, 0, container.Background.Width, container.Background.Height);
                else
                    gr.FillRectangle(new SolidBrush(container.BackColor), 0, 0, width, height); // Очищаем экран нужным цветом

                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                for (int i = 0; i < imageList.Count; i++)
                {
                    x = offsetLeft;
                    //Если число четное - указать игрек
                    //Для высчитывания X - мы всегда делим i на 2 и откидываем остаток

                    if (!IfNumberEven(i))
                        x = offsetLeft + (int)(newImageWidth * multiplier) + offsetBetweenX;

                    var image = CutOffImage(ScaleImage(imageList[i], (int)(imageWidth * multiplier), (int)(imageHeight * multiplier)), (int)(newImageWidth * multiplier), (int)(imageHeight * multiplier));

                    y = (i / 2) * (int)(imageHeight * multiplier) + (i / 2) * offsetBetweenY + offsetUp;

                    gr.DrawImage(image, x, y);
                }
                
                int deltaY = height - (y + (int)(imageHeight * multiplier));
                int deltaX = width;
                int offsetUpAndDown = (int) (deltaY*0.08);//Что бы снизу и сверху были отсупы, на них отведем 8% доступного пространства

                //Сначала промасштабируем логотип, если логотип больше чем нужно
                var coifecient = ScaleImageForFreespacesSizes(imgLogo, deltaX, deltaY - offsetUpAndDown);

                var point = GetLeftUpPointForDrawScreenOnCenter(deltaX, deltaY - offsetUpAndDown, (int)(imgLogo.Width * coifecient),
                                                                (int)(imgLogo.Height * coifecient));

                //Пересчитать координаты точки из пространства "свободного прямоугольника" в пространство фотографии
                point.Y = point.Y + (y + (int)(imageHeight * multiplier)) + offsetUpAndDown/2 + 5;

                gr.DrawImage(imgLogo, point.X, point.Y, imgLogo.Width * coifecient, imgLogo.Height * coifecient);
            }
            
            return dest;
        }

        /// <summary>
        /// ПОлучить развертку для печати на сплошной фотке, где есть одно фото большое и еще 3 маленьких
        /// </summary>
        /// <param name="imageList"></param>
        /// <param name="imageWidth"></param>
        /// <param name="imageHeight"></param>
        /// <returns></returns>
        private Image GetDisplayImageSingleWithOneBigPhoto(PhotoContainer container)
        {
            if (container.PhotosValue < 4)
                throw new Exception("Для данного типа развертки необзодимо 4 фото в коллекции, сейчас в коллекции " + container.PhotosValue + " фото");

            List<Image> imageList = container.Photos;
            int imageWidth = container.PhotoWidthForScale;
            int newImageWidth = (int)(imageWidth*0.9);
            int imageHeight = container.PhotoHeightForScale;
            var imgLogo = (Image)container.LogoImage.Clone();

            const int offsetBetweenX = 20;
            const int offsetBetweenY = 20;
            const int offsetLeft = 50;//отступ слева
            const int offsetRight = 25;//отступ справа
            const int offsetUp = 25;
            const int offsetBottom = 15;
            const int imageValueOnX = 3;//Количество фоток на одну полоску
            const float multiplier = 2.2f;//Коэффициент отличия размеров маленьких фоток от большой

            int width = offsetLeft + (int)(imageWidth * multiplier) + offsetBetweenX + newImageWidth + offsetRight;
            int height = offsetUp + imageHeight * imageValueOnX + offsetBetweenY * imageValueOnX + offsetBottom;

            Image dest = new Bitmap(width, height, PixelFormat.Format32bppPArgb);

            int x = 0;
            int y = 0;

            using (Graphics gr = Graphics.FromImage(dest))
            {
                if (container.Background != null)
                    gr.DrawImage(container.Background, 0, 0, container.Background.Width, container.Background.Height);
                else
                    gr.FillRectangle(new SolidBrush(container.BackColor), 0, 0, width, height); // Очищаем экран

                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                //Сначала прилепим большую картинку
                var image = ScaleImage(imageList[0], (int)(imageWidth * multiplier), (int)(imageHeight * multiplier));
                gr.DrawImage(image, offsetLeft, offsetUp);

                for (int i = 1; i < imageList.Count; i++)
                {
                    x = offsetLeft + (int)(imageWidth * multiplier) + offsetBetweenX;
                    y = offsetUp + offsetBetweenY * (i - 1) + imageHeight * (i - 1);

                    var img = CutOffImage(ScaleImage(imageList[i], imageWidth, imageHeight), newImageWidth, imageHeight);

                    gr.DrawImage(img, x, y);
                }


                int deltaY = height - (offsetUp + (int)(imageHeight * multiplier)) - 20;
                int deltaX = (int)(imageWidth * multiplier);
                int offsetUpAndDown = (int)(deltaY * 0.08);//Что бы снизу и сверху были отсупы, на них отведем 8% доступного пространства

                //Сначала промасштабируем логотип, если логотип больше чем нужно
                var coifecient = ScaleImageForFreespacesSizes(imgLogo, deltaX, deltaY - offsetUpAndDown);

                var point = GetLeftUpPointForDrawScreenOnCenter(deltaX, deltaY - offsetUpAndDown, (int)(imgLogo.Width * coifecient),
                                                                (int)(imgLogo.Height * coifecient));

                //Пересчитать координаты точки из пространства "свободного прямоугольника" в пространство фотографии
                point.Y = point.Y + (offsetUp + (int)(imageHeight * multiplier)) + offsetUpAndDown / 2;
                point.X = point.X + offsetLeft;

                gr.DrawImage(imgLogo, point.X, point.Y+10, imgLogo.Width * coifecient, imgLogo.Height * coifecient);
            }

            dest.RotateFlip(RotateFlipType.Rotate90FlipNone);

            return dest;
        }

        /// <summary>
        /// Одно большое фото на весь лист
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        private Image GetDisplayImageSingleBigPhoto(PhotoContainer container)
        {
            int width = 1279;
            int height = 853;
            List<Image> imageList = container.Photos;

            Image dest = new Bitmap(width, height, PixelFormat.Format32bppPArgb);

            using (Graphics gr = Graphics.FromImage(dest))
            {
                gr.FillRectangle(new SolidBrush(container.BackColor), 0, 0, width, height); // Очищаем экран
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                //Сначала прилепим большую картинку
                var image = ScaleImage(imageList[0], (int)width, (int)height);
                gr.DrawImage(image, 0, 0);
            }

            dest.RotateFlip(RotateFlipType.Rotate90FlipNone);

            return dest;
        }

        /// <summary>
        /// ПОлучить развертку для печати на 2 полоски по 3 фотки на каждой
        /// Отступ слева и справа разный, поправка на поля 5 точек
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        private Image GetDisplayImageForStrips3Foto(PhotoContainer container)
        {
            if (container.PhotosValue < 6)
                throw new Exception("Для данного типа развертки необзодимо 6 фото в коллекции, сейчас в коллекции " + container.PhotosValue + " фото");

            List<Image> imageList = container.Photos;
            int imageWidth = container.PhotoWidthForScale;
            int imageHeight = container.PhotoHeightForScale;
            var imgLogo = (Image)container.LogoImage.Clone();

            const int offsetBetweenX = 40;
            const int offsetBetweenY = 15;
            const int offsetLeft = 37;//отступ слева
            const int offsetRight = 23;//отступ справа
            const int offsetUp = 40;
            const int offsetBottom = 189;
            const int imageValueOnX = 3;//Количество фоток на одну полоску
            int newImageWidth = imageHeight+40;//Хоти сделать картинки квадратными

            int width = newImageWidth * 2 + offsetBetweenX + offsetLeft+offsetRight;
            int height = offsetUp + imageHeight * imageValueOnX + offsetBetweenY * (imageValueOnX - 1) + offsetBottom;//1080

            Image dest = new Bitmap(width, height, PixelFormat.Format32bppPArgb);

            int x = 0;
            int y = 0;

            //Сделаем все картинки квадратными
            using (Graphics gr = Graphics.FromImage(dest))
            {
                if (container.Background != null)
                    gr.DrawImage(container.Background, 0, 0, container.Background.Width, container.Background.Height);
                else
                    gr.FillRectangle(new SolidBrush(container.BackColor), 0, 0, width, height); // Очищаем экран нужным цветом

                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                for (int i = 0; i < imageList.Count; i++)
                {
                    x = offsetLeft;
                    //Если число четное - указать игрек
                    //Для высчитывания X - мы всегда делим i на 2 и откидываем остаток

                    if (!IfNumberEven(i))
                        x = offsetLeft + newImageWidth + offsetBetweenX;

                    var image = CutOffImage(ScaleImage(imageList[i], imageWidth, imageHeight), newImageWidth, imageHeight);

                    y = (i / 2) * imageHeight + (i / 2) * offsetBetweenY + offsetUp;

                    gr.DrawImage(image, x, y);
                }

               
                int deltaY = height - (y + (int)(imageHeight));
                int deltaX = newImageWidth;
                int offsetUpAndDown = (int)(deltaY * 0.08);//Что бы снизу и сверху были отсупы, на них отведем 8% доступного пространства

                //Сначала промасштабируем логотип, если логотип больше чем нужно
                var coifecient = ScaleImageForFreespacesSizes(imgLogo, deltaX, deltaY - offsetUpAndDown);

                var point = GetLeftUpPointForDrawScreenOnCenter(deltaX, deltaY - offsetUpAndDown, (int)(imgLogo.Width * coifecient),
                                                                (int)(imgLogo.Height * coifecient));

                //Пересчитать координаты точки из пространства "свободного прямоугольника" в пространство фотографии
                point.Y = point.Y + (y + (int)(imageHeight)) + offsetUpAndDown / 2;
                point.X = point.X + offsetLeft;

                gr.DrawImage(imgLogo, point.X, point.Y, imgLogo.Width * coifecient, imgLogo.Height * coifecient);

                //нарисовать логотип под 2-м фото
                point.X += newImageWidth + offsetBetweenX;
                gr.DrawImage(imgLogo, point.X, point.Y, imgLogo.Width * coifecient, imgLogo.Height * coifecient);
            }
            return dest;
        }

        /// <summary>
        /// ПОлучить развертку для печати на 2 полоски по 4 фотки на каждой
        /// </summary>
        /// <param name="container"></param>
        private Image GetDisplayImageForStrips4Foto(PhotoContainer container)
        {
            if(container.PhotosValue < 8)
                throw new Exception("Для данного типа развертки необзодимо 8 фото в коллекции, сейчас в коллекции " + container.PhotosValue + " фото");

            const int offsetBetweenX = 40;
            const int offsetBetweenY = 20;
            const int offsetLeft = 37;//отступ слева
            const int offsetRight = 23;//отступ справа
            const int offsetUp = 50;
            const int offsetBottom = 190;
            const int imageValueOnX = 4;//Количество фоток на одну полоску
            var imgLogo = (Image)container.LogoImage.Clone();

            List<Image> imageList = container.Photos;
            int imageWidth = container.PhotoWidthForScale;
            int imageHeight = container.PhotoHeightForScale;

            int width = offsetLeft + imageWidth * 2 + offsetBetweenX + offsetRight;
            int height = offsetUp + imageHeight * imageValueOnX + offsetBetweenY * (imageValueOnX - 1) + offsetBottom;

            Image dest = new Bitmap(width, height, PixelFormat.Format32bppPArgb);

            int x = 0;
            int y = 0;

            using (Graphics gr = Graphics.FromImage(dest))
            {
                if (container.Background != null)
                    gr.DrawImage(container.Background, 0, 0, container.Background.Width, container.Background.Height);
                else
                    gr.FillRectangle(new SolidBrush(container.BackColor), 0, 0, width, height); // Очищаем экран нужным цветом

                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                for (int i = 0; i < imageList.Count; i++)
                {
                    x = offsetLeft;
                    //Если число четное - указать игрек
                    //Для высчитывания X - мы всегда делим i на 2 и откидываем остаток

                    if (!IfNumberEven(i))
                        x = offsetLeft + imageWidth + offsetBetweenX;

                    var image = ScaleImage(imageList[i], imageWidth, imageHeight);

                    y = (i / 2) * imageHeight + (i / 2) * offsetBetweenY + offsetUp;

                    gr.DrawImage(image, x, y);
                }

                int deltaY = height - (y + (int)(imageHeight));
                int deltaX = imageWidth + offsetLeft;
                int offsetUpAndDown = (int)(deltaY * 0.08);//Что бы снизу и сверху были отсупы, на них отведем 8% доступного пространства

                //Сначала промасштабируем логотип, если логотип больше чем нужно
                var coifecient = ScaleImageForFreespacesSizes(imgLogo, deltaX, deltaY - offsetUpAndDown);

                var point = GetLeftUpPointForDrawScreenOnCenter(deltaX, deltaY - offsetUpAndDown, (int)(imgLogo.Width * coifecient),
                                                                (int)(imgLogo.Height * coifecient));

                //Пересчитать координаты точки из пространства "свободного прямоугольника" в пространство фотографии
                point.Y = point.Y + (y + (int)(imageHeight)) + offsetUpAndDown / 2 + 2;

                gr.DrawImage(imgLogo, point.X, point.Y, imgLogo.Width * coifecient, imgLogo.Height * coifecient);

                //нарисовать логотип под 2-м фото
                point.X += imageWidth + offsetBetweenX;
                gr.DrawImage(imgLogo, point.X, point.Y, imgLogo.Width * coifecient, imgLogo.Height * coifecient);
            }
            return dest;
        }

        /// <summary>
        /// Масштабирует переданное изображение, что оно было подогнано под размеры доступного пространства длинной deltaX и шириной deltaY
        /// </summary>
        /// <param name="img"></param>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        /// <returns></returns>
        private float ScaleImageForFreespacesSizes(Image img, int deltaX, int deltaY)
        {
            float coefficient = 1;

            //Если доступное пространство по горизонтали меньше чем картинка
            if (deltaX / img.Width < 1)
            {
                coefficient = (float)deltaX / img.Width;

                //img = ScaleImage(img, (int)(img.Width * coefficient),
                //                                  (int)(img.Height * coefficient));
            }

            //Если доступное пространство по вертикали меньше чем картинка
            if (deltaY / img.Height < 1)
            {
                coefficient = (float)deltaY / img.Height;

                //img = ScaleImage(img, (int)(img.Width * coefficient),
                              //(int)(img.Height * coefficient));
            }

            return coefficient;
        }

        /// <summary>
        /// Возвращает координаты левого верхнего края, что бы отрисовать картинку, которые бы распологалась в центре пространства с шириной deltaX и длинной deltaY
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private Point GetLeftUpPointForDrawScreenOnCenter(int deltaX, int deltaY, int width, int height)
        {
            int x = deltaX/2 - width/2;
            int y = deltaY/2 - height/2;

            return new Point(x,y);
        }

        private static bool IfNumberEven(int number)
        {
            if (number % 2 == 1)
                return false; //не четное число
            else
                return true; //Число четное

        }
    }
}
