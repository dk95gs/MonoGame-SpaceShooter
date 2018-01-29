using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GalacticInvader.GameComponents
{
    /// <summary>
    /// Triple shot powerup
    /// </summary>
    public class TripleShotPowerUp : DrawableGameComponent
    {
        private PlayerShip player;
        private SpriteBatch spriteBatch;
        private float speed = 8;

        public Texture2D tex;       
        public Vector2 pos;   

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="game">Instance of game</param>
        /// <param name="spriteBatch">Instance of spritebatch</param>
        /// <param name="tex">Texture for the powerup</param>
        /// <param name="pos">Starting location</param>
        /// <param name="player">Instance of the playership</param>
        public TripleShotPowerUp(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 pos, PlayerShip player) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.pos = pos;
            this.player = player;
            this.Visible = true;
        }
        /// <summary>
        /// Updates the powerup
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            //Gives player the power up if they touch it
            if (getBounds().Intersects(player.getBounds()))
            {
                player.triplePowerUp = true;
                this.Visible = false;
               
            }
            pos.X -= speed;
            base.Update(gameTime);
        }
        /// <summary>
        /// Draws the powerup
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, pos, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);

        }
        /// <summary>
        /// Gets the powerups boundry
        /// </summary>
        /// <returns>A rectangle with the same position and dimensions as the powerup</returns>
        public Rectangle getBounds()
        {
            return new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
        }
    }
}
