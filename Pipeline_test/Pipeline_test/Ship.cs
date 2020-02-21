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

        //public ShipModel Model
        //{
        //    get { return model; }
        //    set { model = value; }
        //}

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

        private float maxSpeed;

        public float MaxSpeed
        {
            get { return maxSpeed; }
            set { maxSpeed = value; }
        }

        public float scale;

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

        private Vector3 velocity;

        public Vector3 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        private Quaternion rotation;

        public Quaternion Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        private float yaw;

        public float Yaw
        {
            get { return yaw; }
            set { yaw = value; }
        }

        private float pitch;

        public float Pitch
        {
            get { return pitch; }
            set { pitch = value; }
        }

        private int roll;

        public int Roll
        {
            get { return roll; }
            set { roll = value; }
        }


        private Vector3 defaultSpeed;


        #endregion

        public Ship(ContentManager contentManager, float scale)
        {
            this.position = Vector3.Zero;
            this.world = Matrix.CreateTranslation(position);
            this.speed = 0;
            this.maxSpeed = 0;
            this.scale = 0;
            this.yaw = 0;
            this.pitch = 0;
            this.roll = 0;
            this.alive = false;

            //this.model = model;

            //foreach (ModelMesh mesh in this.model.Meshes)
            //{
            //    boundingSphere = BoundingSphere.CreateMerged(this.boundingSphere, mesh.BoundingSphere);
            //}
            //boundingSphere.Radius *= scale;
        }

        public Ship(Vector3 position, ContentManager contentManager, float speed, float scale, bool alive/*, ShipModel model*/)
        {
            this.position = position;
            this.world = Matrix.CreateTranslation(position);
            this.speed = speed;
            this.maxSpeed = 0;
            this.scale = scale;
            this.alive = alive;
            this.yaw = 0;
            this.pitch = 0;
            this.roll = 0;
            velocity = Vector3.Forward;
            defaultSpeed = new Vector3(0, 0, speed);

            foreach (ModelMesh mesh in ShipForm.Model.Model.Meshes)
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

            foreach (ModelMesh mesh in ShipForm.Model.Model.Meshes)
            {
                boundingSphere = BoundingSphere.CreateMerged(this.boundingSphere, mesh.BoundingSphere);
            }
            boundingSphere.Radius *= scale;
        }

        //public void LoadContent(ContentManager contentManager)
        //{
        //    model = contentManager.Load<Model>("p1_saucer");
        //}

        public void Update(GameTime gameTime)
        {
            //position.Z -= speed * gameTime.ElapsedGameTime.Milliseconds;

            //world = Matrix.CreateTranslation(position);

            //boundingSphere.Center = position;

            //if(position.Z <= -2000)
            //{
            //    ShipManager.ObliterateShip(this);
            //}
            velocity -= new Vector3(0, 0, 1);

            velocity.Normalize();
            velocity *= speed;

            rotation = Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll);
            position += Vector3.Transform(velocity, rotation);

            world = Matrix.CreateFromQuaternion(rotation) * Matrix.CreateTranslation(position);
            boundingSphere.Center = position;


        }

        public void Draw(Matrix View, Matrix Projection)
        {
            foreach (ModelMesh mesh in ShipForm.Model.Model.Meshes)
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
