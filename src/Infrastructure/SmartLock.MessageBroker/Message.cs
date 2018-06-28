using System;
using System.Collections.Generic;
using System.Text;

namespace SmartLock.MessageBroker
{
    public class Message : IMessage
    {
        public Message()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public Guid Id { get; }
        public DateTime CreationDate { get; }
    }
}
