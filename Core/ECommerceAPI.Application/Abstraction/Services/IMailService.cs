using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstraction.Services
{
    public interface IMailService
    {
        Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true);
        Task SendMailAsync(string[] to, string subject, string body, bool isBodyHtml = true);
        Task SendPasswordResetMail(string to, string userId, string resetToken);
        Task SendCompletedOrderMailAsync(string to, string orderCode, DateTime orderDate, string username);
    }
}
