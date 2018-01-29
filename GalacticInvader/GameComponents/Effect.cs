using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using GalacticInvader.Interfaces;

namespace GalacticInvader.GameComponents
{
    /// <summary>
    /// Animated effect
    /// </summary>
    public class Effect : DrawableGameComponent, IAnimate
    {
        //Variables
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        //private Vector2 dimension;
        //private List<Rectangle> frames;
        //private int frameIndex = -1;
        //private int delay = 1;
        //private int delayCounter;
        public Vector2 Position { get; set; }
        public Vector2 dimension { get; set; }
        public List<Rectangle> frames { get; set; }
        public int frameIndex { get; set; }
        public int delay { get; set; }
        public int delayCounter { get; set; }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="game">Instance of game</param>
        /// <param name="spriteBatch">Instance of spritebatch</param>
        /// <param name="tex">Spritesheet for the effect</param>
        /// <param name="position">Initial position for the effect</param>
        public Effect(Game game, SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position
            ) : base(game)
        {
           
            this.tex = tex;
            this.Position = position;
            this.spriteBatch = spriteBatch;
            dimension = new Vector2(96, 96);
            this.Enabled = false;   
            this.Visible = false;
            delay = 1;
            frameIndex = -1;
            
            CreateFrames();
        }
        /// <summary>
        /// Starts the effect
        /// </summary>
        public void startAnimation()
        {
            this.Enabled = true;
            this.Visible = true;
        }       
       
        /// <summary>
        /// Updates the effect
        /// </summary>
        /// <param name="gameTime">Instance of gametime</param>
        public override void Update(GameTime gameTime)
        {
            //Loops through the frames list

            PlayFrames(11, 0);
            base.Update(gameTime);

        }
        /// <summary>
        /// Draws the effect frame by frame
        /// </summary>
        /// <param name="gameTime">Instance of gametime</param>
        public override void Draw(GameTime gameTime)
        {
            if (frameIndex >= 0)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(tex, Position, frames[frameIndex], Color.White);
                spriteBatch.End();
            }
            base.Draw(gameTime);

        }
        /// <summary>
        ///  Takes the spritesheet and splits it up frame by frame into a list
        /// </summary>
        public void CreateFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j <= 11; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
                    frames.Add(r);
                }
            }
        }
        /// <summary>
        /// Runs through the list of frames
        /// </summary>
        /// <param name="forward">Moves forward through the list</param>
        /// <param name="backward">Moves backward through the list</param>
        public void PlayFrames(int forward, int backward)
        {
            delayCounter++;
            if (delayCounter > delay)
            {
                frameIndex++;
                if (frameIndex > forward)
                {
                    frameIndex = -1;
                    this.Enabled = false;
                    this.Visible = false;
                }

                delayCounter = 0;
            }
        }
    }
}
