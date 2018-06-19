using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.Types;

namespace Common.Services
{
    [Serializable]
    public class MailContext
    {
        public MailContext(string mailTo, Image pictureAttach)
        {
            MailTo = mailTo;
            AttachFile = pictureAttach;
        }

        public string MailTo { get; private set; }
        public Image AttachFile { get; private set; }
    }

    public class NetworkService : IService
    {
        private const string FileName = "NotSendedMails.bin";
        private List<MailContext> _notSendMailsList;
        private const string Caption = "Ваш классный снимок=)";
        private const string MessageBody = @"
Добрый день, уважаемый гость «PhotoRoom»!

В данном письме Вы найдете снимок, сделанный в нашей фотобудке.

Если Вы хотите получить оригиналы всех кадров, то ответьте на это письмо.
В тексте письма будет достаточно двух слов «Хочу оригиналы», и Вы обязательно их получите.

Вас было очень приятно фотографировать! Ждем Вас снова в фотобудке «PhotoRoom»!

С уважением,
Ваш менеджер Юлия
Аренда фотобудки PhotoRoom
тел. 8 (831) 283 90 83, 8 (930) 283 90 83 (Нижний Новгород)
       8 (8352) 377 397 (Чебоксары и Йошкар-Ола)";

        private const string MailFrom = "1@mail.ru";
        private const string MailPass = "";

        /// <summary>
        /// Проверка на доступность сети
        /// </summary>
        public bool IsConnectionOk()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                //MessageBox.Show("Отсутствует или ограниченно физическое подключение к сети\nПроверьте настройки вашего сетевого подключения");
                return false;
            }

            //bool isConnected = false;
            //using (var tcpClient = new TcpClient())
            //{
            //    tcpClient.Connect("209.85.148.138", 443); // google
            //    isConnected = tcpClient.Connected;
            //}

            //if (!isConnected)
            //{
            //    //MessageBox.Show("Нет подключения к интернету\nПроверьте ваш фаервол или настройки сетевого подключения");
            //    return false;
            //}

            return true;
        }

        /// <summary>
        /// Послать почту
        /// </summary>
        /// <param name="photo"></param>
        /// <param name="mailAdress"></param>
        public void EmailSend(Image photo, string mailAdress)
        {
            if (IsConnectionOk())
            {
                //Сперва загрузим не отосланные месседжи
                LoadNotSendedMails();

                Action<object> action = (object obj) => SendMail((MailContext)obj);

                try
                {
                    //Посылаем письмо
                    var task = new Task(action, new MailContext(mailAdress, photo));
                    task.Start();
                }
                catch (Exception)
                {//если эксепшн во время посылки, кидаем письмо в контейнер, сохраняем его и выходим
                    MessageBox.Show("Ваше письмо будет выслано по указанному вами адресу как только сеть снова будет доступна.", "Ошибка, сеть недоступна");

                    _notSendMailsList.Add(new MailContext(mailAdress, photo));
                    Serialize(_notSendMailsList);
                }

                //Посылаем все ранее не отправленные
                if (_notSendMailsList.Count != 0)
                {
                    //Письма есть, шлем
                    foreach (var mailContext in _notSendMailsList)
                    {
                        var task = new Task(action, mailContext);
                        task.Start();
                    }

                    //Удаляем файл с не отосланными письмами и валим копию из памяти
                    if(File.Exists(FileName))
                        File.Delete(FileName);

                    _notSendMailsList=null;
                }
            }
            else
            //Сеть недоступна
            {
                MessageBox.Show("Ваше письмо будет выслано по указанному вами адресу как только сеть снова будет доступна.", "Ошибка, сеть недоступна");
                _notSendMailsList.Add(new MailContext(mailAdress, photo));
                Serialize(_notSendMailsList);
            }
        }

        public static void SendMail(MailContext mailContext)
        {
            string mailto = mailContext.MailTo;
            Image attachFile = mailContext.AttachFile;
            string fileName = Guid.NewGuid() + ".jpg";
            string smtpServer = "smtp." + mailto.Split('@')[1];

            if (attachFile != null)
            {
                var stream = new FileStream(fileName, FileMode.Create);
                attachFile.Save(stream, ImageFormat.Jpeg);
                stream.Flush();
                stream.Close();
                stream.Dispose();
            }

            try
            {
                var mail = new MailMessage();
                mail.From = new MailAddress(MailFrom);
                mail.To.Add(new MailAddress(mailto));
                mail.Subject = Caption;
                mail.Body = MessageBody;

                if (attachFile != null)
                    mail.Attachments.Add(new Attachment(fileName));

                var client = new SmtpClient();
                client.Host = smtpServer;
                client.Timeout = 300000;//Время на отправку 5 минуты
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(MailFrom.Split('@')[0], MailPass);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);
                mail.Dispose();
            }
            catch (Exception e)
            {
                throw new Exception("Mail.Send: " + e.Message);
            }
            finally
            {
                if (File.Exists(fileName))
                    File.Delete(fileName);
            }
        }

        public void Activate(FotoRoom.IWorkSpace controller)
        {
            LoadNotSendedMails();
        }

        private void LoadNotSendedMails()
        {
            if (File.Exists(FileName))
            {
                var formatter = new BinaryFormatter();
                var fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                _notSendMailsList = (List<MailContext>)formatter.Deserialize(fs);
                fs.Close();
            }
            else
            {
                _notSendMailsList = new List<MailContext>();
            }
        }

        private void Serialize(List<MailContext> container)
        {
            var formatter = new BinaryFormatter();
            var fs = new FileStream(FileName, FileMode.Create);
            formatter.Serialize(fs, container);
        }

        public void Deactivate()
        {
        }
    }
}
