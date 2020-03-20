using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pipeline_test.Messages;

namespace Pipeline_test.Observers
{
    public class ExplosionObserver : Observer
    {
        public override void OnNotify(Ship ship, ObserverActions.Action action)
        {
            ship.Explode();
        }
    }
}