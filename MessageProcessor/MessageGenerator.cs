﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MessageProcessor
{
    class MessageGenerator
    {
        public string GenerateMessage(string messageSuffix, int sleepSeconds = 0)
        {
            if (sleepSeconds > 0)
                Thread.Sleep(sleepSeconds * 1000);
         
            return "Here's the message: " + messageSuffix;
        }
    }
}
