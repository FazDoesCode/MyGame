﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace MyGame
{
    public class ProjectileManager : List<Projectile>
    {
        private GameRoot game;
        private int castDelay;
        private double lastCast;
        private Player caster;
        private SoundEffect firesound;
        private SoundEffect AMOGUS;

        public ProjectileManager(Player player, GameRoot game)
        {
            this.game = game;
            this.caster = player;
            this.castDelay = 500;
            firesound = this.game.content.Load<SoundEffect>("sounds/sound effects/firesound");
            AMOGUS = this.game.content.Load<SoundEffect>("sounds/sound effects/AMOGUS");
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i].Update(gameTime);
            }
            
            this.CastFirebolt(gameTime);
            this.Hit();
        }

        public void Draw()
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i].Draw();
            }
        }

        private void CastFirebolt(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (!keyboardState.IsKeyDown(Keys.Space)) 
            {
                return; 
            } // Checks if player is pressing spacebar.
            
            if (gameTime.TotalGameTime.TotalMilliseconds > this.lastCast + this.castDelay)
            {
                this.Add(new Projectile(new Vector2(this.caster.position.X, this.caster.position.Y + (this.caster.texture.Height / 2)), this.game));
                this.lastCast = gameTime.TotalGameTime.TotalMilliseconds;
                firesound.Play(0.2f, 0,0);
            }
        }

        private void Hit()
        {
            List<Projectile> newProjectileList = new List<Projectile>(this.game.player.projectileManager);
            List<Impostor> newEnemyList = new List<Impostor>(this.game.EnemyManager);

            foreach (var firebolt in this)
            {
                foreach (var enemy in this.game.EnemyManager)
                {
                    if (firebolt.hitbox.Intersects(enemy.hitbox))
                    {
                        AMOGUS.Play(0.2f, 0, 0);
                        newEnemyList.Remove(enemy);
                        newProjectileList.Remove(firebolt);
                    }
                }
            }

            this.game.player.projectileManager.Clear();
            this.game.EnemyManager.Clear();
            this.game.player.projectileManager.AddRange(newProjectileList);
            this.game.EnemyManager.AddRange(newEnemyList);
        }

        public void SetCastDelay(int newDelay)
        {
            this.castDelay = newDelay;
        }
    }
}