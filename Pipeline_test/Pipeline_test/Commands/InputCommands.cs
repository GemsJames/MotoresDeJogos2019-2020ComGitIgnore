using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Pipeline_test.Messages;

namespace Pipeline_test.Commands
{
    
    public class Save : Command
    {
        public override void Execute(Ship ship)
        {
            MessageBus.InsertNewMessage(new ConsoleMessage("Saved"));
            Serialize.save(ship);

           
        }
    }

    public class Load : Command
    {
        public override void Execute(Ship ship)
        {

            Serialize.load(ship);
        }
    }


    public class Accelarate : Command
    {
        public override void Execute(Ship ship)
        {

            ship.Speed += ShipManager.AddSpeed;


            if(ship.Speed >= ship.MaxSpeed)
            {
                ship.Speed = ship.MaxSpeed;
            }

            MessageBus.InsertNewMessage(new ConsoleMessage("Accelarated"));

        }
    }

    public class DeAccelarate : Command
    {
        public override void Execute(Ship ship)
        {

            ship.Speed -= ShipManager.AddSpeed;


            if (ship.Speed <= 0)
            {
                ship.Speed = 0;
            }

        }
    }

    public class TurnLeft : Command
    {
        public override void Execute(Ship ship)
        {

            ship.Yaw += ShipManager.AddYaw;

            MessageBus.InsertNewMessage(new ConsoleMessage("Left"));

        }
    }

    public class TurnRight : Command
    {
        public override void Execute(Ship ship)
        {

            ship.Yaw -= ShipManager.AddYaw;

            MessageBus.InsertNewMessage(new ConsoleMessage("Right"));

        }
    }

    public class FaceDown : Command
    {
        public override void Execute(Ship ship)
        {
            ship.Pitch -= ShipManager.AddPitch;

            MessageBus.InsertNewMessage(new ConsoleMessage("Face DOWN"));

        }
    }

    public class FaceUp : Command
    {
        public override void Execute(Ship ship)
        {

            ship.Pitch += ShipManager.AddPitch;

            MessageBus.InsertNewMessage(new ConsoleMessage("Face Up"));

        }
    }

    public class FireRocket : Command
    {
        public override void Execute(Ship ship)
        {

            MessageBus.InsertNewMessage(new ConsoleMessage("FIRE "));

            GenericManager<Hazard>.ShootRocket(ship.Position, HazardForm.Speed, ship.Yaw, ship.Pitch, ship.Roll);

        }
    }
}
