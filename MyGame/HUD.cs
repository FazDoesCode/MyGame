using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    public class HUD
    {
        private HUDPositions hudPos;
        private GameRoot game;
        private SpriteFont font;
        private Vector2 scoreOrigin;
        private Player player;
        private int score;
        
        public HUD(GameRoot game)
        {
            this.game = game;
            this.player = this.game.player;
            this.score = 0;
            this.font = this.game.content.Load<SpriteFont>("sprites/font");
            this.SetHudpositions();
        }

        private void SetHudpositions()
        {
            this.hudPos = new HUDPositions
            {
                scorePos = new Vector2(100, 100)
            };
        }

        public void Update(GameTime gameTime)
        {
            this.score = this.player.score;
            this.scoreOrigin = this.font.MeasureString(this.score.ToString()) / 2;
        }

        public void Draw()
        {
            this.game.spriteBatch.DrawString(this.font,
                this.score.ToString(),
                this.hudPos.scorePos,
                Color.Black,
                0,
                this.scoreOrigin,
                1.0f,
                SpriteEffects.None,
                0.5f);
        }

        private struct HUDPositions
        {
            public Vector2 scorePos;
        }
    }
}