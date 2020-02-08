﻿using AlienGrab;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Pipeline_test.Messages;

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

        List<Ship> ships;
        ShipModel shipModel;

        Random random;
        
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
            ships = new List<Ship>();

            //Initialize Other Classes
            DebugShapeRenderer.Initialize(GraphicsDevice);
            MessageBus.Initialize();
            consoleWriter = new ConsoleWriter();
            ShipManager.Initialize();
            Camera.Initialize(GraphicsDevice);
            //StringConc.Initialize();
            //
           
            MessageBus.InsertNewMessage(new ConsoleMessage("Game Initiated!"));

            base.Initialize();
        }

        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);

            shipModel = new ShipModel(Content, "p1_saucer");

            ShipManager.LoadContent(Content, random, shipModel);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Update Classes
            MessageBus.Update();
            consoleWriter.Update();
            ShipManager.Update(gameTime, random, Content, shipModel);
            Camera.Update(gameTime, GraphicsDevice,ShipManager.PlayerShip);
            //

            //Memória


            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            ShipManager.Draw();

            DebugShapeRenderer.Draw(gameTime, Camera.View, Camera.Projection);

            base.Draw(gameTime);
        }
    }
}