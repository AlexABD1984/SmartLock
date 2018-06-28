using System;
using System.Collections.Generic;
using static SmartLock.MessageBroker.InMemoryMessageBrokerSubscriptionsManager;

namespace SmartLock.MessageBroker.RabbitMQ
{
    public interface IMessageBrokerManager
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
            bool HasSubscriptionsForEvent(string eventName);
            Type GetEventTypeByName(string eventName);
            void Clear();
            IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : Message;
            IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);
            string GetEventKey<T>();
        }
    }