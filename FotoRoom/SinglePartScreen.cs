using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FotoRoom.View;

namespace FotoRoom
{
    public class SinglePartScreen : MainSceneBase
    {
        public override void StartScene_Load(object sender, EventArgs e)
        {
            ChangeView<StartView>(this);
        }
    }
}
