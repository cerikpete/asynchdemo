using System.Threading;
using Core;

namespace MessageProcessor.Messages
{
    class MessageWriter
    {
        public void WriteMessage(string message, int sleepSeconds = 0)
        {
            if (sleepSeconds > 0)
                Thread.Sleep(sleepSeconds * 1000);

            Log<MessageWriter>.Info(message);
        }
    }
}
