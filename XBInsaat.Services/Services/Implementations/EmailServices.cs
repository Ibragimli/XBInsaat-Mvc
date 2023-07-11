using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Data.Datacontext;
using XBInsaat.Services.Services.Interfaces;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Text;
using MailKit.Security;
using MimeKit.Text;

namespace XBInsaat.Services.Services.Implementations
{
    public class EmailServices : IEmailServices
    {

       
        public void Send(string to, string subject, string html)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("idagrouptester@yandex.ru"));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.yandex.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("idagrouptester@yandex.ru", "idagroup123");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
