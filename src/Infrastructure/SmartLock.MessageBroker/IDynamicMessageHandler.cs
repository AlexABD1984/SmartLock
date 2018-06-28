using System.Threading.Tasks;

namespace SmartLock.MessageBroker
{
    public interface IDynamicMessageHandler
    {
        Task Handle(dynamic eventData);
    }
}