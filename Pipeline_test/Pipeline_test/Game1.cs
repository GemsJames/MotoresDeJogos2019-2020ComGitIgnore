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
            HazardManager.Initialize();
            Camera.Initialize(GraphicsDevice);
            skybox = new Skybox(Content);
            player1InputManager = new InputManager(Keys.Space, Keys.A, Keys.D, Keys.E, Keys.W, Keys.S, Keys.LeftShift);
            CollisionManager.Initialize();

            MessageBus.InsertNewMessage(new ConsoleMessage("Game Initiated!"));

            base.Initialize();
        }

        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);

            //shipModel = new ShipModel(Content, "p1_saucer");

            ShipForm.Loadcontent(Content, "p1_saucer", 0.01f);
            HazardForm.Loadcontent(Content, "p1_saucer", 0.005f, 20f);

            ShipManager.LoadContent(Content, random);
            HazardManager.LoadContent(Content, random);
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
            HazardManager.Update(gameTime, random, Content);
            Camera.Update(gameTime, GraphicsDevice,ShipManager.PlayerShip);
            CollisionManager.DetectCollisions(ShipManager.BusyShips,ShipManager.BusyShips);


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

            skybox.Draw(Camera.View, Camera.Projection, Camera.getPosition());

            ShipManager.Draw();
            HazardManager.Draw();

            //DebugShapeRenderer.Draw(gameTime, Camera.View, Camera.Projection);

            base.Draw(gameTime);
        }
    }
}