using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline_test.Observers
{
    public abstract class Observer
    {
        public abstract void OnNotify(float valor, ObserverActions.Action action);
    }
}
