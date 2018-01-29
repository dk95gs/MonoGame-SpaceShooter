using GalacticInvader.Interfaces;
using GalacticInvader.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticInvader.GameComponents
{
    /// <summary>
    /// Boss
    /// </summary>
    public class Boss : DrawableGameComponent, IAnimate
    {
        //Variables
        int bossHealth = 10;
        public int speed = -4;
        int speedX = 4;
        public Texture2D tex;
        public Vector2 pos;
        public PlayerShip player;
        public static bool isDead;
        Rectangle srcRect;
        public Vector2 diff;
        bool isAnimated;
        SoundEffect death;
        int row;
        int collum;

        public Effect deathEffect;
        SpriteBatch spriteBatch;

        public Vector2 dimension {get;set;}
        public List<Rectangle> frames { get; set; }
        public int frameIndex { get; set; }
        public int delay { get; set; }
        public int delayCounter { get; set; }

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="game">Instance of game</param>
        /// <param name="spriteBatch">Instance of spritebatch</param>
        /// <param name="tex">Spritesheet for the boss</param>
        /// <param name="player">Instance of the playership</param>
        /// <param name="pos">Spawn position</param>
        /// <param name="isAnimated">Animated or not</param>
        /// <param name="row">Spritesheet row</param>
        /// <param name="collum">Spritesheet collum</param>
        /// <param name="death">Sound effect for death and damage</param>
        /// <param name="deathEffect">Effect for when the boss dies or is damaged</param>
        public Boss(Game game, SpriteBatch spriteBatch,
            Texture2D tex, PlayerShip player, Vector2 pos,
            bool isAnimated, int? row, int? collum,
            SoundEffect death, Effect deathEffect) : base(game)
        {

            this.tex = tex;
            this.spriteBatch = spriteBatch;
            this.player = player;
            this.isAnimated = isAnimated;
            this.row = (int)row;
            this.collum = (int)collum;
            this.Visible = true;
            this.Enabled = true;
            this.death = death;
            this.deathEffect = deathEffect;
            if (isAnimated)
            {
                dimension = new Vector2(122,128);
                delay = 10;
                frameIndex = -1;
                CreateFrames();
            }
            isDead = false;
            this.pos = pos;
        }
        /// <summary>
        /// Checks for collision between boss and player ship and playerships lazers
        /// </summary>
        public void CheckCollision()
        {
            foreach (Lazer l in PlayerShip.lazerList)
            {
                if (l.getBounds().Intersects(getBounds()))
                {
                    death.Play();
                    
                    l.isVisable = false;
                    deathEffect.Position = new Vector2(getBounds().X, getBounds().Y);
                    deathEffect.startAnimation();
                    bossHealth--;
                    if (bossHealth == 0)
                    {
                        UserInterface.score += 250;
                        this.Visible = false;
                        this.Enabled = false;
                        ActionScene.spawnBoss = true;
                        isDead = true;
                    }
                }
            }
            if (player.getBounds().Intersects(getBounds()))
            {
                
                deathEffect.Position = new Vector2(player.getBounds().X, player.getBounds().Y);
                
                if (PlayerShip.isDamagableByBoss == true)
                {
                    if (PlayerShip.livesList.Count() > 0)
                    {
                        PlayerShip.health--;
                        PlayerShip.livesList.RemoveAt(0);
                    }
                    PlayerShip.isDamagableByBoss = false;
                    death.Play();
                    deathEffect.startAnimation();
                }   
            }
        }
     
      
        /// <summary>
        /// Updates boss
        /// </summary>
        /// <param name="gameTime">Instance of gametime</param>
        public override void Update(GameTime gameTime)
        {
            //Moves boss
            pos.Y += speed;
            pos.X += speedX;
            //Removes boss if its health hits 0
            if(PlayerShip.health == 0)
            {
                this.Visible = false;
                this.Enabled = false;
            }
            //Animates the boss if it's supposed to be animated
            if (isAnimated)
            {
                PlayFrames(2, 3);

            }
            //Keeps the boss within the game screen
            if (pos.Y < 0)
            {
               speed = 4;
            }
            if (pos.Y > Shared.stage.Y - tex.Height)
            {
                speed = -4;
            }
            if (pos.X > Shared.stage.X - tex.Width/4)
            {
                speedX = -4;
            }
            if (pos.X < 0)
            {
                speedX = -speedX;
            }
            CheckCollision();
            base.Update(gameTime);
        }
        /// <summary>
        /// Draws the boss
        /// </summary>
        /// <param name="gameTime">Instance of gametime</param>
        public override void Draw(GameTime gameTime)
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
            base.Draw(gameTime);

        }
        /// <summary>
        /// Gets boundrys of the boss
        /// </summary>
        /// <returns>A rectangle with the same position and dimensions as the Boss</returns>
        public Rectangle getBounds()
        {
            return new Rectangle((int)pos.X, (int)pos.Y, 123, 80);
        }
        /// <summary>
        /// Takes the spritesheet and splits it up frame by frame into a list
        /// </summary>
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
