using ECommerceAPI.Application.Abstraction.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Services
{
    public class MailService : IMailService
    {
        readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            SendMailAsync(new[] {to} , subject, body, isBodyHtml);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {

            MailMessage mail = new()
            {
                IsBodyHtml = isBodyHtml
            };

            foreach (var to in tos) 
                mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new("cikko121212@gmail.com", "Ciko", System.Text.Encoding.UTF8);

            SmtpClient smtp = new()
            {
                Credentials = new NetworkCredential("cikko121212@gmail.com", "Nerimanov_002"),
                Port = 587,
                EnableSsl = true,
                Host = "srcv179.turhost.com"
            };
            await smtp.SendMailAsync(mail);
        }
         
        public async Task SendPasswordResetMail(string to, string userId, string resetToken)
        {
            StringBuilder mail = new();

            mail.AppendLine("Hello<br>If you requested for new password, you can reset your password using the link below. <br> <strong><a targer=\"_blank\" href=\"");
            mail.AppendLine(_configuration["AngularClientUrl"]);
            mail.AppendLine("/update-password/");
            mail.AppendLine(userId);
            mail.AppendLine("/");
            mail.AppendLine(resetToken);
            mail.AppendLine("\">Click for reset password </a></strong><br><br><span style=\"font-size:12px;\"> If it is not done by you please don't consider mail </span> <br> With our respect...<br><br><br>Mini|E-Commerce");

            await SendMailAsync(to, "Password reset", mail.ToString());
        }

        public async Task SendCompletedOrderMailAsync(string to, string orderCode, DateTime orderDate, string username)
        {
            string mail = $"Sayın {username} Merhaba<br>" +
                $"{orderDate} tarihinde vermiş olduğunuz {orderCode} kodlu siparişiniz tamamlanmış ve kargo firmasına verilmiştir.<br>Hayrını görünüz efendim...";

            await SendMailAsync(to, $"{orderCode} Sipariş Numaralı Siparişiniz Tamamlandı", mail);
        }
    }
}

