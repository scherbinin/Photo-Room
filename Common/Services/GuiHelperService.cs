using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Types;
using FotoRoom;
using FotoRoom.ControlUpdater;

namespace Common.Services
{
    public class GuiHelperService : IService
    {
        private readonly Dictionary<Control, Control> _usedControls = new Dictionary<Control, Control>();

        public void Activate(IWorkSpace controller)
        {
        }

        public void Deactivate()
        {

        }

        public void GetIconSelect(Control ctrl, Image icon)
        {
            if (_usedControls.ContainsKey(ctrl) == false)
            {//Выставить выделение
                var pictureBox = new PictureBox
                                     {Size = new Size(50, 50), SizeMode = PictureBoxSizeMode.StretchImage};

                Point pozition = ctrl.Location;

                var iconlocation = new Point(pozition.X + ctrl.Width + (int)(pictureBox.Width * 0.1), pozition.Y + (int)(ctrl.Height/4.0));
                pictureBox.Location = iconlocation;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                Control parent = ctrl.Parent;

                TransparentAdder.SetTransparentControl(parent, pictureBox, icon);

                parent.Controls.Add(pictureBox);

                _usedControls.Add(ctrl, pictureBox);
            }
            else
            {//Убрать выделение
                _usedControls[ctrl].Dispose();
                _usedControls.Remove(ctrl);
            }
        }
    }
}
