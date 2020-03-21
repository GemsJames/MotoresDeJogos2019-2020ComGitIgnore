using Pipeline_test.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline_test
{
  
    public class Serialize 
    {

        public static void Save()
        {
            SaveList(ShipManager.AvailableShips);
            SaveList(ShipManager.BusyShips);
        }

        public static void SaveList(List<Ship> shipList)
        {
            //save
           FileStream stream = File.Create("saveFile.txt");
            // var formatter = new BinaryFormatter();
            List<Ship> auxList = new List<Ship>();

            foreach (Ship ship in shipList)
            {
                auxList.Add(ship);
            }
            System.Xml.Serialization.XmlSerializer formatter = new System.Xml.Serialization.XmlSerializer(typeof(List<Ship>));
            formatter.Serialize(stream, auxList);
            stream.Close();


            //foreach (Ship ship in ShipManager.BusyShips)
            //{
            //    System.Xml.Serialization.XmlSerializer formatter = new System.Xml.Serialization.XmlSerializer(ship.GetType());
            //    formatter.Serialize(stream, ship);
            //}
         
       }
       public static void Load()
        {
            LoadList(ShipManager.AvailableShips);
            LoadList(ShipManager.BusyShips);
        }

       public static void LoadList(List<Ship> shipList)
        {
            
            MessageBus.InsertNewMessage(new ConsoleMessage("Loaded"));
            FileStream stream = File.OpenRead("saveFile.txt");

           
            System.Xml.Serialization.XmlSerializer disformatter = new System.Xml.Serialization.XmlSerializer(typeof(List<Ship>));


            List<Ship> other = (List<Ship>)disformatter.Deserialize(stream);

           
            shipList.Clear();
            shipList.AddRange(other);
            // newBusyShips = disformatter.Deserialize(stream);
            //ShipManager.BusyShips (disformatter.Deserialize(stream)

            stream.Close();
        }

    }

}
