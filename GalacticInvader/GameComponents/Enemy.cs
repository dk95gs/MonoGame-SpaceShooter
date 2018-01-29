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
    /// Normal enemy
    /// </summary>
    public class Enemy : IAnimate
    {
        //Variables
        private bool isAnimated;
        private SoundEffect death;
        private int row;
        private int collum;
        private int points = 50;
        private Rectangle srcRect;

        public Texture2D tex;
        public Vector2 pos;
        public PlayerShip player;
        public bool isVisable;
        public Vector2 diff; 
        public Effect deathEffect;

        public Vector2 dimension { get; set; }
        public List<Rectangle> frames { get; set; }
        public int frameIndex { get; set; }
        public int delay { get; set; }
        public int delayCounter { get; set; }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="tex">Spritesheet for the enemy</param>
        /// <param name="player">Instance of the player</param>
        /// <param name="pos">Enemy's starting position</param>
        /// <param name="isAnimated">If the enemy is animated or not</param>
        /// <param name="width">Width of the frame</param>
        /// <param name="height">Height of the frame</param>
        /// <param name="row">Spritesheet rows</param>
        /// <param name="collum">Spritesheet collums</param>
        /// <param name="death">Sound effect for death</param>
        /// <param name="deathEffect">Effect for death</param>
        public Enemy(Texture2D tex,PlayerShip player, Vector2 pos,
                     bool isAnimated, int? width, int? height,int? 
                     row, int? collum, SoundEffect death, Effect deathEffect) 
        {
            this.tex = tex;
           
            this.player = player;
            this.isAnimated = isAnimated;
            this.row = (int) row;
            this.collum = (int) collum;
            isVisable = true;
            this.death = death;
            this.deathEffect = deathEffect;
            if(isAnimated)
            {
                dimension = new Vector2((float)width,(float)height);
                delay = 5;
                frameIndex = -1;
                CreateFrames();
            }

            this.pos = pos;
        }
        /// <summary>
        /// Checks the collision between enemy and playership and its lazers
        /// </summary>
        public void CheckCollision()
        {
            foreach(Lazer l in PlayerShip.lazerList)
            {
                if(l.getBounds().Intersects(getBounds()))
                {
                    death.Play();
                    isVisable = false;
                    l.isVisable = false;
                    deathEffect.Position = new Vector2(getBounds().X, getBounds().Y);
                    deathEffect.startAnimation();
                    UserInterface.score += points;
                }
            }
            if(player.getBounds().Intersects(getBounds()))
            {
                isVisable = false;
                deathEffect.Position = new Vector2(player.getBounds().X, player.getBounds().Y);
                deathEffect.startAnimation();
                if (PlayerShip.isDamagable == true)
                {
                    if (PlayerShip.livesList.Count() > 0)
                    {
                        PlayerShip.health--;
                        PlayerShip.livesList.RemoveAt(0);
                        PlayerShip.isDamagable = false;
                    }
                }
                death.Play();
            }
        }
      
        /// <summary>
        /// Updates the enemy
        /// </summary>
        /// <param name="gameTime">Instance of gametime</param>
        public void Update(GameTime gameTime)
        {
            //Animates enemy if it's supposed to be animated
            if (isAnimated)
            {
                PlayFrames(3, 3);
            }
            //Moves enemy towards the player until it hits them
            diff = PlayerShip.position - pos;
            diff.Normalize();
            pos += diff *5;
            CheckCollision();


        }
        /// <summary>
        /// Draws the enemey
        /// </summary>
        /// <param name="spriteBatch">Instance of spritebatch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isAnimated)
            {
                if (frameIndex >= 0)
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(tex, pos, frames[frameIndex], Color.White);
                    spriteBatch.End();
                }
               
            }
            else
            {
                spriteBatch.Begin();
                spriteBatch.Draw(tex, pos, Color.White);
                spriteBatch.End();
            }
          
        }
        /// <summary>
        /// Gets the enemys boundry
        /// </summary>
        /// <returns>A rectangle with the same position and dimensions as the enemy</returns>
        public Rectangle getBounds()
        {
            return new Rectangle((int)pos.X , (int)pos.Y , 123 , 80 );
        }

        public void CreateFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i <= row; i++)
            {
                for (int j = 0; j < collum; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
                    frames.Add(r);
                }
            }
        }

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
