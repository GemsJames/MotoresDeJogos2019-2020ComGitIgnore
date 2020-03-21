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
    public class Ship : IExplodable, ICollidable
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

        private float explosionSize;

        public float ExplosionSize
        {
            get { return explosionSize; }
            set { explosionSize = value; }
        }

        private Vector3 defaultSpeed;


        #endregion

        public Ship(float scale)
        {
            this.position = Vector3.Zero;
            this.world = Matrix.CreateTranslation(position);
            this.speed = 0;
            this.maxSpeed = 0;
            this.yaw = 0;
            this.pitch = 0;
            this.roll = 0;
            this.alive = false;
            this.explosionSize = 1;
        }

        public Ship(Vector3 position, float speed, bool alive)
        {
            this.position = position;
            this.world = Matrix.CreateTranslation(position);
            this.speed = speed;
            this.maxSpeed = 0;
            this.alive = alive;
            this.yaw = 0;
            this.pitch = 0;
            this.roll = 0;
            velocity = Vector3.Forward;
            defaultSpeed = new Vector3(0, 0, speed);
            this.explosionSize = 1;

            foreach (ModelMesh mesh in ShipForm.Model.Meshes)
            {
                boundingSphere = BoundingSphere.CreateMerged(this.boundingSphere, mesh.BoundingSphere);
            }
            boundingSphere.Radius *= ShipForm.scale;
        }

        public void SpawnShip(Vector3 position, float speed)
        {
            this.position = position;
            this.world = Matrix.CreateTranslation(position);
            this.speed = speed;
            this.alive = true;
            this.explosionSize = 1;

            foreach (ModelMesh mesh in ShipForm.Model.Meshes)
            {
                boundingSphere = BoundingSphere.CreateMerged(this.boundingSphere, mesh.BoundingSphere);
            }
            boundingSphere.Radius *= ShipForm.scale;
        }

        public void Update(GameTime gameTime)
        {

            velocity -= new Vector3(0, 0, 1);

            velocity.Normalize();
            velocity *= speed;

            rotation = Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll);
            position += Vector3.Transform(velocity, rotation);

            world = Matrix.CreateFromQuaternion(rotation) * Matrix.CreateTranslation(position);
            boundingSphere.Center = position;


        }

        public void Explode()
        {
            ExplosionManager.SpawnExplosion(position,explosionSize);
            alive = false;
        }

        public void Draw(Matrix View, Matrix Projection)
        {
            foreach (ModelMesh mesh in ShipForm.Model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.LightingEnabled = false;
                    effect.World = Matrix.CreateScale(ShipForm.scale) * World;
                    effect.View = View;
                    effect.Projection = Projection;
                }
                mesh.Draw();
            }

            DebugShapeRenderer.AddBoundingSphere(boundingSphere, Color.Red);
        }
    }
}
