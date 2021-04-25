using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame 
{
    public class GameRoot : Game
    {
        public GraphicsDeviceManager graphics;
        public ContentManager content;
        public SpriteBatch spriteBatch;
        private SpriteBatch SpriteBatch;
        private Player player;
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
            this.player = new Player(this);
            this.EnemyManager = new EnemyManager(this);
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

            this.player.Update(gameTime, kState);
            this.EnemyManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Purple);
            this.SpriteBatch.Begin();

            this.player.Draw();
            this.EnemyManager.Draw();

            this.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}