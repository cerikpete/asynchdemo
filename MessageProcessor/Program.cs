using System;
using System.Threading;
using System.Threading.Tasks;
using Core;
using MessageProcessor.Messages;
using Queuing;
using System.Messaging;
using log4net;

namespace MessageProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            LogManager.GetLogger(typeof (Program));

            Log<Program>.Debug("Entering the processor");

            SimpleDemoOfTasks();

            Log<Program>.Debug("End of Main method");
            Console.ReadKey();
        }

        private static void ThreadsTheOldSchoolWay()
        {
            var writer = new ConsoleWriter();
            Thread thread = new Thread(writer.WriteToConsole);
            thread.Start();
            thread.Join();
        }

        private static void SimpleDemoOfTasks()
        {
            var task1 = Task.Factory.StartNew(() =>
            {
                var writer = new MessageWriter();
                writer.WriteMessage("This is the first message with a 5 second delay", 5);
            });

            var task2 = Task.Factory.StartNew(() =>
            {
                var writer = new MessageWriter();
                writer.WriteMessage("This is the second message");
            });

            //Task.WaitAll(task1, task2);
        }

        private static void DemoTaskWhereTheOuputIsUsed()
        {
            var task = Task<string>.Factory.StartNew(() =>
            {
                var messageGenerator = new MessageGenerator();
                return messageGenerator.GenerateMessage("Erik", 5);
            });
            var result = task.Result; // This will block on its own, no need to call Wait
            Log<Program>.Info(result);
        }

        private static void DemoQueue()
        {
            Log<Program>.Debug("Writing message to the queue");
            WriteMessageToQueue("This is a queue message");

            Log<Program>.Debug("Getting message from the queue");
            var message = GetMessageFromQueue();
            Log<Program>.Info(message.Body.ToString());
        }

        private static void DemoQueueWithProcessor()
        {
            Log<Program>.Info("Writing message to the queue");
            WriteMessageToQueue("This is another queue message");
        }

        private static void WriteMessageToQueue(string message)
        {
            var queueManager = new QueueManager();
            queueManager.Send(message);
        }

        private static Message GetMessageFromQueue()
        {
            var queueManager = new QueueManager();
            return queueManager.Receive();
        }
    }
}
