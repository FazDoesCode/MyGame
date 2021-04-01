using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame 
{
    public class GameRoot : Game 
    {
        public static GraphicsDeviceManager graphics;
        private SpriteBatch _spriteBatch;
        private List<Player> players;

        public GameRoot() 
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            players = new List<Player>();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            players.Add(new Player(new Vector2(100, 100), Content));
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            if (kState.IsKeyDown(Keys.Escape)) Exit();

            players[0].Update(kState, gameTime);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkRed);
            _spriteBatch.Begin();

            players[0].Draw(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}