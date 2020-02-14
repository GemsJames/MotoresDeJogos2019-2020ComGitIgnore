using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pipeline_test
{
    public static class ShipForm
    {
        private static ShipModel model;

        public static ShipModel Model
        {
            get { return model; }
            set { model = value; }
        }
        
        public static void Loadcontent(ShipModel model)
        {
            Model = model;
        }
    }
}
