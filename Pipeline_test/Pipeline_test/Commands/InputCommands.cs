using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pipeline_test.Messages;

namespace Pipeline_test.Commands
{
    public class Accelarate : Command
    {
        public override void Execute(Ship ship)
        {

            MessageBus.InsertNewMessage(new ConsoleMessage("Accelarated"));

        }
    }

    public class TurnLeft : Command
    {
        public override void Execute(Ship ship)
        {

            MessageBus.InsertNewMessage(new ConsoleMessage("Left"));

        }
    }

    public class TurnRight : Command
    {
        public override void Execute(Ship ship)
        {

            MessageBus.InsertNewMessage(new ConsoleMessage("Right"));

        }
    }

    public class FireRocket : Command
    {
        public override void Execute(Ship ship)
        {

            MessageBus.InsertNewMessage(new ConsoleMessage("FIRE CARALHO"));

        }
    }
}
