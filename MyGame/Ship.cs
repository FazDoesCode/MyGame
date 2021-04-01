using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame 
{
    public class Ship
    {
        private ContentManager _Content;
        private Texture2D _texture;
        private Vector2 _position;
        private Rectangle _hitbox;
        private float _speed;
        private float _health;

        public Ship(Vector2 position, ContentManager content)
        {
            this._Content = content;
            this._texture = _Content.Load<Texture2D>("sprites/player/sd-ship");
            this._position = position;
            this._hitbox = new Rectangle(0, 0, this._texture.Width, this._texture.Height);
            this._speed = 500.0f;
            this._health = 3.0f;
            Console.Write("Created a new player!\nHealth:\n     {0}hp\nPosition:\n     X: {01}\n     Y: {02}\n",this._health, this._position.X, this._position.Y);
        } // Constructor sets all objects and values

        public void Update(GameTime gTime, KeyboardState kState)
        {
            this.Movement(gTime, kState);
            this.BackFall(gTime, kState);
            this.Boundaries();
        } // Runs code block 60 times per second

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this._texture, this._position, this._hitbox, Color.White);
        } // Draws ship on the screen

        private void Boundaries()
        {
            if (this._position.X > GameRoot.graphics.PreferredBackBufferWidth - this._hitbox.Width)
                this._position.X = GameRoot.graphics.PreferredBackBufferWidth - this._hitbox.Width;

            if (this._position.X < 0) this._position.X = 0;
            
            if (this._position.Y > GameRoot.graphics.PreferredBackBufferHeight - this._hitbox.Height)
                this._position.Y = GameRoot.graphics.PreferredBackBufferHeight - this._hitbox.Height;
            
            if (this._position.Y < 0) this._position.Y = 0;
        } // Keeps ship within bounds

        private void Movement(GameTime gTime, KeyboardState kState)
        {
            float deltaTime = (float) gTime.ElapsedGameTime.TotalSeconds;

            if (kState.IsKeyDown(Keys.A)) this._position.X -= this._speed * deltaTime;
            
            if (kState.IsKeyDown(Keys.D)) this._position.X += this._speed * deltaTime;
           
            if (kState.IsKeyDown(Keys.W)) this._position.Y -= this._speed * deltaTime;
            
            if (kState.IsKeyDown(Keys.S)) this._position.Y += this._speed * deltaTime;

        } // Moves the ship object

        private void BackFall(GameTime gTime, KeyboardState kState)
        {
            float backFallSpeed = 50.0f;
            float deltaTime = (float) gTime.ElapsedGameTime.TotalSeconds;
            if (kState.IsKeyDown(Keys.D) || this._position.X < 50) return;
            this._position.X -= backFallSpeed * deltaTime;
        } // Pushes the ship back while it's idle
    }
}