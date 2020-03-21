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
            SaveShip(ShipManager.PlayerShip);
        }

        public static void SaveShip(Ship ship)
        {
            FileStream stream = File.Create("saveFile2.txt");
            System.Xml.Serialization.XmlSerializer formatter = new System.Xml.Serialization.XmlSerializer(typeof(Ship));
            formatter.Serialize(stream, ship);
            stream.Close();
        }

        public static void SaveList(List<Ship> shipList)
        {
      
            FileStream stream = File.Create("saveFile.txt");

            List<Ship> auxList = new List<Ship>();

            foreach (Ship ship in shipList)
            {
                auxList.Add(ship);
            }
            System.Xml.Serialization.XmlSerializer formatter = new System.Xml.Serialization.XmlSerializer(typeof(List<Ship>));
            formatter.Serialize(stream, auxList);
            stream.Close();


         
        }
       public static void Load()
       {
            LoadList(ShipManager.AvailableShips);
            LoadList(ShipManager.BusyShips);
            LoadShip(ShipManager.PlayerShip);
       }

        public static void LoadShip(Ship ship)
        {
            FileStream stream = File.OpenRead("saveFile2.txt");
            System.Xml.Serialization.XmlSerializer disformatter = new System.Xml.Serialization.XmlSerializer(typeof(Ship));

            ship = (Ship)disformatter.Deserialize(stream);

            ShipManager.SpawnPlayer();
            stream.Close();
        }

        public static void LoadList(List<Ship> shipList)
        {
            
            
            FileStream stream = File.OpenRead("saveFile.txt");

           
            System.Xml.Serialization.XmlSerializer disformatter = new System.Xml.Serialization.XmlSerializer(typeof(List<Ship>));


            List<Ship> other = (List<Ship>)disformatter.Deserialize(stream);

           
            shipList.Clear();
            shipList.AddRange(other);
            stream.Close();
        }

    }

}
