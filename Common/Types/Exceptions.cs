using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Types
{
    public class FocusGetExceptions : Exception
    {
        public FocusGetExceptions() : base("Ошибка при совершении фотографирования. Не удается поймать фокус")
            
        {
            
        }
    }
}
