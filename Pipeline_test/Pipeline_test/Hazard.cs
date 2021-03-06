using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlienGrab;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pipeline_test.Observers;

namespace Pipeline_test
{
    public class Hazard : IExplodable, ICollidable
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

        private float speed;

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
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

        private float roll;

        public float Roll
        {
            get { return roll; }
            set { roll = value; }
        }

        private bool barrelRollin;

        public bool BarrelRollin
        {
            get { return barrelRollin; }
            set { barrelRollin = value; }
        }

        private float rollRandomMod;

        public float RollRandomMod
        {
            get { return rollRandomMod; }
            set { rollRandomMod = value; }
        }

        private float explosionSize;

        public float ExplosionSize
        {
            get { return explosionSize; }
            set { explosionSize = value; }
        }

        private Vector3 defaultSpeed;


        #endregion

        public List<Observer> observers;
        public ScoreObserver scoreObserver;


        public Hazard() //para usar c generics pq n deixa usar construtor com parametros la dentro...
        {
            this.position = Vector3.Zero;
            this.world = Matrix.CreateTranslation(position);
            this.speed = 0;
            this.yaw = 0;
            this.pitch = 0;
            this.roll = 0;
            this.alive = false;
            this.barrelRollin = false;
            this.rollRandomMod = 1;
            this.explosionSize = 0.5f;
            this.observers = new List<Observer>();
            this.scoreObserver = new ScoreObserver();
            AddObserver(scoreObserver);
        }

        public Hazard(float rollRandomMod)
        {
            this.position = Vector3.Zero;
            this.world = Matrix.CreateTranslation(position);
            this.speed = 0;
            this.yaw = 0;
            this.pitch = 0;
            this.roll = 0;
            this.alive = false;
            this.barrelRollin = false;
            this.rollRandomMod = rollRandomMod;
            this.explosionSize = 0.5f;
            this.observers = new List<Observer>();
            this.scoreObserver = new ScoreObserver();
            AddObserver(scoreObserver);
        }

        public Hazard(Vector3 position, float speed, bool alive, bool barrelRollin, float rollRandomMod)
        {
            this.position = position;
            this.world = Matrix.CreateTranslation(position);
            this.speed = speed;
            this.alive = alive;
            this.yaw = 0;
            this.pitch = 0;
            this.roll = 0;
            this.barrelRollin = barrelRollin;
            this.rollRandomMod = rollRandomMod;
            this.explosionSize = 0.5f;
            this.observers = new List<Observer>();
            this.scoreObserver = new ScoreObserver();
            AddObserver(scoreObserver);

            velocity = Vector3.Forward;
            defaultSpeed = new Vector3(0, 0, speed);

            foreach (ModelMesh mesh in HazardForm.Model.Meshes)
            {
                boundingSphere = BoundingSphere.CreateMerged(this.boundingSphere, mesh.BoundingSphere);
            }

            boundingSphere.Radius *= HazardForm.scale;
        }

        public void SpawnHazard(Vector3 position, float speed, float yaw, float pitch, float roll, bool barrelRollin)
        {
            this.position = position;
            this.world = Matrix.CreateTranslation(position);
            this.speed = speed;
            this.alive = true;
            this.barrelRollin = barrelRollin;

            this.pitch = pitch;
            this.yaw = yaw;
            this.roll = roll;
            this.speed = speed;

            foreach (ModelMesh mesh in HazardForm.Model.Meshes)
            {
                boundingSphere = BoundingSphere.CreateMerged(this.boundingSphere, mesh.BoundingSphere);
            }
            boundingSphere.Radius *= HazardForm.scale;
        }

        public void Explode()
        {
            ExplosionManager.SpawnExplosion(position, explosionSize);
            alive = false;
            Notify(10);
        }

        public void AddObserver(Observer observer)
        {
            observers.Add(observer);
        }

        public void Notify(float valor)
        {
            for (int i = 0; i < observers.Count; i++)
            {
                observers[i].OnNotify(valor, ObserverActions.Action.Explosion);
            }
        }

        public void Update(GameTime gameTime)
        {
            velocity -= new Vector3(0, 0, 1);

            velocity.Normalize();
            velocity *= speed;

            if (barrelRollin)
            {
                roll += GenericManager<Hazard>.AddRoll * rollRandomMod;
                //roll += HazardManager.AddRoll * rollRandomMod;
            }

            rotation = Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll);
            position += Vector3.Transform(velocity, rotation);

            world = Matrix.CreateFromQuaternion(rotation) * Matrix.CreateTranslation(position);
            boundingSphere.Center = position;
        }

        public void Draw(Matrix View, Matrix Projection)
        {
            foreach (ModelMesh mesh in HazardForm.Model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.LightingEnabled = false;
                    effect.World = Matrix.CreateScale(HazardForm.scale) * World;
                    effect.View = View;
                    effect.Projection = Projection;
                }
                mesh.Draw();
            }

            DebugShapeRenderer.AddBoundingSphere(boundingSphere, Color.Red);
        }
    }
}
