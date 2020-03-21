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
        public override void OnNotify(float valor, ObserverActions.Action action)
        {
            MessageBus.InsertNewMessage(new ConsoleMessage("coisas e tal" + valor));
        }
    }
}
