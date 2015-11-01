using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FotoRoom.Stuff
{
    /// <summary>
    /// Порядяковые номера сделанных кадров
    /// </summary>
    public enum PhotoCadrNumber
    {
        FistCadr = 1,
        SecondCadr =2,
        ThirdCadr = 3,
        FourthCadr = 4,
        FifthCadr = 5,
        SixthCadr = 6,
        SeventhCadr = 7,
        EighthCadr = 8
    }

    /// <summary>
    /// Режим съемки: количество фоток и тип выходной фотки
    /// </summary>
    public enum PhotoShotMode
    {
        /// <summary>
        /// 2 полоски по 3 фотки на каждой
        /// </summary>
        Strips3Foto = 6,

        /// <summary>
        /// 2 полоски по 4 фотки на каждой
        /// </summary>
        Strips4Foto = 8,

        /// <summary>
        /// Одно большое фото, где 4 фотки, одна большая, остальные маленькие
        /// </summary>
        Single4Foto1Big = 4,

        /// <summary>
        /// Одно большое фото, где все 4 фотки равных размеров
        /// </summary>
        Single4Foto = 5,

        /// <summary>
        /// Одно большое фото
        /// </summary>
        SingleFoto = 1
    }

    public enum PaperTypeEnume
    {
        Motovay = 1,
        Glynec = 2
    }
}
