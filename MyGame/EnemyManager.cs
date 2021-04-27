using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class EnemyManager : Dictionary<int, Impostor>
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
                if (enemy.Value.position.X < (0 - enemy.Value.texture.Width))
                {
                    this.Remove(enemy.Key);
                }
            }
            
            if (this.Count < this.enemyCap)
                this.CreateEnemy(gTime);

            foreach (var enemy in this)
            {
                enemy.Value.Update(gTime);
            }
        }

        public void Draw()
        {
            foreach (var enemy in this)
            {
                enemy.Value.Draw();
            }
        }
        
        private void CreateEnemy(GameTime gTime)
        {
            if (gTime.TotalGameTime.TotalMilliseconds > this.lastSpawned + this.spawnDelay)
            {
                this.Add(this.CreateId(), new Impostor(this.RandomPosition(), this.game));
                lastSpawned = gTime.TotalGameTime.TotalMilliseconds;
            }
        }

        private Vector2 RandomPosition()
        {
            int maxX = this.game.graphics.PreferredBackBufferWidth + 100;
            int maxY = randUtil.Next(0, this.game.graphics.PreferredBackBufferHeight - 64);
            return new Vector2(maxX, maxY);
        }
        
        private int CreateId()
        {
            return randUtil.Next(Int32.MaxValue);
        }
    }
}