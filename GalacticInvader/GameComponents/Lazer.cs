using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace GalacticInvader.GameComponents
{
    /// <summary>
    /// Lazer or other projectile
    /// </summary>
    public class Lazer 
    {
        //Variables
        private Texture2D lazerTex;
        private float rotation = 0f;

        public Vector2 lazerPos;        
        public bool isVisable;
        private Rectangle srcRect;
        private Vector2 origin;
        public int y;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lazerTex">Texture for the lazer</param>
        public Lazer( Texture2D lazerTex)
        {
        
            this.lazerTex = lazerTex;
            lazerPos = PlayerShip.position;
            lazerPos.X += 70;
            lazerPos.Y += 50;
            y = 0;
            srcRect = new Rectangle(0, 0, lazerTex.Width, lazerTex.Height);
            origin = new Vector2(lazerTex.Width / 2, lazerTex.Height / 2);
        }
        /// <summary>
        /// Updates the lazer for the playership
        /// </summary>
        public void Update()
        {
            lazerPos.X += 2 * PlayerShip.SPEED;
            lazerPos.Y += y;
            rotation += (float) .1;
        }
        /// <summary>
        /// Updates the lazer for the BigBoss
        /// </summary>
        public void BossUpdate()
        {
            lazerPos.X -= 2 * PlayerShip.SPEED;
            lazerPos.Y -= y;
            rotation += (float).1;
        }
        /// <summary>
        /// Draws the lazer
        /// </summary>
        /// <param name="spriteBatch">Instance of the spritebatch</param>
       public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(lazerTex, lazerPos, Color.White);
            spriteBatch.Draw(lazerTex, lazerPos, srcRect, Color.White,
                rotation, origin, 1.0f, SpriteEffects.None, 0f);
        }
        /// <summary>
        /// Gets the lazers boundry
        /// </summary>
        /// <returns>A rectangle with the same position and dimensions as the lazer</returns>
        public Rectangle getBounds()
        {
            return new Rectangle((int)lazerPos.X, (int)lazerPos.Y, lazerTex.Width, lazerTex.Height);
        }
    }
}
