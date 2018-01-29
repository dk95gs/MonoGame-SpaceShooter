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
    /// The help scene
    /// </summary>
    public class HelpScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D helpTex;
        /// <summary>
        /// CLass constructor
        /// </summary>
        /// <param name="game">Instnace of game</param>
        public HelpScene(Game game) : base(game)
        {
            GalacticInvader g = (GalacticInvader)game;
            this.spriteBatch = g.spriteBatch;
            helpTex = g.Content.Load<Texture2D>("Images/help");
        }
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        /// <summary>
        /// Draws the helpscene
        /// </summary>
        /// <param name="gameTime">Instance of gametime</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(helpTex, Vector2.Zero, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
