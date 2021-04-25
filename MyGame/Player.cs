using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame 
{
    public class Player
    {
        private GameRoot game;
        private Texture2D texture;
        private Vector2 position;
        private Rectangle hitbox;
        private float speed;
        private float health;

        public Player(GameRoot game)
        {
            this.game = game;
            this.texture = this.game.content.Load<Texture2D>("sprites/player/TrollFace-64x64");
            this.position = new Vector2(100, 400);
            this.speed = 500.0f;
            this.health = 3.0f;
        } // Constructor sets all objects and values
        
        public void Update(GameTime gTime, KeyboardState kState)
        {
            this.hitbox = new Rectangle(0, 0, this.texture.Width, this.texture.Height);
            this.Movement(gTime, kState);
            this.BackFall(gTime, kState);
            this.Boundaries();
            if (this.health == 0) Environment.Exit(1);
        } // Runs code block 60 times per second

        public void Draw()
        {
            this.game.spriteBatch.Draw(this.texture, this.position, this.hitbox, Color.White);
        } // Draws ship on the screen

        private void Collosion()
        {
            
        }

        private void Boundaries()
        {
            if (this.position.X > this.game.graphics.PreferredBackBufferWidth - this.hitbox.Width)
                this.position.X = this.game.graphics.PreferredBackBufferWidth - this.hitbox.Width;

            if (this.position.X < 0) this.position.X = 0;
            
            if (this.position.Y > this.game.graphics.PreferredBackBufferHeight - this.hitbox.Height)
                this.position.Y = this.game.graphics.PreferredBackBufferHeight - this.hitbox.Height;
            
            if (this.position.Y < 0) this.position.Y = 0;
        } // Keeps ship within bounds

        private void Movement(GameTime gTime, KeyboardState kState)
        {
            float deltaTime = (float) gTime.ElapsedGameTime.TotalSeconds;

            if (kState.IsKeyDown(Keys.A)) this.position.X -= this.speed * deltaTime;
            
            if (kState.IsKeyDown(Keys.D)) this.position.X += this.speed * deltaTime;
           
            if (kState.IsKeyDown(Keys.W)) this.position.Y -= this.speed * deltaTime;
            
            if (kState.IsKeyDown(Keys.S)) this.position.Y += this.speed * deltaTime;

        } // Moves the ship object

        private void BackFall(GameTime gTime, KeyboardState kState)
        {
            float backFallSpeed = 50.0f;
            float deltaTime = (float) gTime.ElapsedGameTime.TotalSeconds;
            if (kState.IsKeyDown(Keys.D) || this.position.X < 50) return;
            this.position.X -= backFallSpeed * deltaTime;
        } // Pushes the ship back while it's idle
    }
}