using System.Threading.Tasks;

namespace StuffRescue.Web.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
