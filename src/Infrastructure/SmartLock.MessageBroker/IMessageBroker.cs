using System;
using System.Collections.Generic;
using System.Text;

namespace SmartLock.MessageBroker
{
    public interface IMessageBroker
    {
        void Publish(Message message);

        void Subscribe<T, TM>()
            where T : Message
            where TM : IMessageHandler<T>;

        void SubscribeDynamic<TM>(string messageName)
            where TM : IDynamicMessageHandler;

        void UnsubscribeDynamic<TM>(string messageName)
            where TM : IDynamicMessageHandler;

        void Unsubscribe<T, TM>()
            where T : Message
            where TM : IMessageHandler<T>;
            
    }
}
