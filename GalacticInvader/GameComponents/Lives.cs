using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalacticInvader.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace GalacticInvader.GameComponents
{
    /// <summary>
    /// Player lives
    /// </summary>
    public class Lives : IAnimate
    {
        //Variables
        private Texture2D tex;
        //private Vector2 dimension;
        //private List<Rectangle> frames;
        //private int frameIndex = -1;
        //private int delay;
        //private int delayCounter;
        public Vector2 pos;

        public Vector2 dimension { get; set; }
        public List<Rectangle> frames { get; set; }
        public int frameIndex { get; set; }
        public int delay { get; set; }
        public int delayCounter { get; set; }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="tex">Spritesheet for the lives</param>
        /// <param name="position">starting position for lives</param>
        /// <param name="delay">delay between frame changes</param>
        public Lives(
            Texture2D tex,
            Vector2 position,
            int delay)
        {
           
            this.tex = tex;
            this.pos = position;
            this.delay = delay;
            dimension = new Vector2(32, 32);
            frameIndex = -1;
            CreateFrames();

        }
      
        /// <summary>
        /// Updates lives
        /// </summary>
        public void Update()
        {
            //Loops through the frames list
            PlayFrames(7, 7);
        }
        /// <summary>
        /// Draws the lives
        /// </summary>
        /// <param name="spriteBatch">Instance of spritebatch</param>
        public  void Draw(SpriteBatch spriteBatch)
        {
            if (frameIndex >= 0)
            {  
                spriteBatch.Draw(tex, pos, frames[frameIndex], Color.White);             
            }           
        }
        /// <summary>
        ///  Takes the spritesheet and splits it up frame by frame into a list
        /// </summary>
        public void CreateFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j <= 7; j++)
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
                    for (int i = 0; i < backward; i++)
                    {
                        frameIndex--;
                    }
                }
                delayCounter = 0;
            }
        }
    }
}
