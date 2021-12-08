using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitSystem.Classes
{
    class EmailService
    {
        private readonly string message;
        private readonly string recipient;
        private readonly string subj;
        public EmailService(string Message, string Recipient, string Subj)
        {
            message = Message;
            recipient = Recipient;
            subj = Subj;
        }
        public string CreateHTMLBody()
        {
            string Body1 = "<b>FIT Email !</b><h1>GYM SYSTEM</h1>";
            Body1 += "<b><font size=\"4\" color=\"#044fbf\">";
            string Body3 = "</font></b><br><b><font size=\"4\" color=\"read\">Urgent";
            string Body4 = "<b><font size=\"4\" color=\"black\">FIT System</font></b><br><hr>";
            Body4 += "<font size=\"1\" color=\"black\">Powered by FIT</font></b>";
            return Body1 + Body3;
        }

        public void Send()
        {
            try
            {
                MailMessage msg = ComposeMessage();
                msg.To.Add(new MailAddress(address: recipient));
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc00);
                ServicePointManager.MaxServicePointIdleTime = 1;
                SmtpClient smtp = CreateSmtpConn();

                smtp.Send(msg);
                MessageBox.Show("Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private static SmtpClient CreateSmtpConn()
        {
            SmtpClient smtp = new SmtpClient()
            {
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("UmeshanUC@gmail.com", "UCreates@123#UC"),
                //Credentials = new NetworkCredential("umeshanuc@gmail.com", "clemurmngkfjasow"),
                DeliveryMethod = SmtpDeliveryMethod.Network,

            };
            return smtp;
        }

        private MailMessage ComposeMessage()
        {
            MailMessage msg = new MailMessage()
            {
                //From = new MailAddress("systems@cosmos.lk", "System Engineer"),
                From = new MailAddress("UmeshanUC@gmail.com", "FitSystem"),

                Subject = subj,
                IsBodyHtml = true,
                Body = CreateHTMLBody(),
            };
            return msg;
        }
    }
}




