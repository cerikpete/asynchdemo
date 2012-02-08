using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.Threading;

namespace MessageProcessor.Messages
{
    class ParallelMessage
    {
        private readonly string _message;
        private readonly int _sleepSeconds;

        public ParallelMessage(string message, int sleepSeconds)
        {
            _message = message;
            _sleepSeconds = sleepSeconds;
        }

        public void ProcessMessage()
        {
            Log<ParallelMessage>.Info("Processing message: " + _message);
            Thread.Sleep(_sleepSeconds * 1000);
            Log<ParallelMessage>.Info("Done processing message: " + _message);
        }
    }
}
