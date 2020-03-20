using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pipeline_test.Observers;

namespace Pipeline_test
{
    public static class CollisionManager
    {

        public static List<Observer> observers = new List<Observer>();

        public static MessageObserver messageObserver = new MessageObserver();

        public static void Initialize()
        {
            AddObserver(messageObserver);
        }

        public static void AddObserver(Observer observer)
        {
            observers.Add(observer);
        }

        public static void Notify(Ship ship)
        {
            for (int i = 0; i < observers.Count; i++)
            {
                observers[i].OnNotify(ship, ObserverActions.Action.Explosion);
            }
        }

        public static void DetectCollisions(List<Ship> listA, List<Ship> listB)
        {
            foreach (Ship shipA in listA)
            {
                foreach (Ship shipB in listB)
                {
                    if (shipA.BoundingSphere.Intersects(shipB.BoundingSphere) && shipA != shipB)
                    {
                        Notify(shipA);
                        Notify(shipB);
                        ShipManager.ObliterateShip(shipA);
                        ShipManager.ObliterateShip(shipB);
                    }
                }
            }
        }

        public static void DetectCollisions(List<Ship> listA, List<Hazard> listB)
        {
            foreach (Ship shipA in listA)
            {
                foreach (Hazard hazard in listB)
                {
                    if (shipA.BoundingSphere.Intersects(hazard.BoundingSphere) && shipA != ShipManager.PlayerShip)
                    {
                        ExplosionManager.SpawnExplosion(shipA.Position);
                        Notify(shipA);
                        ShipManager.ObliterateShip(shipA);
                        GenericManager<Hazard>.ObliterateHazard(hazard);
                    }
                }
            }
        }
    }
}
