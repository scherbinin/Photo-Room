using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Services;
using FotoRoom;

namespace test
{
    public partial class PrinterSettingsWorker : Form, IWorkSpace
    {
        public PrinterSettingsWorker()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
//            var printService = new PrintService();
//            printService.Activate(this);
//
//            var pd = new PrintDocument();
//            printService.SaveDevmode(pd.PrinterSettings, 1, "d:\\StripsGlossyPhotoSettings.dat");

            var service = new NetworkService();
            service.Activate(this);

            service.EmailSend(new Bitmap(10, 10), "scherbininiliy@mail.ru");

        }

        public void ChangeView<T>(params object[] args) where T : UserControl
        {
            throw new NotImplementedException();
        }

        public T GetService<T>() where T : class
        {
            throw new NotImplementedException();
        }
    }
}
