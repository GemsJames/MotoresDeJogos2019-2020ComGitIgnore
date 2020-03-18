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
    public class GenericManager<T> where T :  Hazard, new()
    {
        #region Variables

        private static string msg;

        public static string Msg
        {
            get { return msg; }
            set { msg = value; }
        }

        private static List<T> availableHazards;

        public static List<T> AvailableHazards
        {
            get { return availableHazards; }
            set { availableHazards = value; }
        }

        private static List<T> busyHazards;

        public static List<T> BusyHazards
        {
            get { return busyHazards; }
            set { busyHazards = value; }
        }

        private static ContentManager customContent;

        public static ContentManager CustomContent
        {
            get { return customContent; }
            set { customContent = value; }
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
        private static List<T> tempHazards;

        private static int HazardNumber;

        #endregion

        public static void Initialize()
        {
            HazardNumber = 1000;

            AvailableHazards = new List<T>(HazardNumber);
            BusyHazards = new List<T>(HazardNumber);
            tempHazards = new List<T>(HazardNumber);
            
            addRoll = 0.25f;

            msg = string.Format("Available Hazards: , {0}.  ,Busy Hazards: {1} ", availableHazards.Count.ToString(), busyHazards.Count.ToString(), (busyHazards.Count + availableHazards.Count).ToString());

        }

        public static void LoadContent(ContentManager content, Random random)
        {
            customContent = content;
           
            for (int i = 0; i < HazardNumber; i++)
            {
                //AvailableHazards.Add(new T(content, (float)(random.NextDouble() - 0.5f) * 2));
                availableHazards.Add(new T());
            }
        }

        public static void Update(GameTime gameTime, Random random, ContentManager content)
        {
            foreach (T Hazard in busyHazards)
            {
                Hazard.Update(gameTime);
                if (!Hazard.Alive)
                {
                    tempHazards.Add(Hazard);
                }
            }

            foreach (T Hazard in tempHazards)
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

        public static void ShootRocket(Vector3 position, float speed, float yaw, float pitch, float roll)
        {
            if (availableHazards.Count() > 0)
            {
                availableHazards[0].SpawnHazard(position, speed, yaw, pitch, roll, true);
                busyHazards.Add(availableHazards[0]);
                availableHazards.Remove(availableHazards[0]);
            }
            else
            {
                //busyHazards.Add(new T(customContent));
                busyHazards.Add(new T());

                availableHazards[0].SpawnHazard(position, speed, yaw, pitch, roll, true);
                busyHazards.Add(availableHazards[0]);
                availableHazards.Remove(availableHazards[0]);
                MessageBus.InsertNewMessage(new ConsoleMessage("Spawned Hazard from scratch!"));
            }
        }



        public static void ObliterateHazard(T Hazard)
        {
            Hazard.Alive = false;
        }

        public static void Draw()
        {
            foreach (T hazard in busyHazards)
            {
                if (Camera.frustum.Intersects(hazard.BoundingSphere))
                {
                    hazard.Draw(Camera.View, Camera.Projection);
                }
            }
        }

    }
}
