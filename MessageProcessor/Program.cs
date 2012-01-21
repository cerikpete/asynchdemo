using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Entering the processor");

            DemoTaskWhereTheOuputIsUsed();

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
            var result = task.Result;
            Console.WriteLine(result);
        }
    }
}
