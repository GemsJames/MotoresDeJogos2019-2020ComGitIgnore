using AlienGrab;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Pipeline_test.Messages;
using Pipeline_test.Commands;
using Microsoft.Xna.Framework.Content;

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

        private static SpriteFont font;

        public static SpriteFont Font
        {
            get { return font; }
            set { font = value; }
        }


        public static void Initialize()
        {
            score = 0;
        }

        public static void LoadContent(ContentManager content,string fontName)
        {
            font = content.Load<SpriteFont>(fontName);
        }

        public static void AddScore(float scoreToAdd)
        {
           score += scoreToAdd;
            MessageBus.InsertNewMessage(new ConsoleMessage("Score : " + score));
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(font, "Score :" + score, new Vector2(100, 100), Color.Black);

            spriteBatch.End();
        }

    }
}
