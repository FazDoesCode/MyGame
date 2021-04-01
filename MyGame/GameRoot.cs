using System;
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
        private List<Ship> _ships;
        private List<Impostor> _impostors;

        public GameRoot() 
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.Title = "KILL THE IMPOSTOR!";
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1200;
        }

        protected override void Initialize()
        {
            this._ships = new List<Ship>();
            this._impostors = new List<Impostor>();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this._spriteBatch = new SpriteBatch(GraphicsDevice);

            this._ships.Add(new Ship(new Vector2(100, 400), Content));

            Random random = new Random();
            for (int i = 0; i <= 8; i++)
            {
                this._impostors.Add(new Impostor(new Vector2(random.Next(200, Window.ClientBounds.Width - 64), random.Next(64, Window.ClientBounds.Height - 64)), Content));
            }
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            if (kState.IsKeyDown(Keys.Escape)) Exit();

            this._ships[0].Update(gameTime, kState);
            
            foreach (var impostor in this._impostors)
            {
                impostor.Update(gameTime, kState);
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Purple);
            this._spriteBatch.Begin();

            this._ships[0].Draw(this._spriteBatch);
            
            foreach (var impostor in this._impostors)
            {
                impostor.Draw(this._spriteBatch);
            }

            this._spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}