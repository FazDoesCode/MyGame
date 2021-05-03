using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    public class ProjectileManager : List<Projectile>
    {
        private GameRoot game;
        private int castDelay;
        private double lastCast;
        private Player caster;

        public ProjectileManager(Player player, GameRoot game)
        {
            this.game = game;
            this.caster = player;
            this.castDelay = 1000;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var firebolt in this)
            {
                firebolt.Update(gameTime);
            }
            
            this.CastFirebolt(gameTime);
            this.Hit();
        }

        public void Draw()
        {
            foreach (var firebolt in this)
            {
                firebolt.Draw();
            }
        }

        private void CastFirebolt(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (!keyboardState.IsKeyDown(Keys.Space)) return; // Checks if player is pressing spacebar.
            
            if (gameTime.TotalGameTime.TotalMilliseconds > this.lastCast + this.castDelay)
            {
                this.Add(new Projectile(new Vector2(this.caster.position.X, this.caster.position.Y), this.game));
                this.lastCast = gameTime.TotalGameTime.TotalMilliseconds;
            }
        }

        private void Hit2()
        {
            List<Projectile> newProjectileList = new List<Projectile>();
            List<Impostor> newEnemyList = new List<Impostor>();
            
            newEnemyList.AddRange(this.game.EnemyManager);
            newProjectileList.AddRange(this);
            
            for (int i = 0; i < newEnemyList.Count - 1; i++)
            {
                for (int j = 0; j < newProjectileList.Count - 1; j++)
                {
                    if (this[i].hitbox.Intersects(this.game.EnemyManager[j].hitbox))
                    {
                        newEnemyList.Remove(this.game.EnemyManager[i]);
                        newProjectileList.Remove(this[j]);
                    }
                }
            }
            
            this.Clear();
            this.game.EnemyManager.Clear();
            this.AddRange(newProjectileList);
            this.game.EnemyManager.AddRange(newEnemyList);
        }
        
        private void Hit()
        {
            List<Projectile> newProjectileList = new List<Projectile>();
            List<Impostor> newEnemyList = new List<Impostor>();

            foreach (var firebolt in this)
            {
                newProjectileList.Add(firebolt);
            }

            foreach (var enemy in this.game.EnemyManager)
            {
                newEnemyList.Add(enemy);
            }
            
            foreach (var firebolt in this)
            {
                foreach (var enemy in this.game.EnemyManager)
                {
                    if (firebolt.hitbox.Intersects(enemy.hitbox))
                    {
                        newEnemyList.Remove(enemy);
                        newProjectileList.Remove(firebolt);
                    }
                }
            }

            this.game.player.projectileManager.Clear();
            this.game.player.projectileManager.AddRange(newProjectileList);
            this.game.EnemyManager.Clear();
            this.game.EnemyManager.AddRange(newEnemyList);
        } // Not proud of this one.
    }
}