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

        public void CastFirebolt(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (!keyboardState.IsKeyDown(Keys.Space)) return; // Checks if player is pressing spacebar.
            
            if (gameTime.TotalGameTime.TotalMilliseconds > this.lastCast + this.castDelay)
            {
                this.Add(new Projectile(new Vector2(this.caster.position.X, this.caster.position.Y), this.game));
                this.lastCast = gameTime.TotalGameTime.TotalMilliseconds;
            }
        }
        
        private void Hit()
        {
            foreach (var firebolt in this)
            {
                foreach (var enemy in this.game.EnemyManager)
                {
                    if (firebolt.hitbox.Intersects(enemy.Value.hitbox))
                    {
                        this.game.EnemyManager.Remove(enemy.Key); // TODO: https://stackoverflow.com/questions/2024179/collection-was-modified-enumeration-operation-may-not-execute-in-arraylist
                        this.Remove(firebolt);
                    }
                }
            }
            
        }
    }
}