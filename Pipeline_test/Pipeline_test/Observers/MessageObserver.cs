using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pipeline_test.Messages;

namespace Pipeline_test.Observers
{
    public class MessageObserver : Observer
    {
        public override void OnNotify(Ship ship, ObserverActions.Action action)
        {
            MessageBus.InsertNewMessage(new ConsoleMessage("esta ship colidiu " + ship));
        }
    }
}
