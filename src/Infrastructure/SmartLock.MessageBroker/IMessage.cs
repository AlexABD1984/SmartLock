using System;
using System.Collections.Generic;
using System.Text;

namespace SmartLock.MessageBroker
{
    public interface IMessage
    {
        Guid Id { get; }
        DateTime CreationDate { get; }
    }
}
