using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using GalacticInvader.Scenes;

namespace GalacticInvader.GameComponents
{
    /// <summary>
    /// The user interface
    /// </summary>
    public class UserInterface : DrawableGameComponent
    {
        public static string rapidFireStatus;
        public static string tripleFireStatus;
        public static int score;

        private Color rapidFireColor;        
        private Color tripleFireColor;
        private SpriteFont font;
        private SpriteBatch spriteBatch;
        private PlayerShip player;
        private float sec;
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="game">Instance of game</param>
        /// <param name="spriteBatch">Instance of spritebatch</param>
        /// <param name="font">Spritefont for the ui</param>
        /// <param name="player">Instance of playership</param>
        public UserInterface (Game game,SpriteBatch spriteBatch, SpriteFont font,PlayerShip player) : base (game)
        {
            this.spriteBatch = spriteBatch;
            this.font = font;
            this.player = player;
            rapidFireStatus = "";
            score = 0;
        }
        /// <summary>
        /// Updates the UI
        /// </summary>
        /// <param name="gameTime">Instance of gametime</param>
        public override void Update(GameTime gameTime)
        {
            sec += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //Shows the player if they have a power up or not
            if (player.rapidFire == true)
            {
                rapidFireStatus = "Online";
                rapidFireColor = Color.Cyan;
            }
            else if (player.rapidFire == false)
            {
                rapidFireStatus = "Offline";
                rapidFireColor = Color.Red;
            }

            if(player.triplePowerUp == true)
            {
                tripleFireStatus = "Online";
                tripleFireColor = Color.Cyan;
            }
            else if (!player.triplePowerUp)
            {
                tripleFireStatus = "Offline";
                tripleFireColor = Color.Red;
            }
            base.Update(gameTime);
        }
        /// <summary>
        /// Draws the UI
        /// </summary>
        /// <param name="gameTime">Instance of gametime</param>
        public override void Draw(GameTime gameTime)
        {
          
            spriteBatch.Begin();
            //Displays endgame text if player dies
            if (PlayerShip.health == 0)
            {
                spriteBatch.DrawString(font, "Game Over - Press X to restart or ESC to return to main menu",
                            new Vector2(Shared.stage.X / 2 - 250, Shared.stage.Y / 2), Color.Red);                
            }
            //Displays lives
            spriteBatch.DrawString(font, "LIVES:",
                   Vector2.Zero, Color.Red);

            //Displays points
            spriteBatch.DrawString(font, $"POINTS: {score.ToString()}",
                   new Vector2(800, 0), Color.Red);

            //Display powerup status
            spriteBatch.DrawString(font, "Rapid Fire: ",
            new Vector2(350, 0), Color.Red);

            spriteBatch.DrawString(font, rapidFireStatus,
            new Vector2(450, 0), rapidFireColor);

           spriteBatch.DrawString(font, "Triple Fire: ",
            new Vector2(570, 0), Color.Red);

            spriteBatch.DrawString(font, tripleFireStatus,
            new Vector2(670, 0), tripleFireColor);
            //Warns player of incoming boss
            if (ActionScene.bossText)
            {

                spriteBatch.DrawString(font, "Boss is incoming!",
                  new Vector2(Shared.stage.X/2 -100,Shared.stage.Y/2), Color.Red);
            }
            //Text displayed when boss is defeated
            if (ActionScene.bossWinText)
            {

                spriteBatch.DrawString(font, "Think you won? Think again!",
                  new Vector2(Shared.stage.X / 2 - 100, Shared.stage.Y / 2), Color.Red);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
