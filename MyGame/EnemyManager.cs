using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    public class EnemyManager : Dictionary<int, Impostor>
    {
        private Random randUtil;
        private int enemyCap;
        
        public EnemyManager()
        {
            this.randUtil = new Random();
            this.enemyCap = 4;
        }

        public void Update(GameTime gTime, KeyboardState kState)
        {

            foreach (var enemy in this)
            {
                if (enemy.Value.position.X < 0 - enemy.Value.texture.Width)
                {
                    this.Remove(enemy.Key);
                    Console.WriteLine("Removed enemy number: " + enemy.Key);
                }
            }
            
            if (this.Count < enemyCap)
                this.CreateEnemy();

            foreach (var enemy in this)
            {
                enemy.Value.Update(gTime, kState);
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
            this.Add(this.CreateId(), new Impostor(this.RandomPosition()));
        }

        private Vector2 RandomPosition()
        {
            int maxX = randUtil.Next(GameRoot.graphics.PreferredBackBufferWidth, GameRoot.graphics.PreferredBackBufferWidth + 1000);
            int maxY = randUtil.Next(0, GameRoot.graphics.PreferredBackBufferHeight - 64);
            return new Vector2(maxX, maxY);
        }
        
        private int CreateId()
        {
            return randUtil.Next(Int32.MaxValue);
        }
    }
}