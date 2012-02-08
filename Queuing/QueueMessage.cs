using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.Threading;

namespace Queuing
{
    public class QueueMessage
    {
        public void DoSomeWork()
        {
            Log<QueueMessage>.Info("Doing some work, then waiting a few seconds");
            Thread.Sleep(10000);
            Log<QueueMessage>.Info("Done doing some work, wrapping it up");
        }
    }
}
