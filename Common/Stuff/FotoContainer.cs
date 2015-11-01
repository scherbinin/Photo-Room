using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using FotoRoom.Stuff;

namespace Common.Stuff
{
    public class PhotoContainer 
    {
        /// <summary>
        /// Список фоток
        /// </summary>
        public List<Image> Photos = new List<Image>();

        /// <summary>
        /// Количество фоток
        /// </summary>
        public int PhotosValue
        {
            get { return Photos.Count; }
        }

        public PhotoShotMode PhotoMode { get; set; }

        /// <summary>
        /// Длинна фото по горизонтали, до которой будем ее масштабировать
        /// </summary>
        public int PhotoWidthForScale { get; set; }

        /// <summary>
        /// Длинна фото по вертикали, до которой будем ее масштабировать
        /// </summary>
        public int PhotoHeightForScale { get; set; }

        /// <summary>
        /// Логотип
        /// </summary>
        public Image LogoImage { get; set; }

        /// <summary>
        /// Цвет заливки фотки
        /// </summary>
        public Color BackColor { get; set; }

        /// <summary>
        /// Текстура на фото, альтернатива простой цветовой заливке (BackColor)
        /// </summary>
        public Image Background { get; set; }
    }
}
