using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame 
{
    public class Player
    {
        private ContentManager _Content;
        private Texture2D _texture;
        private Vector2 _position;
        private Vector2 _center;
        private float _speed = 250f;

        public Player(Vector2 position, ContentManager content)
        {
            this._Content = content;
            this._texture = _Content.Load<Texture2D>("sprites/player/idle");
            this._position = position;
            this._center.X = _texture.Width / 2f;
            this._center.Y = _texture.Height / 2f;
            Console.Write("Created a new player!\nPosition:\n     X: {0}\n     Y: {01}\n", position.X, position.Y);
        }

        public void Update(KeyboardState kState, GameTime gTime)
        {
            this.Boundaries();
            this.Movement(kState, gTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this._texture, this._position, new Rectangle(0, 0, 185, 137), Color.White);
        }

        private void Boundaries()
        {
            if (this._position.X > GameRoot.graphics.PreferredBackBufferWidth - 180)
            {
                this._position.X = GameRoot.graphics.PreferredBackBufferWidth - 180;
                return;
            }

            if (this._position.X < 0)
            {
                this._position.X = 0;
                return;
            }

            if (this._position.Y > GameRoot.graphics.PreferredBackBufferHeight - 137)
            {
                this._position.Y = GameRoot.graphics.PreferredBackBufferHeight - 137;
                return;
            }
                
            
            if (this._position.Y < 0)
                this._position.Y = 0;
        }

        private void Movement(KeyboardState kState, GameTime gTime)
        {
            float deltaTime = (float) gTime.ElapsedGameTime.TotalSeconds;

            if (kState.IsKeyDown(Keys.A))
            {
                this._position.X -= this._speed * deltaTime;
                return;
            }
            if (kState.IsKeyDown(Keys.D))
            {
                this._position.X += this._speed * deltaTime;
                return;
            }
            if (kState.IsKeyDown(Keys.W))
            {
                this._position.Y -= this._speed * deltaTime;
                return;
            }
            if (kState.IsKeyDown(Keys.S))
                this._position.Y += this._speed * deltaTime;
        }
    }
}