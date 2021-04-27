using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    public class Impostor
    {
        public Texture2D texture;
        public Vector2 position;
        public Rectangle hitbox;
        private GameRoot game;
        private float backfallSpeed;
        
        public Impostor(Vector2 position, GameRoot game)
        {
            this.game = game;
            this.texture = this.game.content.Load<Texture2D>("sprites/enemies/among-us-red");
            this.position = position;
            this.backfallSpeed = 80.0f;
        }

        public void Update(GameTime gTime)
        {
            this.hitbox = new Rectangle((int) this.position.X, (int) this.position.Y, this.texture.Width, this.texture.Height);
            this.BackFall(gTime);
        }
        
        public void Draw()
        {
            this.game.spriteBatch.Draw(this.texture, this.hitbox, Color.White);
        } // Draws ship on the screen

        private void BackFall(GameTime gTime)
        {
            float deltaTime = (float) gTime.ElapsedGameTime.TotalSeconds;
            this.position.X -= this.backfallSpeed * deltaTime;
        }
    }
}