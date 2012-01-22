using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Queuing;
using System.Messaging;

namespace QueueListener
{
    class Program
    {
        private static MessageQueue messageQueue;

        static void Main(string[] args)
        {
            Console.WriteLine("Begining listening to queue...");
            messageQueue = QueueInitializer.InitializeQueue();
            StartListening();
            Console.ReadKey();
        }

        private static void StartListening()
        {
            messageQueue.ReceiveCompleted += new System.Messaging.ReceiveCompletedEventHandler(ReceiveCompleted);
            messageQueue.BeginReceive();
        }

        static void ReceiveCompleted(object sender, System.Messaging.ReceiveCompletedEventArgs e)
        {
            Console.WriteLine("Receiving message...");
            var message = messageQueue.EndReceive(e.AsyncResult);
            Console.WriteLine(message.Body.ToString());
            messageQueue.BeginReceive();
        }
    }
}
