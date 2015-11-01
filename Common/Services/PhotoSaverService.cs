using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Types;
using FotoRoom;

namespace Common.Services
{
    public class PhotoSaverService : IService
    {
        private const string FolderName = "Photos";

        private List<Image> _fotoContainer = new List<Image>();

        public PhotoSaverService()
        {
        }

        public void Activate(IWorkSpace controller)
        {
        }

        public void Deactivate()
        {
        }

        public void PushToPhotoContainer(Image img)
        {
            this._fotoContainer.Add(img);
        }

        public void SaveAndClearContainer()
        {
            int num = -1;
            if (!Directory.Exists(FolderName))
            {
                Directory.CreateDirectory(FolderName);
            }
            FileInfo[] files = (new DirectoryInfo(FolderName)).GetFiles();
            int num1 = -1;
            FileInfo[] fileInfoArray = files;
            for (int i = 0; i < (int)fileInfoArray.Length; i++)
            {
                FileInfo fileInfo = fileInfoArray[i];
                int.TryParse(fileInfo.Name.Split(".".ToCharArray())[0], out num);
                if (num1 < num)
                {
                    num1 = num;
                }
            }
            num1++;
            (new Task((object obj) =>
            {
                this.SavePictutreToFolder(obj.ToString(), num1);
                lock (new object())
                {
                    this._fotoContainer.Clear();
                }
            }, FolderName)).Start();
        }

        private void SavePictutreToFolder(string newDirectoryPath, int startIndex)
        {
            for (int i = 0; i < this._fotoContainer.Count; i++)
            {
                int num = startIndex + i;
                string str = string.Concat(newDirectoryPath, "\\", num.ToString(), ".jpg");
                FileStream fileStream = new FileStream(str, FileMode.Create);
                this._fotoContainer[i].Save(fileStream, ImageFormat.Jpeg);
                fileStream.Flush();
                fileStream.Close();
                fileStream.Dispose();
            }
        }
    }
}
