using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using GalacticInvader.Interfaces;

namespace GalacticInvader.GameComponents
{
    /// <summary>
    /// A damagable and damaging projectile that travels towards the player
    /// </summary>
    public class Astroid : DrawableGameComponent, IAnimate
    {
        //Delcaring variables 
        private SpriteBatch spriteBatch;
        private PlayerShip player;
        private SoundEffect death;
        private Effect deathEffect;

        public Texture2D tex;
        public Vector2 pos;

        public Vector2 dimension { get; set; }
        public List<Rectangle> frames { get; set; }
        public int frameIndex { get; set; }
        public int delay { get; set; }
        public int delayCounter { get; set; }

        /// <summary>
        /// Class contructor
        /// </summary>
        /// <param name="game">Instance of game class</param>
        /// <param name="spriteBatch">Instance of spritebatch</param>
        /// <param name="tex">Spritesheet for an astroid</param>
        /// <param name="pos">Asteroids starting position</param>
        /// <param name="player">Instance of the playership</param>
        /// <param name="deathEffect">Effect on death</param>
        /// <param name="death">SoundEffect that plays on death</param>
        public Astroid(Game game,SpriteBatch spriteBatch, Texture2D tex, Vector2 pos, PlayerShip player, Effect deathEffect,SoundEffect death) : base(game)
        {
            this.tex = tex;
            this.pos = pos;
            this.player = player;
            this.spriteBatch = spriteBatch;
            this.deathEffect = deathEffect;
            this.death = death;
            dimension = new Vector2(70, 70);
            delay = 10;
            frameIndex = 0;
            CreateFrames();
        }

       
        /// <summary>
        /// Handles collision between the asteroid and the playership and its lazers
        /// </summary>
        public void CheckCollision()
        {
            foreach (Lazer l in PlayerShip.lazerList)
            {
                if (l.getBounds().Intersects(getBounds()))
                {
                    this.Visible = false;
                    l.isVisable = false;
                    deathEffect.Position = new Vector2(getBounds().X, getBounds().Y);
                    deathEffect.startAnimation();
                    death.Play();
                    UserInterface.score += 25;
                }
            }
            if (getBounds().Intersects(player.getBounds()))
            {
                this.Visible = false;
                deathEffect.Position = new Vector2(getBounds().X, getBounds().Y);
                deathEffect.startAnimation();
                death.Play();
                if (PlayerShip.isDamagable == true)
                {
                    if (PlayerShip.livesList.Count() > 0)
                    {
                        PlayerShip.health--;
                        PlayerShip.livesList.RemoveAt(0);
                    }
                    PlayerShip.isDamagable = false;
                    
                }
            }

        }
        /// <summary>
        /// Updates the astroid
        /// </summary>
        /// <param name="gameTime">instance of the GameTime</param>
        public override void Update(GameTime gameTime)
        {
            pos.X -= 8;
            CheckCollision();
            PlayFrames(16, 16);
           
            base.Update(gameTime);
        }
        /// <summary>
        /// Draws the asteroid
        /// </summary>
        /// <param name="gameTime">Instance of GameTime</param>
        public override void Draw(GameTime gameTime)
        {
            
            if (frameIndex >= 0)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(tex, pos, frames[frameIndex], Color.White);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }
        /// <summary>
        /// Gets the boundry of the asteroid
        /// </summary>
        /// <returns>A rectangle with the same position and dimensions as the asteroid</returns>
        public Rectangle getBounds()
        {
            return new Rectangle((int)pos.X, (int)pos.Y, 70, 70);
        }
        /// <summary>
        /// Takes the spritesheet and splits it up frame by frame into a list
        /// </summary>
        public void CreateFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j <= 15; j++)
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
