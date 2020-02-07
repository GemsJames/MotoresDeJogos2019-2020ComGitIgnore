using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline_test
{
    public class Camera
    {
        private Matrix view;

        public Matrix View
        {
            get { return view; }
            set { view = value; }
        }

        private Matrix projection;

        public Matrix Projection
        {
            get { return projection; }
            set { projection = value; }
        }

        private Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Camera(Vector3 position, GraphicsDeviceManager graphics)
        {
            view = Matrix.CreateLookAt(position, Vector3.Forward, Vector3.Up);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(50f), graphics.PreferredBackBufferWidth / graphics.PreferredBackBufferHeight, 1, 5000);
        }
    }
}
