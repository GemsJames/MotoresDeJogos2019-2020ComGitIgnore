using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline_test.Messages
{
    public class Message
    {
        private MessageType MessageType;

        public MessageType messageType
        {
            get { return MessageType; }
            set { MessageType = value; }
        }

        private bool read;

        public bool Read
        {
            get { return read; }
            set { read = value; }
        }

        public Message(MessageType messageType)
        {
            this.messageType = messageType;
            this.read = false;
        }
    }
}
