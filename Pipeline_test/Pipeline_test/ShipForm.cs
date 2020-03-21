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
        private static Model model;

        public static Model Model
        {
            get { return model; }
            set { model = value; }
        }

        public static float scale;

        public static float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public static void Loadcontent(ContentManager content, string modelString, float scale)
        {
            Model = content.Load<Model>(modelString);

            Scale = scale;
        }
    }
}
