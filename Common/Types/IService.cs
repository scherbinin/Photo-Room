using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FotoRoom;

namespace Common.Types
{
    public interface IService
    {
        void Activate(IWorkSpace controller);
        void Deactivate();
    }
}
