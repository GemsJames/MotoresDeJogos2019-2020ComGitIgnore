using AlienGrab;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Pipeline_test.Messages;
using Pipeline_test.Commands;

namespace Pipeline_test
{
    public static class ScoreManager
    {
        private static float score;

        public static float Score
        {
            get { return score; }
            set { score = value; }
        }

        public static void Initialize()
        {
            score = 0;

        }

        public static void AddScore(float scoreToAdd)
        {
            score += scoreToAdd;
            MessageBus.InsertNewMessage(new ConsoleMessage("Score : " + score));
        }

    }
}
