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

        private static List<Ship> tempShips;

        private static double tempTimer;
        private static double spawnTime;

        private static int shipNumber;

        #endregion

        public static void Initialize()
        {
            shipNumber = 1000;

            AvailableShips = new List<Ship>(shipNumber);
            BusyShips = new List<Ship>(shipNumber);
            tempShips = new List<Ship>(shipNumber);

            tempTimer = 0;
            spawnTime = 0.01f;

            msg = string.Format("Available ships: , {0}.  ,Busy Ships: {1} ", availableShips.Count.ToString(), busyShips.Count.ToString(), (busyShips.Count + availableShips.Count).ToString());

        }

        public static void LoadContent(ContentManager content)
        {
            for (int i = 0; i < shipNumber; i++)
            {
                AvailableShips.Add(new Ship(content));
            }
        }

        //AvailableShips.Add(new Ship(new Vector3(random.Next(-1000, 1000), random.Next(-1000, 1000), random.Next(-1000, 1000)), content, 0.5f, 0.01f));

        public static void Update(GameTime gameTime, Random random, ContentManager content)
        {
            tempTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (tempTimer >= spawnTime)
            {
                tempTimer -= spawnTime;
                SpawnShip(random, content);
            }

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

            MemoryDebug.Update();

            
            //MessageBus.InsertNewMessage(new ConsoleMessage("Available ships: " + availableShips.Count + " ,Busy Ships: " + busyShips.Count + " Total: " + (busyShips.Count + availableShips.Count)));
            

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
                availableShips[0].SpawnShip(new Vector3(random.Next(-1000, 1000), random.Next(-1000, 1000), random.Next(-1000, 1000)), (float)random.Next(1, 10) * 0.05f, 0.01f);
                busyShips.Add(availableShips[0]);
                availableShips.Remove(availableShips[0]);
            }
            else
            {
                busyShips.Add(new Ship(new Vector3(random.Next(-1000, 1000), random.Next(-1000, 1000), random.Next(-1000, 1000)), content, 0.5f, 0.01f, true));
                MessageBus.InsertNewMessage(new ConsoleMessage("Spawned ship from scratch!"));
            }
        }

        public static void ObliterateShip(Ship ship)
        {
            ship.Alive = false;
        }

        public static void Draw(Camera camera)
        {
            foreach (Ship ship in busyShips)
            {
                ship.Draw(camera.View, camera.Projection);
            }
        }

    }
}
