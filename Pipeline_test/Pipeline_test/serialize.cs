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
            
            SaveListShip(ShipManager.AvailableShips);
            SaveListShip(ShipManager.BusyShips);
            SaveShip(ShipManager.PlayerShip);
            SaveListHazard(GenericManager<Hazard>.AvailableHazards);
            SaveListHazard(GenericManager<Hazard>.BusyHazards);
        }

        public static void Load()
        {
            LoadListShip(ShipManager.AvailableShips);
            LoadListShip(ShipManager.BusyShips);
            LoadShip(ShipManager.PlayerShip);
            LoadListHazard(GenericManager<Hazard>.AvailableHazards);
            LoadListHazard(GenericManager<Hazard>.BusyHazards);
        }

        public static void SaveShip(Ship ship)
        {
            FileStream stream = File.Create("saveFile2.txt");
            System.Xml.Serialization.XmlSerializer formatter = new System.Xml.Serialization.XmlSerializer(typeof(Ship));
            formatter.Serialize(stream, ship);
            stream.Close();

        }

        public static void SaveListShip(List<Ship> shipList)
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
        public static void SaveListHazard(List<Hazard> hazardList)
        {

            FileStream stream = File.Create("saveFile3.txt");

            List<Hazard> auxList = new List<Hazard>();

            foreach (Hazard hazard in hazardList)
            {
                auxList.Add(hazard);
            }
            System.Xml.Serialization.XmlSerializer formatter = new System.Xml.Serialization.XmlSerializer(typeof(List<Hazard>));
            formatter.Serialize(stream, auxList);
            stream.Close();



        }

        public static void LoadShip(Ship ship)
        {
            FileStream stream = File.OpenRead("saveFile2.txt");
            System.Xml.Serialization.XmlSerializer disformatter = new System.Xml.Serialization.XmlSerializer(typeof(Ship));

            ship = (Ship)disformatter.Deserialize(stream);

            ShipManager.SpawnPlayer(ship.Position);
            stream.Close();
        }

        public static void LoadListShip(List<Ship> shipList)
        {
            
            
            FileStream stream = File.OpenRead("saveFile.txt");

           
            System.Xml.Serialization.XmlSerializer disformatter = new System.Xml.Serialization.XmlSerializer(typeof(List<Ship>));


            List<Ship> other = (List<Ship>)disformatter.Deserialize(stream);

           
            shipList.Clear();
            shipList.AddRange(other);
            stream.Close();
        }

        public static void LoadListHazard(List<Hazard> hazardList)
        {


            FileStream stream = File.OpenRead("saveFile3.txt");


            System.Xml.Serialization.XmlSerializer disformatter = new System.Xml.Serialization.XmlSerializer(typeof(List<Hazard>));


            List<Hazard> other = (List<Hazard>)disformatter.Deserialize(stream);


            hazardList.Clear();
            hazardList.AddRange(other);
            stream.Close();
        }

    }

}
