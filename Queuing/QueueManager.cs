using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;

namespace Queuing
{
    public class QueueManager
    {
        private const string QueuePath = @".\Private$\DemoQueue";
        private MessageQueue messageQueue;

        public QueueManager()
        {
            messageQueue = QueueInitializer.InitializeQueue();
        }

        public void Send(string messageBody)
        {
            var message = new Message();
            message.Body = messageBody;
            messageQueue.Send(message);
        }

        public Message Receive()
        {
            return messageQueue.Receive();
        }
    }
}
