using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Data.Interface.Service
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
