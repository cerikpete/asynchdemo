using System;
using Core;
using Queuing;
using System.Messaging;
using log4net;

namespace QueueListener
{
    class Program
    {
        private static MessageQueue messageQueue;

        static void Main(string[] args)
        {
            LogManager.GetLogger(typeof(Program));

            Log<Program>.Debug("Begining listening to queue...");
            messageQueue = QueueInitializer.InitializeQueue();
            StartListening();
            Console.ReadKey();
        }

        private static void StartListening()
        {
            messageQueue.ReceiveCompleted += ReceiveCompleted;
            messageQueue.BeginReceive();
        }

        static void ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            Log<Program>.Debug("Receiving message...");
            var message = messageQueue.EndReceive(e.AsyncResult);
            Log<Program>.Info(message.Body.ToString());
            messageQueue.BeginReceive();
        }
    }
}
