using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Common.Services;

namespace FotoRoom.Stuff
{
    /// <summary>
    /// Обертка для PictureBox
    /// </summary>
    public class PictureBoxCoverage
    {
        private PictureBox _pictureBox;
        // Флаг того что нужно перефоткать
        public bool IsNeedReFoting { get; private set; }

        private readonly GuiHelperService _selectService;

        public PictureBoxCoverage(PictureBox pb, IWorkSpace serviceCollection)
        {
            _selectService = serviceCollection.GetService<GuiHelperService>();

            _pictureBox = pb;
        }

        public PictureBoxCoverage()
        {
            throw new Exception("Не следует использовать этот конструктор");
        }

        /// <summary>
        /// Выставить изобращение
        /// </summary>
        public Image Image
        {
            set
            {
                _pictureBox.Image = value;
            }
            get { return _pictureBox.Image; }
        }

        public bool InvokeRequired
        {
            get { return _pictureBox.InvokeRequired; }
        }

        public PictureBox PictureBox
        {
            get { return _pictureBox; }
        }

        /// <summary>
        /// Пометить фотку для перефотографирования, если кликаем еще раз, то отменяем пометку
        /// </summary>
        public void ChooseForRefoting(Image img)
        {
            IsNeedReFoting = !IsNeedReFoting;
            _selectService.GetIconSelect(PictureBox, img);
        }
    }
}
