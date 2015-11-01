using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FotoRoom
{
    public interface IWorkSpace
    {
        void ChangeView<T>(params object[] args) where T : UserControl;
        T GetService<T>() where T : class;
    }
}
