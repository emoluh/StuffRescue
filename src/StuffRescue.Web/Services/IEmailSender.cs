using System.Threading.Tasks;

namespace StuffRescue.Web.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
