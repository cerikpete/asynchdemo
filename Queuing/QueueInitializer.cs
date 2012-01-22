using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;

namespace Queuing
{
    public static class QueueInitializer
    {
        private const string QueuePath = @".\Private$\DemoQueue";

        public static MessageQueue InitializeQueue()
        {
            MessageQueue messageQueue;
            if (!MessageQueue.Exists(QueuePath))
                messageQueue = MessageQueue.Create(QueuePath);
            else
                messageQueue = new MessageQueue(QueuePath);
            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            return messageQueue;
        }
    }
}
