using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline_test.Messages
{
    class ConsoleMessage : Message
    {
        private string message;
       
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public ConsoleMessage(string message) : base(MessageType.Console)
        {
            this.message = message;
        }

      
    }
}
