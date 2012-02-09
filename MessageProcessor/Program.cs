using System;
using System.Threading;
using System.Threading.Tasks;
using Core;
using MessageProcessor.Messages;
using Queuing;
using System.Messaging;
using System.Linq;
using log4net;
using System.Collections.Generic;

namespace MessageProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            LogManager.GetLogger(typeof (Program));

            Log<Program>.Debug("Entering the processor");

            ContinueWithDemo();

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

        private static void DemoQueueWithQueueMessage()
        {
            Log<Program>.Debug("Writing message to the queue");
            WriteMessageToQueue(new QueueMessage());
            Log<Program>.Debug("Getting coffee");
            Thread.Sleep(2500);
            Log<Program>.Debug("Reading a book");
            Thread.Sleep(3000);
            Log<Program>.Debug("That's it for this thread");
        }

        private static void WriteMessageToQueue(string message)
        {
            var queueManager = new QueueManager();
            queueManager.Send(message);
        }

        private static void WriteMessageToQueue(QueueMessage queueMessage)
        {
            var queueManager = new QueueManager();
            queueManager.Send(queueMessage);
        }

        private static Message GetMessageFromQueue()
        {
            var queueManager = new QueueManager();
            return queueManager.Receive();
        }

        private static void ParallelDemo()
        {
            var parallelMessage1 = new ParallelMessage("Message 1", 5);
            var parallelMessage2 = new ParallelMessage("Message 2", 1);
            var parallelMessage3 = new ParallelMessage("Message 3", 8);
            var messages = new List<ParallelMessage> { parallelMessage1, parallelMessage2, parallelMessage3 };
            messages.AsParallel().ForAll(m => m.ProcessMessage());
        }

        private static void PassingInExternalData()
        {
            var myText = "Some text";
            Task.Factory.StartNew((text) => new MessageWriter().WriteMessage(text.ToString(), 2), myText);
        }

        private static void ContinueWithDemo()
        {
            Task.Factory.StartNew(() =>
                {
                    Log<Program>.Debug("Entering first task");
                    var messageGenerator = new MessageGenerator();
                    string theMessage = messageGenerator.GenerateMessage("my original message (after sleeping 5 seconds)", 5);
                    new MessageWriter().WriteMessage(theMessage);
                    return theMessage;
                })
            .ContinueWith((s) =>
                {
                    Log<Program>.Debug("Entering second task");
                    var messageWriter = new MessageWriter();
                    messageWriter.WriteMessage(s.Result + " now appended with another message");
                });
        }
    }
}
