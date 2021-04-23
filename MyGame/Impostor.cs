using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    public class Impostor
    {
        public Texture2D texture;
        public Vector2 position;
        private Rectangle hitbox;
        private float backfallSpeed;
        
        public Impostor(Vector2 position)
        {
            this.texture = GameRoot.content.Load<Texture2D>("sprites/enemies/among-us-red");
            this.position = position;
            this.backfallSpeed = 80.0f;
            this.hitbox = new Rectangle(0, 0, this.texture.Width, this.texture.Height);
            Console.Write("Created an impostor!\nPosition:\n     X: {0}\n     Y: {01}\n", this.position.X, this.position.Y);
        }

        public void Update(GameTime gTime, KeyboardState kState)
        {
            this.BackFall(gTime);
        }
        
        public void Draw()
        {
            GameRoot.spriteBatch.Draw(this.texture, this.position, this.hitbox, Color.White);
        } // Draws ship on the screen

        private void BackFall(GameTime gTime)
        {
            float deltaTime = (float) gTime.ElapsedGameTime.TotalSeconds;
            this.position.X -= this.backfallSpeed * deltaTime;
        }
    }
}