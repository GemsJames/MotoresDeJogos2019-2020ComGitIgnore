using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline_test
{
    class CollisionManager
    {
        public static void DetectCollisins(List<Ship> listA, List<Ship> listB)
        {
            foreach (Ship shipA in listA)
            {
                foreach (Ship shipB in listB)
                {
                    if (shipA.BoundingSphere.Intersects(shipB.BoundingSphere))
                    {

                    }
                }
            }
        }
    }
}
