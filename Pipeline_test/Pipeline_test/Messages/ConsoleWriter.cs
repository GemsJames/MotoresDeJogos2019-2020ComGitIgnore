using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline_test.Messages
{
    class ConsoleWriter
    {
        private List<Message> messages;
        private ConsoleMessage consoleMessage;

        public ConsoleWriter()
        {
            messages = new List<Message>(100);
        }

        public void Update()
        {
            messages = MessageBus.GetMessagesOfType(MessageType.Console);
            foreach(Message message in messages)
            {
                consoleMessage = (ConsoleMessage)message;
                Console.WriteLine(consoleMessage.Message);

                MessageBus.RemoveMessage(message);
            }
            messages.Clear();
        }
    }
}
