using System;
using System.Threading;
using System.Threading.Tasks;
using MessageProcessor.Messages;
using Queuing;
using System.Messaging;

namespace MessageProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Entering the processor");

            SimpleDemoOfTasks();

            Console.WriteLine("End of Main method");
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
            Console.WriteLine(result);
        }

        private static void DemoQueue()
        {
            Console.WriteLine("Writing message to the queue");
            WriteMessageToQueue("This is a queue message");

            Console.WriteLine("Getting message from the queue");
            var message = GetMessageFromQueue();
            Console.WriteLine(message.Body.ToString());
        }

        private static void DemoQueueWithProcessor()
        {
            Console.WriteLine("Writing message to the queue");
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
