using System.Threading.Tasks;

namespace Core.Common.Contracts
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
