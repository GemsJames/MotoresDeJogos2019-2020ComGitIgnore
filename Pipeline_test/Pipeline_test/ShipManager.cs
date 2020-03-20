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
    public static class ShipManager
    {
        #region Variables

        private static List<Ship> availableShips;

        private static string msg; 

        public static string Msg 
        {
            get { return msg; }
            set { msg = value; }
        }

        public static List<Ship> AvailableShips
        {
            get { return availableShips; }
            set { availableShips = value; }
        }

        private static List<Ship> busyShips;

        public static List<Ship> BusyShips
        {
            get { return busyShips; }
            set { busyShips = value; }
        }

        private static Ship playerShip;

        public static Ship PlayerShip
        {
            get { return playerShip; }
            set { playerShip = value; }
        }

        private static float addSpeed;

        public static float AddSpeed
        {
            get { return addSpeed; }
            set { addSpeed = value; }
        }

        private static float addYaw;

        public static float AddYaw
        {
            get { return addYaw; }
            set { addYaw = value; }
        }

        private static float addPitch;

        public static float AddPitch
        {
            get { return addPitch; }
            set { addPitch = value; }
        }



        private static List<Ship> tempShips;

        private static double tempTimer;
        private static double spawnTime;

        private static int shipNumber;

        #endregion

        public static void Initialize()
        {
            shipNumber = 100;

            AvailableShips = new List<Ship>(shipNumber);
            BusyShips = new List<Ship>(shipNumber);
            tempShips = new List<Ship>(shipNumber);

            tempTimer = 0;
            spawnTime = 5.0f;
            addSpeed = 0.25f;
            addYaw = 0.025f;
            addPitch = 0.025f;

            msg = string.Format("Available ships: , {0}.  ,Busy Ships: {1} ", availableShips.Count.ToString(), busyShips.Count.ToString(), (busyShips.Count + availableShips.Count).ToString());

        }

        public static void LoadContent(ContentManager content, Random random)
        {
            for (int i = 0; i < shipNumber; i++)
            {
                AvailableShips.Add(new Ship(1));
            }

            playerShip = availableShips[random.Next(0,shipNumber)];

            playerShip.SpawnShip(new Vector3(random.Next(-1000, 1000), random.Next(-1000, 1000), random.Next(-1000, 1000)), 0f);
            busyShips.Add(playerShip);
            availableShips.Remove(playerShip);

            playerShip.MaxSpeed = 10f;

            for (int i = 0; i < shipNumber/2; i++)
            {
                SpawnShip(random, content);
            }

        }

        public static void Update(GameTime gameTime, Random random, ContentManager content)
        {
            tempTimer += gameTime.ElapsedGameTime.TotalSeconds;

            foreach (Ship ship in busyShips)
            {
                ship.Update(gameTime);
                if (!ship.Alive)
                {
                    tempShips.Add(ship);
                }
            }

            foreach(Ship ship in tempShips)
            {
                availableShips.Add(ship);
                busyShips.Remove(ship);
            }
            tempShips.Clear();


            //Memory stuff
            MemoryDebug.Update();            

            StringBuilder msg = new StringBuilder("Available ships: ");
            msg.Append(availableShips.Count.ToString());
            msg.Append(" ,Busy Ships: ");
            msg.Append(busyShips.Count.ToString());
            msg.Append(" Total: ");
            msg.Append((busyShips.Count + availableShips.Count).ToString());
            string finalMsg = msg.ToString();
        
            MessageBus.InsertNewMessage(new ConsoleMessage(finalMsg));
        }

        public static void SpawnShip(Random random, ContentManager content)
        {
            if(availableShips.Count() > 0)
            {
                availableShips[0].SpawnShip(new Vector3(random.Next(-1000, 1000), random.Next(-1000, 1000), random.Next(-1000, 1000)), (float)random.Next(1, 10) * 0.05f);
                busyShips.Add(availableShips[0]);
                availableShips.Remove(availableShips[0]);
            }
            else
            {
                busyShips.Add(new Ship(new Vector3(random.Next(-1000, 1000), random.Next(-1000, 1000), random.Next(-1000, 1000)), 0.5f, true));
                MessageBus.InsertNewMessage(new ConsoleMessage("Spawned ship from scratch!"));
            }
        }

        public static void ObliterateShip(Ship ship)
        {
            ship.Alive = false;
        }

        public static void Draw()
        {
            foreach (Ship ship in busyShips)
            {
                if (Camera.frustum.Intersects(ship.BoundingSphere))
                {
                    ship.Draw(Camera.View, Camera.Projection);
                }
            }

            playerShip.Draw(Camera.View, Camera.Projection);
        }

    }
}
