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
        
        public EnemyManager(GameRoot game)
        {
            this.game = game;
            this.randUtil = new Random();
            this.enemyCap = 8;
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
                this.CreateEnemy();

            foreach (var enemy in this.Values)
            {
                enemy.Update(gTime);
            }
        }

        public void Draw()
        {
            foreach (var enemy in this)
            {
                enemy.Value.Draw();
            }
        }
        
        private void CreateEnemy()
        {
            this.Add(this.CreateId(), new Impostor(this.RandomPosition(), this.game));
        }

        private Vector2 RandomPosition()
        {
            int maxX = randUtil.Next(this.game.graphics.PreferredBackBufferWidth, this.game.graphics.PreferredBackBufferWidth + 1000);
            int maxY = randUtil.Next(0, this.game.graphics.PreferredBackBufferHeight - 64);
            return new Vector2(maxX, maxY);
        }
        
        private int CreateId()
        {
            return randUtil.Next(Int32.MaxValue);
        }
    }
}