using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame 
{
    public class GameRoot : Game 
    {
        public static GraphicsDeviceManager graphics;
        public static ContentManager content;
        public static SpriteBatch spriteBatch;
        private SpriteBatch SpriteBatch;
        private Ship ship;
        private EnemyManager EnemyManager;

        public GameRoot() 
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            content = Content;
            IsMouseVisible = true;
            Window.Title = "KILL THE IMPOSTOR!";
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1200;
        }

        protected override void Initialize()
        {
            this.ship = new Ship();
            this.EnemyManager = new EnemyManager();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.SpriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatch = this.SpriteBatch;
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            if (kState.IsKeyDown(Keys.Escape)) Exit();

            this.ship.Update(gameTime, kState);
            this.EnemyManager.Update(gameTime, kState);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Purple);
            this.SpriteBatch.Begin();

            this.ship.Draw();
            this.EnemyManager.Draw();

            this.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}