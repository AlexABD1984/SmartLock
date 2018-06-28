using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartLock.MessageBroker
{
    public interface IMessageHandler<in TMessage> : IMessageHandler
        where TMessage : Message
    {
        Task Handle(TMessage message);
    }

    public interface IMessageHandler
    {
    }
}
