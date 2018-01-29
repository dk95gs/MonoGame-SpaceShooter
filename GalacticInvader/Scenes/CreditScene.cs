using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticInvader.Scenes
{
    /// <summary>
    /// Shows the credits
    /// </summary>
    public class CreditScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D creditTex;
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="game">Instance of game</param>
        public CreditScene(Game game) : base(game)
        {
            GalacticInvader g = (GalacticInvader)game;
            this.spriteBatch = g.spriteBatch;
            creditTex = g.Content.Load<Texture2D>("Images/credit");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        /// <summary>
        /// Draws the scene
        /// </summary>
        /// <param name="gameTime">Instance of gametime</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(creditTex, Vector2.Zero, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
