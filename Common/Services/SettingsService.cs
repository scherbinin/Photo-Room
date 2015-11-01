using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Common.Properties;
using Common.Types;
using FotoRoom;
using FotoRoom.Stuff;

namespace Common.Services
{
    /// <summary>
    /// Сервис хранения настроек и ресурсов
    /// </summary>
    public class SettingsService : IService
    {
        private const string SettingsFileName = "c:\\settings.bin";
        private const string LogoDefaultName = "LogoDefault";
        private readonly Color _defaultBacgroundColor = Color.DarkBlue;

        private SettingsEntity _entity = null;

        private void InitializeEntity()
        {
            var formatter = new BinaryFormatter();

            if(File.Exists(SettingsFileName))
            {
                //Десериализовать из файла

                var fs = new FileStream(SettingsFileName, FileMode.Open, FileAccess.Read);
                _entity = (SettingsEntity) formatter.Deserialize(fs);
                fs.Close();
                fs.Dispose();
            }
            else
            {

                _entity = InitializeEntityByDefault();

                Serialize(_entity);
            }
        }

        /// <summary>
        /// Создание сущности настроек по умолчанию
        /// </summary>
        /// <returns></returns>
        public SettingsEntity InitializeEntityByDefault()
        {
            //Создать по умолчанию и сериализовать
            return new SettingsEntity()
            {
                ProgramBackground = Properties.Resources.DefaultBackground,
                BackgroundPhotoColor = _defaultBacgroundColor,
                PhotoBackground = null,
                PhotoLogo = Properties.Resources.LogoDefault,
                PaperType = PaperTypeEnume.Motovay,
                SettingsForPrintMatovoeSingleFoto = Resources.SinglePhotoSettings,
                SettingsForPrintMatovoeStripsFoto = Resources.StripsPhotoSettings,
                SettingsForPrintGlanectStripsFoto = Resources.StripsGlossyPhotoSettings,
                SettingsForPrintGlanecSingleFoto = Resources.SingleGlossyPhotoSettings
            };
        }

        private void Serialize(SettingsEntity entity)
        {
            _entity = entity;

            var formatter = new BinaryFormatter();
            var fs = new FileStream(SettingsFileName, FileMode.Create);
            formatter.Serialize(fs, _entity);
            fs.Close();
            fs.Dispose();
        }

        public SettingsEntity GetSettings()
        {
            return _entity;
        }

        public void SaveSettings(SettingsEntity entity)
        {
            Serialize(entity);
        }

        public void Activate(IWorkSpace controller)
        {
            InitializeEntity();
        }

        public void Deactivate()
        {
            Serialize(_entity);
        }
    }

    /// <summary>
    /// Сущность хранения настроек
    /// </summary>
    [Serializable]
    public class SettingsEntity
    {
        /// <summary>
        /// Тип бумаги
        /// </summary>
        public PaperTypeEnume PaperType;
        /// <summary>
        /// Текстура фона програмки
        /// </summary>
        public Image ProgramBackground;
        /// <summary>
        /// Текстура фона фотографии
        /// </summary>
        public Image PhotoBackground;
        /// <summary>
        /// Картинка логотипа
        /// </summary>
        public Image PhotoLogo;
        /// <summary>
        /// Цвет фона
        /// </summary>
        public Color BackgroundPhotoColor;
        /// <summary>
        /// Настройки для печати одного фото
        /// </summary>
        public byte[] SettingsForPrintSingleFoto;
        /// <summary>
        /// Настройки для печати двух полосок
        /// </summary>
        public byte[] SettingsForPrintStripsFoto;

        public byte[] SettingsForPrintMatovoeSingleFoto;
        public byte[] SettingsForPrintMatovoeStripsFoto;
        public byte[] SettingsForPrintGlanecSingleFoto;
        public byte[] SettingsForPrintGlanectStripsFoto;

        /// <summary>
        /// Количество распечатанных фото
        /// </summary>
        public int PagePrinted;
    }
}
