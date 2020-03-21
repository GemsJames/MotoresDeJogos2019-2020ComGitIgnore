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
    
    interface IExplodable
    {
        Vector3 Position
        {
            get;
            set;
        }

        float ExplosionSize
        {
            get;
            set;
        }
    }
}
