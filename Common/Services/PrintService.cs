using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Common.Types;
using FotoRoom;

namespace Common.Services
{
    public class PrintService : IService
    {
        private IWorkSpace _controller;
        //private const string FileName = "d:\\StripsPhotoSettings.dat";

        public void Print(Image img, byte[] printMode, int copyVal)
        {
            //var cloneImg = (Image)img.Clone();

            var pd = new PrintDocument();
            SetDevmode(pd.DefaultPageSettings.PrinterSettings,2,"",printMode);
            pd.DefaultPageSettings.PrinterSettings.Copies = (short)copyVal;
            pd.PrintController = new System.Drawing.Printing.StandardPrintController();//должно убрать окошко

            var pr = pd.DefaultPageSettings.PrinterSettings.PaperSizes;
            pd.DefaultPageSettings.Landscape = true; //or false!
            pd.DefaultPageSettings.Margins = new Margins(0,0,0,0);

            pd.PrintPage += (sender, args) =>
            {
                var e = args.Graphics;

                //e.DrawImage(cloneImg, 0, 0, 405, 611);
                e.DrawImage(img, 0, 0, 405, 611);
            };
            pd.Print();
        }

        #region Методы для загрузки/сохранения свойств принтера

        [DllImport("winspool.Drv", EntryPoint = "DocumentPropertiesW", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern int DocumentProperties(IntPtr hwnd, IntPtr hPrinter, [MarshalAs(UnmanagedType.LPWStr)] string pDeviceName, IntPtr pDevModeOutput, IntPtr pDevModeInput, int fMode);
        [DllImport("kernel32.dll", ExactSpelling = true)]
        public static extern IntPtr GlobalFree(IntPtr handle);
        [DllImport("kernel32.dll", ExactSpelling = true)]
        public static extern IntPtr GlobalLock(IntPtr handle);
        [DllImport("kernel32.dll", ExactSpelling = true)]
        public static extern IntPtr GlobalUnlock(IntPtr handle);
        /// <summary>
        /// Применение настроек принтера полученных из файла или из массива байт (mode=1 - загрузка из файла/ mode=2 - загрузка из переданного массива)
        /// </summary>
        /// <param name="printerSettings"></param>
        /// <param name="Filename"></param>
        /// /// <param name="Filename"></param>
        public void SetDevmode(PrinterSettings printerSettings, int mode, String Filename, byte[] devmodearray)
        //Grabs the data in arraylist and chucks it back into memory "Crank the suckers out"
        {
            ///int mode
            ///1 = Load devmode structure from file
            ///2 = Load devmode structure from arraylist
            IntPtr hDevMode = IntPtr.Zero;                        // a handle to our current DEVMODE
            IntPtr pDevMode = IntPtr.Zero;                          // a pointer to our current DEVMODE
            Byte[] Temparray;
            try
            {
                var DevModeArray = devmodearray;
                // Obtain the current DEVMODE position in memory
                hDevMode = printerSettings.GetHdevmode(printerSettings.DefaultPageSettings);
                // Obtain a lock on the handle and get an actual pointer so Windows won't move
                // it around while we're futzing with it
                pDevMode = GlobalLock(hDevMode);
                // Overwrite our current DEVMODE in memory with the one we saved.
                // They should be the same size since we haven't like upgraded the OS
                // or anything.
                if (mode == 1)  //Load devmode structure from file
                {
                    FileStream fs = new FileStream(Filename, FileMode.Open, FileAccess.Read);
                    Temparray = new byte[fs.Length];
                    fs.Read(Temparray, 0, Temparray.Length);
                    fs.Close();
                    fs.Dispose();
                    for (int i = 0; i < Temparray.Length; ++i)
                    {
                        Marshal.WriteByte(pDevMode, i, Temparray[i]);
                    }
                }
                if (mode == 2)  //Load devmode structure from arraylist
                {
                    for (int i = 0; i < DevModeArray.Length; ++i)
                    {
                        Marshal.WriteByte(pDevMode, i, DevModeArray[i]);
                    }
                }
                // We're done futzing
                GlobalUnlock(hDevMode);
                // Tell our printer settings to use the one we just overwrote
                printerSettings.SetHdevmode(hDevMode);
                printerSettings.DefaultPageSettings.SetHdevmode(hDevMode);
                // It's copied to our printer settings, so we can free the OS-level one
                GlobalFree(hDevMode);
            }
            catch (Exception ex)
            {
                if (hDevMode != IntPtr.Zero)
                {
                    MessageBox.Show("BUGGER");
                    GlobalUnlock(hDevMode);
                    // And to boot, we don't need that DEVMODE anymore, either
                    GlobalFree(hDevMode);
                    hDevMode = IntPtr.Zero;
                }
            }
        }
        
        /// <summary>
        /// Сохранение в файл или в массив байт (mode = 1 - в файл/ mode=2 - в массив)
        /// </summary>
        /// <param name="printerSettings"></param>
        /// <param name="mode"></param>
        /// <param name="Filename"></param>
        public byte[]  SaveDevmode(PrinterSettings printerSettings, int mode, String Filename)
        //Grabs the devmode data in memory and stores in arraylist
        {
            ///int mode
            ///1 = Save devmode structure to file
            ///2 = Save devmode structure to Byte array and arraylist
            IntPtr hDevMode = IntPtr.Zero;                        // handle to the DEVMODE
            IntPtr pDevMode = IntPtr.Zero;                          // pointer to the DEVMODE
            IntPtr hwnd = ((Form)_controller).Handle;

            byte[] DevModeArray = null;

            try
            {
                // Get a handle to a DEVMODE for the default printer settings
                hDevMode = printerSettings.GetHdevmode(printerSettings.DefaultPageSettings);
                // Obtain a lock on the handle and get an actual pointer so Windows won't
                // move it around while we're futzing with it
                pDevMode = GlobalLock(hDevMode);
                int sizeNeeded = DocumentProperties(hwnd, IntPtr.Zero, printerSettings.PrinterName, IntPtr.Zero, pDevMode, 0);
                if (sizeNeeded <= 0)
                {
                    MessageBox.Show("Devmode Bummer, Cant get size of devmode structure");
                    GlobalUnlock(hDevMode);
                    GlobalFree(hDevMode);
                    return null;
                }
                
                DevModeArray = new byte[sizeNeeded];    //Copies the buffer into a byte array
                if (mode == 1)  //Save devmode structure to file
                {
                    FileStream fs = new FileStream(Filename, FileMode.Create);
                    for (int i = 0; i < sizeNeeded; ++i)
                    {
                        fs.WriteByte(Marshal.ReadByte(pDevMode, i));
                    }
                    fs.Close();
                    fs.Dispose();
                }
                if (mode == 2)  //Save devmode structure to Byte array and arraylist
                {
                    for (int i = 0; i < sizeNeeded; ++i)
                    {
                        DevModeArray[i] = (byte)(Marshal.ReadByte(pDevMode, i));
                        //Copies the array to an arraylist where it can be recalled
                    }
                }
                // Unlock the handle, we're done futzing around with memory
                GlobalUnlock(hDevMode);
                // And to boot, we don't need that DEVMODE anymore, either
                GlobalFree(hDevMode);
                hDevMode = IntPtr.Zero;
            }
            catch (Exception ex)
            {
                if (hDevMode != IntPtr.Zero)
                {
                    MessageBox.Show("BUGGER");
                    GlobalUnlock(hDevMode);
                    // And to boot, we don't need that DEVMODE anymore, either
                    GlobalFree(hDevMode);
                    hDevMode = IntPtr.Zero;
                }
            }

            return DevModeArray;
        }

        #endregion

        public void Activate(IWorkSpace controller)
        {
            _controller = controller;
        }

        public void Deactivate()
        {
        }

        //public  void qwe(object sender, PrintPageEventArgs args)
        //{

        //    Rectangle m = args.MarginBounds;

        //    //m.Location = new Point(0,0);

        //    //m.Height += 100;
        //    //m.Width += 100;

        //    //Центровка картинки
        //    //if ((double)_img.Width / (double)_img.Height > (double)m.Width / (double)m.Height) // image is wider
        //    //{
        //    //    m.Height = (int)((double)_img.Height / (double)_img.Width * (double)m.Width);
        //    //}
        //    //else
        //    //{
        //    //    m.Width = (int)((double)_img.Width / (double)_img.Height * (double)m.Height);
        //    //}

        //    //args.Graphics.DrawImage(_img, m);
        //}
    }
}
