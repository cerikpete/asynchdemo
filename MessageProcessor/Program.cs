using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageProcessor.Messages;
using MessageProcessor.Queuing;
using System.Messaging;

namespace MessageProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Entering the processor");

            DemoQueue();

            Console.WriteLine("Completed all of the tasks");
            Console.ReadKey();
        }

        private static void SpinUpDemoTasks()
        {
            var task1 = Task.Factory.StartNew(() =>
            {
                var writer = new MessageWriter();
                writer.WriteMessage("This is the first message", 5);
            });

            var task2 = Task.Factory.StartNew(() =>
            {
                var writer = new MessageWriter();
                writer.WriteMessage("This is the second message");
            });

            Task.WaitAll(task1, task2);
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
