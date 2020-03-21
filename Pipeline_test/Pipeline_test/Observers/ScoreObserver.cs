using AlienGrab;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Pipeline_test.Messages;
using Pipeline_test.Commands;

namespace Pipeline_test.Observers
{
    public class ScoreObserver : Observer
    {
        public override void OnNotify(float valor, ObserverActions.Action action)
        {
            ScoreManager.AddScore(valor);
        }
    }
}
