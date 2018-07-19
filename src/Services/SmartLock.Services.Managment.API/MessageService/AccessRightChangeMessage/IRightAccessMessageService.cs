using SmartLock.MessageBroker;
using System.Threading.Tasks;

namespace SmartLock.Services.Managment.API.MessageService
{
    public interface IAccessRightMessageService
    {
        Task SaveMessageAndAccessRightContextChangesAsync(Message message);
        Task PublishThroughMessageBrokerAsync(Message message);
    }
}