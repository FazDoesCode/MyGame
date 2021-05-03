using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class EnemyManager : List<Impostor>
    {
        private Random randUtil;
        private GameRoot game;
        private int enemyCap;
        private int spawnDelay;
        private double lastSpawned;
        
        public EnemyManager(GameRoot game)
        {
            this.game = game;
            this.randUtil = new Random();
            this.enemyCap = 25;
            this.spawnDelay = 1000;
        }

        public void Update(GameTime gTime)
        {
            foreach (var enemy in this)
            {
                if (enemy.position.X < (0 - enemy.texture.Width))
                {
                    this.Remove(enemy);
                }
            }
            
            if (this.Count < this.enemyCap) this.CreateEnemy(gTime);

            foreach (var enemy in this)
            {
                enemy.Update(gTime);
            }
        }

        public void Draw()
        {
            foreach (var enemy in this)
            {
                enemy.Draw();
            }
        }
        
        private void CreateEnemy(GameTime gTime)
        {
            if (gTime.TotalGameTime.TotalMilliseconds > this.lastSpawned + this.spawnDelay)
            {
                this.Add(new Impostor(this.RandomPosition(), this.game));
                lastSpawned = gTime.TotalGameTime.TotalMilliseconds;
            }
        }

        private Vector2 RandomPosition()
        {
            int maxX = this.game.graphics.PreferredBackBufferWidth + 100;
            int maxY = randUtil.Next(0, this.game.graphics.PreferredBackBufferHeight - 64);
            return new Vector2(maxX, maxY);
        }
    }
}