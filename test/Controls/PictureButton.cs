using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FotoRoom.Controls
{
    public class PictureButton : UserControl
    {
        public Image Image;
        public Image ImageUp;
        private readonly Control _parent;

        public PictureButton(Control parent)
        {
            _parent = parent;

            var point = new Point(Left, Top);
            var width = Width;
            var height = Height;

            var destRect = new Rectangle(0, 0, width, height);//куда вставляем
            var srcRect = new Rectangle(point.X, point.Y, width, height);//Что вырезаем

            var buff = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            var dest = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            using (var gr = Graphics.FromImage(dest))
            {
                _parent.DrawToBitmap(buff, new Rectangle(0, 0, Width, Height));
                gr.DrawImage(buff, destRect, srcRect, GraphicsUnit.Pixel);
                
                if(Image!=null)
                    gr.DrawImage(Image, 0, 0, width, height);
            }

            Image = dest;
        }
    }
}
