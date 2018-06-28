using System;
using System.Collections.Generic;
using System.Text;
using static SmartLock.MessageBroker.InMemoryMessageBrokerSubscriptionsManager;

namespace SmartLock.MessageBroker
{
    public interface IMessageBrokerSubscriptionsManager
    {
        bool IsEmpty { get; }
        event EventHandler<string> OnEventRemoved;
        void AddDynamicSubscription<TH>(string eventName)
           where TH : IDynamicMessageHandler;

        void AddSubscription<T, TH>()
           where T : Message
           where TH : IMessageHandler<T>;

        void RemoveSubscription<T, TH>()
             where TH : IMessageHandler<T>
             where T : Message;
        void RemoveDynamicSubscription<TH>(string eventName)
            where TH : IDynamicMessageHandler;

        bool HasSubscriptionsForEvent<T>() where T : Message;
        bool HasSubscriptionsForEvent(string messageName);
        Type GetEventTypeByName(string messageName);
        void Clear();
        IEnumerable<SubscriptionInfo> GetHandlersForMessage<T>() where T : Message;
        IEnumerable<SubscriptionInfo> GetHandlersForMessage(string messageName);
        string GetEventKey<T>();
    }
}
