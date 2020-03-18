using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline_test
{
    [Serializable]
    public class Serialize 
    {
    
       public static void save(Ship ship1)    {
            //save
            FileStream stream = File.Create("saveFile");
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, ship1.Position);
            formatter.Serialize(stream, ship1.Pitch);
            formatter.Serialize(stream, ship1.Yaw);
            formatter.Serialize(stream, ship1.Roll);
            formatter.Serialize(stream, ShipManager.BusyShips);
            //foreach(Ship ship in ShipManager.BusyShips)
            //{

            //}
            stream.Close();
       }
    }

}
