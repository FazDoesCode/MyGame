﻿using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame 
{
    public class Player
    {
        private GameRoot game;
        public Texture2D texture;
        public Texture2D hurt1;
        public Texture2D hurt2;
        public Vector2 position;
        public ProjectileManager projectileManager;
        private Rectangle hitbox;
        private float speed;
        private float health;
        private SoundEffect hurtSound;

        public Player(GameRoot game)
        {
            this.game = game;
            this.texture = this.game.content.Load<Texture2D>("sprites/player/TrollFace-64x64");
            this.hurt1 = this.game.content.Load<Texture2D>("sprites/player/Trollfacehurt1");
            this.hurt2 = this.game.content.Load<Texture2D>("sprites/player/Trollfacehurt2");
            this.position = new Vector2(100, 400);
            this.projectileManager = new ProjectileManager(this, this.game);
            this.speed = 500.0f;
            this.health = 3.0f;
            hurtSound = this.game.content.Load<SoundEffect>("sounds/sound effects/FUCK");
        } // Constructor sets all objects and values
        
        public void Update(GameTime gTime, KeyboardState kState)
        {
            this.hitbox = new Rectangle((int) this.position.X, (int) this.position.Y, this.texture.Width, this.texture.Height); // Sets a new hitbox 60 times per second.
            this.Movement(gTime, kState); // Calls the movement method.
            this.BackFall(gTime, kState); // Calls the backfall method.
            this.projectileManager.Update(gTime); // Calls the projectile manager's update method.
            this.Boundaries(); // Calls the boundaries method.
            this.Collision(); // Calls the collision function.
        } // Runs code block 60 times per second.

        public void Draw()
        {
            if (this.health == 3)
            {
                this.game.spriteBatch.Draw(this.texture, this.hitbox, Color.White); // Draws this object on the screen.
            }
            if (this.health == 2)
            {
                this.game.spriteBatch.Draw(this.hurt1, this.hitbox, Color.White);
            }
            if (this.health == 1)
            {
                this.game.spriteBatch.Draw(this.hurt2, this.hitbox, Color.White);
            }
            this.projectileManager.Draw(); // Calls the projectile manager's drawing method.
        } // Draws ship on the screen

        private void Collision()
        {
            if (this.health == 0)
            {
                Environment.Exit(1);
            }
            for (int i = 0; i < this.game.EnemyManager.Count; i++)
            {
                if (this.hitbox.Intersects(this.game.EnemyManager[i].hitbox))
                {
                    this.health--;
                    hurtSound.Play();
                    Console.WriteLine("Deducted health");
                    this.game.EnemyManager.Remove(this.game.EnemyManager[i]);
                }
            }
        } // Loops through the enemy manager and checks if the player collides with an enemy.

        private void Boundaries()
        {
            if (this.position.X > this.game.graphics.PreferredBackBufferWidth - this.hitbox.Width)
            {
                this.position.X = this.game.graphics.PreferredBackBufferWidth - this.hitbox.Width;
            }

            if (this.position.X < 0)
            {
                this.position.X = 0;
            }

            if (this.position.Y > this.game.graphics.PreferredBackBufferHeight - this.hitbox.Height)
            {
                this.position.Y = this.game.graphics.PreferredBackBufferHeight - this.hitbox.Height;
            }

            if (this.position.Y < 0)
            {
                this.position.Y = 0;
            }
        } // Keeps player within bounds.

        private void Movement(GameTime gTime, KeyboardState kState)
        {
            float deltaTime = (float) gTime.ElapsedGameTime.TotalSeconds;

            if (kState.IsKeyDown(Keys.A))
            {
                this.position.X -= this.speed * deltaTime;
            }

            if (kState.IsKeyDown(Keys.D))
            {
                this.position.X += this.speed * deltaTime;
            }

            if (kState.IsKeyDown(Keys.W))
            {
                this.position.Y -= this.speed * deltaTime;
            }

            if (kState.IsKeyDown(Keys.S))
            {
                this.position.Y += this.speed * deltaTime;
            }

        } // Moves the ship object

        private void BackFall(GameTime gTime, KeyboardState kState)
        {
            float backFallSpeed = 50.0f;
            float deltaTime = (float) gTime.ElapsedGameTime.TotalSeconds;
            if (kState.IsKeyDown(Keys.D) || this.position.X < 50)
            {
                return;
            }
            this.position.X -= backFallSpeed * deltaTime;
        } // Pushes the ship back while it's idle
    }
}