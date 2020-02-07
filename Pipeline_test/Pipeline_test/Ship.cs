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
    public class Ship
    {
        #region Variables
        private Model model;

        public Model Model
        {
            get { return model; }
            set { model = value; }
        }

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

        private float speed;

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        private float scale;

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        private bool alive;

        public bool Alive
        {
            get { return alive; }
            set { alive = value; }
        }


        private BoundingSphere boundingSphere;

        public BoundingSphere BoundingSphere
        {
            get { return boundingSphere; }
            set { boundingSphere = value; }
        }

        #endregion

        public Ship(ContentManager contentManager)
        {
            this.position = Vector3.Zero;
            this.world = Matrix.CreateTranslation(position);
            this.speed = 0;
            this.scale = 0;
            this.alive = false;

            LoadContent(contentManager);

            //foreach (ModelMesh mesh in this.model.Meshes)
            //{
            //    boundingSphere = BoundingSphere.CreateMerged(this.boundingSphere, mesh.BoundingSphere);
            //}
            //boundingSphere.Radius *= scale;
        }

        public Ship(Vector3 position, ContentManager contentManager, float speed, float scale, bool alive)
        {
            this.position = position;
            this.world = Matrix.CreateTranslation(position);
            this.speed = speed;
            this.scale = scale;
            this.alive = alive;

            LoadContent(contentManager);

            foreach(ModelMesh mesh in this.model.Meshes)
            {
                boundingSphere = BoundingSphere.CreateMerged(this.boundingSphere, mesh.BoundingSphere);
            }
            boundingSphere.Radius *= scale;
        }

        public void SpawnShip(Vector3 position, float speed, float scale)
        {
            this.position = position;
            this.world = Matrix.CreateTranslation(position);
            this.speed = speed;
            this.scale = scale;
            this.alive = true;

            foreach (ModelMesh mesh in this.model.Meshes)
            {
                boundingSphere = BoundingSphere.CreateMerged(this.boundingSphere, mesh.BoundingSphere);
            }
            boundingSphere.Radius *= scale;
        }

        public void LoadContent(ContentManager contentManager)
        {
            model = contentManager.Load<Model>("p1_saucer");
        }

        public void Update(GameTime gameTime)
        {
            position.Z -= speed * gameTime.ElapsedGameTime.Milliseconds;

            world = Matrix.CreateTranslation(position);

            boundingSphere.Center = position;

            if(position.Z <= -2000)
            {
                ShipManager.ObliterateShip(this);
            }
        }

        public void Draw(Matrix View, Matrix Projection)
        {
            foreach (ModelMesh mesh in model.Meshes)
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

            DebugShapeRenderer.AddBoundingSphere(boundingSphere, Color.Red);
        }
    }
}
