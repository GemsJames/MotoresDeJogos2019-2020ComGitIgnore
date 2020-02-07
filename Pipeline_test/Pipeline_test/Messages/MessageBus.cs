using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline_test.Messages
{
    public static class  MessageBus
    {
        private static List<Message> messages;

        public static List<Message> Messages
        {
            get { return messages; }
            set { messages = value; }
        }

        private static List<Message> messagesToReturn; 

        public static void Initialize()
        {
            messages = new List<Message>(100);

            messagesToReturn = new List<Message>(100);
        }

        public static void InsertNewMessage(Message message)
        {
            messages.Add(message);
        }

        public static void Update()
        {
           messagesToReturn.Clear();

           MessageBus.InsertNewMessage(new ConsoleMessage("Message number:" + messages.Count()));
        }

        public static List<Message> GetMessagesOfType(MessageType messageType)
        {
            messagesToReturn.Clear();

            foreach(Message message in messages)
            {
                if(message.messageType == messageType)
                {
                    messagesToReturn.Add(message);
                }
            }

            return messagesToReturn;
        }

        public static void RemoveMessage(Message messageToRemove)
        {
            messages.Remove(messageToRemove);
        }
    }
}
