using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;

namespace MessageProcessor.Queuing
{
    class QueueManager
    {
        private const string QueuePath = @".\Private$\DemoQueue";
        private MessageQueue messageQueue;

        public QueueManager()
        {
            InitializeQueue();
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

        private void InitializeQueue()
        {
            if (!MessageQueue.Exists(QueuePath))
                messageQueue = MessageQueue.Create(QueuePath);
            else
                messageQueue = new MessageQueue(QueuePath);
            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
        }
    }
}
