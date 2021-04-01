using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    public class Impostor
    {
        private ContentManager _Content;
        private Texture2D _texture;
        private Vector2 _position;
        private Rectangle _hitbox;

        public Impostor(Vector2 position, ContentManager content)
        {
            this._Content = content;
            this._texture = _Content.Load<Texture2D>("sprites/enemies/among-us-red");
            this._position = position;
            this._hitbox = new Rectangle(0, 0, this._texture.Width, this._texture.Height);
            Console.Write("Created an impostor!\nPosition:\n     X: {0}\n     Y: {01}\n", this._position.X, this._position.Y);
        }

        public void Update(GameTime gTime, KeyboardState kState)
        {
            this.BackFall(gTime);
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this._texture, this._position, this._hitbox, Color.White);
        } // Draws ship on the screen

        private void BackFall(GameTime gTime)
        {
            float backFallSpeed = 50.0f;
            float deltaTime = (float) gTime.ElapsedGameTime.TotalSeconds;
            this._position.X -= backFallSpeed * deltaTime;
        }
    }
}