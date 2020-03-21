using AlienGrab;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Pipeline_test.Messages;
using Pipeline_test.Commands;

namespace Pipeline_test
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        ConsoleWriter consoleWriter;

        Skybox skybox;

        Random random;

        InputManager player1InputManager;
        List<Command> commands;

        List<ICollidable> collidables;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferMultiSampling = true;
            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 900;

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            random = new Random();

            //Initialize Other Classes
            DebugShapeRenderer.Initialize(GraphicsDevice);
            MessageBus.Initialize();
            consoleWriter = new ConsoleWriter();
            ShipManager.Initialize();

            //HazardManager.Initialize();
            GenericManager<Hazard>.Initialize();

            Camera.Initialize(GraphicsDevice);
            skybox = new Skybox(Content);
            player1InputManager = new InputManager(Keys.Space, Keys.A, Keys.D, Keys.E, Keys.W, Keys.S, Keys.LeftShift, Keys.F1, Keys.F2);
            CollisionManager.Initialize();
            collidables = new List<ICollidable>();

            ExplosionManager.Initialize();

            ScoreManager.Initialize();

            MessageBus.InsertNewMessage(new ConsoleMessage("Game Initiated!"));

            base.Initialize();
        }

        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);

            //shipModel = new ShipModel(Content, "p1_saucer");

            ShipForm.Loadcontent(Content, "p1_saucer", 0.01f);
            HazardForm.Loadcontent(Content, "missil", 0.1f, 40f);
            ExplosionForm.Loadcontent(Content, "explosion");

            ShipManager.LoadContent(Content, random);
            //HazardManager.LoadContent(Content, random);
            GenericManager<Hazard>.LoadContent(Content, random);
            ExplosionManager.LoadContent(Content);
            ScoreManager.LoadContent(Content, "font");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MessageBus.Update();
            consoleWriter.Update();
            ShipManager.Update(gameTime, random, Content);
            //HazardManager.Update(gameTime, random, Content);
            GenericManager<Hazard>.Update(gameTime, random, Content);
            Camera.Update(gameTime, GraphicsDevice,ShipManager.PlayerShip);

            ExplosionManager.Update(gameTime);

            //CollisionManager.DetectCollisions(ShipManager.BusyShips,ShipManager.BusyShips);
            //CollisionManager.DetectCollisions(ShipManager.BusyShips, GenericManager<Hazard>.BusyHazards);
            CollisionManager.collidablesTodos.Clear();

            CollisionManager.AddCollidableList(ShipManager.BusyShips);
            CollisionManager.AddCollidableList(GenericManager<Hazard>.BusyHazards);
            CollisionManager.DetectCollisions(CollisionManager.collidablesTodos, ShipManager.PlayerShip);


            commands = player1InputManager.UpdateInputManager();
            foreach(Command command in commands)
            {
                command.Execute(ShipManager.PlayerShip);
            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            ScoreManager.Draw(spriteBatch);
            skybox.Draw(Camera.View, Camera.Projection, Camera.getPosition());
            ShipManager.Draw();
            //HazardManager.Draw();
            GenericManager<Hazard>.Draw();
            ExplosionManager.Draw();

            //DebugShapeRenderer.Draw(gameTime, Camera.View, Camera.Projection);

            base.Draw(gameTime);
        }
    }
}