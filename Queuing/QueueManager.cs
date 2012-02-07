using System.Messaging;

namespace Queuing
{
    public class QueueManager
    {
        private readonly MessageQueue messageQueue;

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
