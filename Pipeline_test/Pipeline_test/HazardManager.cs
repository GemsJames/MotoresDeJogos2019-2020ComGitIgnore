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
    public static class HazardManager
    {
        #region Variables

        private static string msg;

        public static string Msg
        {
            get { return msg; }
            set { msg = value; }
        }

        private static List<Hazard> availableHazards;

        public static List<Hazard> AvailableHazards
        {
            get { return availableHazards; }
            set { availableHazards = value; }
        }

        private static List<Hazard> busyHazards;

        public static List<Hazard> BusyHazards
        {
            get { return busyHazards; }
            set { busyHazards = value; }
        }

        // Might use these later
        //private static float addYaw;

        //public static float AddYaw
        //{
        //    get { return addYaw; }
        //    set { addYaw = value; }
        //}

        //private static float addPitch;

        //public static float AddPitch
        //{
        //    get { return addPitch; }
        //    set { addPitch = value; }
        //}

        private static float addRoll;

        public static float AddRoll
        {
            get { return addRoll; }
            set { addRoll = value; }
        }
        private static List<Hazard> tempHazards;

        private static int HazardNumber;

        #endregion

        public static void Initialize()
        {
            HazardNumber = 1000;

            AvailableHazards = new List<Hazard>(HazardNumber);
            BusyHazards = new List<Hazard>(HazardNumber);
            tempHazards = new List<Hazard>(HazardNumber);

            addRoll = 0.05f;

            msg = string.Format("Available Hazards: , {0}.  ,Busy Hazards: {1} ", availableHazards.Count.ToString(), busyHazards.Count.ToString(), (busyHazards.Count + availableHazards.Count).ToString());

        }

        public static void LoadContent(ContentManager content, Random random)
        {
            for (int i = 0; i < HazardNumber; i++)
            {
                AvailableHazards.Add(new Hazard(content));
            }
        }

        public static void Update(GameTime gameTime, Random random, ContentManager content)
        {
            foreach (Hazard Hazard in busyHazards)
            {
                Hazard.Update(gameTime);
                if (!Hazard.Alive)
                {
                    tempHazards.Add(Hazard);
                }
            }

            foreach (Hazard Hazard in tempHazards)
            {
                availableHazards.Add(Hazard);
                busyHazards.Remove(Hazard);
            }
            tempHazards.Clear();

            MemoryDebug.Update();

            StringBuilder msg = new StringBuilder("Available Hazards: ");
            msg.Append(availableHazards.Count.ToString());
            msg.Append(" ,Busy Hazards: ");
            msg.Append(busyHazards.Count.ToString());
            msg.Append(" Total: ");
            msg.Append((busyHazards.Count + availableHazards.Count).ToString());
            string finalMsg = msg.ToString();

            MessageBus.InsertNewMessage(new ConsoleMessage(finalMsg));
        }

        public static void ShootRocket(ContentManager content, Vector3 position, float speed, float yaw, float pitch, float roll)
        {
            if (availableHazards.Count() > 0)
            {
                availableHazards[0].SpawnHazard(position,speed,yaw,pitch,roll);
                busyHazards.Add(availableHazards[0]);
                availableHazards.Remove(availableHazards[0]);
            }
            else
            {
                busyHazards.Add(new Hazard(content));

                availableHazards[0].SpawnHazard(position, speed, yaw, pitch, roll);
                busyHazards.Add(availableHazards[0]);
                availableHazards.Remove(availableHazards[0]);
                MessageBus.InsertNewMessage(new ConsoleMessage("Spawned Hazard from scratch!"));
            }
        }



        public static void ObliterateHazard(Hazard Hazard)
        {
            Hazard.Alive = false;
        }

        public static void Draw()
        {
            foreach (Hazard Hazard in busyHazards)
            {
                Hazard.Draw(Camera.View, Camera.Projection);
            }
        }

    }
}
