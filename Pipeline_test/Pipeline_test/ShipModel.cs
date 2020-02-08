using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlienGrab;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Pipeline_test
{
    public  class ShipModel
    {
        private Model model;

        public Model Model
        {
            get { return model; }
            set { model = value; }
        }

        public ShipModel(ContentManager content, string modelString)
        {
            model = content.Load<Model>(modelString);
        }

    }
}
