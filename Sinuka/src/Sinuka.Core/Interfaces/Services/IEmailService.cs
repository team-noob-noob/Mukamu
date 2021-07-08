using System.Threading.Tasks;

namespace Sinuka.Core.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmail(string to, string message, string subject);
    }
}