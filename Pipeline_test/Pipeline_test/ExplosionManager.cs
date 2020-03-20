using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlienGrab;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pipeline_test.Messages;

namespace Pipeline_test
{
    public static class ExplosionManager
    {
        #region Variables

        private static List<Explosion> availableExplosions;

        private static string msg;

        public static string Msg
        {
            get { return msg; }
            set { msg = value; }
        }

        public static List<Explosion> AvailableExplosions
        {
            get { return availableExplosions; }
            set { availableExplosions = value; }
        }

        private static List<Explosion> busyExplosions;

        public static List<Explosion> BusyExplosions
        {
            get { return busyExplosions; }
            set { busyExplosions = value; }
        }

        private static List<Explosion> tempExplosions;

        private static int explosionNumber;

        #endregion

        public static void Initialize()
        {
            explosionNumber = 100;

            AvailableExplosions = new List<Explosion>(explosionNumber);
            BusyExplosions = new List<Explosion>(explosionNumber);
            tempExplosions = new List<Explosion>(explosionNumber);

        }

        public static void LoadContent(ContentManager content)
        {
            for (int i = 0; i < explosionNumber; i++)
            {
                AvailableExplosions.Add(new Explosion());
            }

            //SpawnExplosion(ShipManager.PlayerShip.Position);
        }

        public static void Update(GameTime gameTime)
        {

            foreach (Explosion Explosion in busyExplosions)
            {
                Explosion.Update(gameTime);
                if (!Explosion.Alive)
                {
                    tempExplosions.Add(Explosion);
                }
            }

            foreach (Explosion Explosion in tempExplosions)
            {
                availableExplosions.Add(Explosion);
                busyExplosions.Remove(Explosion);
            }
            tempExplosions.Clear();

        }

        public static void SpawnExplosion(Vector3 position)
        {
            if (availableExplosions.Count() > 0)
            {
                availableExplosions[0].SpawnExplosion(position, 0.02f, 1f);
                busyExplosions.Add(availableExplosions[0]);
                availableExplosions.Remove(availableExplosions[0]);
                MessageBus.InsertNewMessage(new ConsoleMessage("Spawned Explosion from available!"));
            }
            else
            {
                busyExplosions.Add(new Explosion(position, 0.02f, 1f, true));
                MessageBus.InsertNewMessage(new ConsoleMessage("Spawned Explosion from scratch!"));
            }
        }

        public static void ObliterateExplosion(Explosion Explosion)
        {
            Explosion.Alive = false;
        }

        public static void Draw()
        {
            foreach (Explosion Explosion in busyExplosions)
            {
                Explosion.Draw(Camera.View, Camera.Projection);
            }
        }

    }
}