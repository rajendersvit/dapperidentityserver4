using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Data.Interface.Service
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
