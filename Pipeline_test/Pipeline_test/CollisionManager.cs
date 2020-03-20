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
        public static ExplosionObserver explosionObserver = new ExplosionObserver();

        public static void Initialize()
        {
            AddObserver(messageObserver);
            AddObserver(explosionObserver);
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
                        if(shipA != ShipManager.PlayerShip)
                            Notify(shipA);
                        if (shipB != ShipManager.PlayerShip)
                            Notify(shipB);
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
                        Notify(shipA);
                        GenericManager<Hazard>.ObliterateHazard(hazard);
                    }
                }
            }
        }
    }
}
