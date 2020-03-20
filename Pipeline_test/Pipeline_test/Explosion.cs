using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlienGrab;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pipeline_test.Messages;

namespace Pipeline_test
{
    public class Explosion
    {
        #region Variables

        private Matrix world;

        public Matrix World
        {
            get { return world; }
            set { world = value; }
        }

        private Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        private bool alive;

        public bool Alive
        {
            get { return alive; }
            set { alive = value; }
        }

        private float speed;

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public float scale;

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        private float maxScale;

        public float MaxScale
        {
            get { return maxScale; }
            set { maxScale = value; }
        }

        #endregion

        public Explosion()
        {
            this.position = Vector3.Zero;
            this.world = Matrix.CreateTranslation(position);
            this.alive = false;
            this.scale = 0;
            this.speed = 0;
            this.maxScale = 1;
        }

        public Explosion(Vector3 position, float speed,float maxScale, bool alive)
        {
            this.position = position;
            this.world = Matrix.CreateTranslation(position);
            this.speed = speed;
            this.alive = alive;
            this.scale = 0;
            this.maxScale = maxScale;
        }

        public void SpawnExplosion(Vector3 position, float speed, float maxScale)
        {
            this.position = position;
            this.world = Matrix.CreateTranslation(position);
            this.speed = speed;
            this.alive = true;
            this.scale = 0;
            this.maxScale = maxScale;

        }

        public void Update(GameTime gameTime)
        {

            if(scale < maxScale)
                scale += speed /** (float)gameTime.ElapsedGameTime.Seconds*/;

            if (scale >= maxScale)
            {
                alive = false;
            }
        }

        public void Draw(Matrix View, Matrix Projection)
        {
            foreach (ModelMesh mesh in ExplosionForm.Model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.LightingEnabled = false;
                    effect.World = Matrix.CreateScale(scale) * World;
                    effect.View = View;
                    effect.Projection = Projection;
                }
                mesh.Draw();
            }
        }
    }
}