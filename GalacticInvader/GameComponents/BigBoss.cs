using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalacticInvader.Interfaces;
using GalacticInvader.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace GalacticInvader.GameComponents
{
    /// <summary>
    /// Boss that fires lazers
    /// </summary>
    public class BigBoss : DrawableGameComponent, IAnimate
    {
        //Variables
        private Effect deathEffect;
        private SoundEffect deathSound;

        private float y = 4;
        private float x = -2;
        private Texture2D tex;
        private SpriteBatch SpriteBatch;
        private Vector2 pos;
        public float shotTime;
        public static List<Lazer> bossLazerList;
        Lazer lazer;
        private PlayerShip player;
        private Texture2D lazerTex;
        private int health;
        public static bool isDead = false;

        public Vector2 dimension { get; set; }
        public List<Rectangle> frames { get; set; }
        public int frameIndex { get; set; }
        public int delay { get; set; }
        public int delayCounter { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Instance of game</param>
        /// <param name="spriteBatch">Instance of spritebatch</param>
        /// <param name="tex">Spritesheet for the bigboss</param>
        /// <param name="lazerTex">Texture for bigboss lazer</param>
        /// <param name="player">Instance of playershi[</param>
        /// <param name="deathEffect">Effect on death and damage</param>
        /// <param name="deathSound">Sound effect on death and damage</param>
        public BigBoss (Game game,SpriteBatch spriteBatch, Texture2D tex,
                        Texture2D lazerTex,PlayerShip player, Effect deathEffect, SoundEffect deathSound) : base(game)
        {
            this.deathSound = deathSound;
            this.SpriteBatch = spriteBatch;
            this.tex = tex;
            this.lazerTex = lazerTex;
            this.player = player;
            this.deathEffect = deathEffect;
            health = 50;
            dimension = new Vector2(150, 150);
            pos = new Vector2(900, 100);
            delay = 10;
            frameIndex = 0;
            CreateFrames();
            bossLazerList = new List<Lazer>();
            
        }
        /// <summary>
        /// Makes any lazer that moves off screen invisible and removes any invisible lazers from the list
        /// </summary>
        public void UpdateLazers()
        {
            foreach (Lazer l in bossLazerList)
            {
                if (l.lazerPos.X < 0)
                    l.isVisable = false;
            }
            for (int i = 0; i < bossLazerList.Count; i++)
            {
                if (!bossLazerList[i].isVisable)
                {
                    bossLazerList.RemoveAt(i);
                    i--;
                }
            }
        }
      
        /// <summary>
        /// Updates the BigBoss
        /// </summary>
        /// <param name="gameTime">Instance of gametime</param>
        public override void Update(GameTime gameTime)
        {
            /// If the playership is damagable and it intersects
            /// with the BigBoss the playership take damage
            if (PlayerShip.isDamagable == true)
            {
                if (PlayerShip.livesList.Count() > 0)
                {
                    if (getBounds().Intersects(player.getBounds()))
                    {
                        PlayerShip.health--;
                        PlayerShip.livesList.RemoveAt(0);
                        PlayerShip.isDamagable = false;
                        deathEffect.Position = PlayerShip.position;
                        deathEffect.startAnimation();
                        deathSound.Play();
                    }
                }
            }
            //Bigboss dies if health is less then 0
            if (health < 0)
            {
                this.Enabled = false;
                this.Visible = false;
                isDead = true;
                ActionScene.bossWinText = true;
            }
            UpdateLazers();
            //Checks to see if playerships lazer hits bigboss
            foreach(Lazer l in PlayerShip.lazerList)
            {
                if(l.getBounds().Intersects(getBounds()))
                {
                    l.isVisable = false;
                    health--;
                    deathEffect.Position = pos;
                    deathEffect.startAnimation();
                    deathSound.Play();
                }
            }
            //Checks to see if the bosses lazer hits the player
            foreach(Lazer l in bossLazerList)
            {
                if(l.getBounds().Intersects(player.getBounds()))
                {
                    if (PlayerShip.isDamagable == true)
                    {
                        if (PlayerShip.livesList.Count() > 0)
                        {
                            l.isVisable = false;
                            PlayerShip.health--;
                            PlayerShip.livesList.RemoveAt(0);
                            PlayerShip.isDamagable = false;
                            deathEffect.Position = PlayerShip.position;
                            deathEffect.startAnimation();
                            deathSound.Play();
                        }
                    }
                }
            }
            //bigboss shot interval
            shotTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //bigboss shooting
            if(shotTime > 1)
            {
                lazer = new Lazer(lazerTex);
                lazer.lazerPos = pos;
                lazer.lazerPos.Y += 80;
                lazer.lazerPos.X += 20;
                lazer.isVisable = true;

                bossLazerList.Add(lazer);
                
                shotTime = 0;
            }
            //Updates lazers
            foreach (Lazer l in bossLazerList)
                l.BossUpdate();

            //Moves bigboss and keeps it within 
            //a certain section of the screen
            pos.Y += y;
            pos.X += x;
            if (pos.Y > Shared.stage.Y - (tex.Height/4))
            {
                y = -4;
            }
            if (pos.Y < 0)
            {
                y = 4;
            }
            if (pos.X < 500)
                x = 2;

            if (pos.X > Shared.stage.X - tex.Width)
                x = -2;

            PlayFrames(3, 3);
            base.Update(gameTime);
        }
        /// <summary>
        /// Draws the BigBoss and his lazers
        /// </summary>
        /// <param name="gameTime">Instance of gametime</param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            if(frameIndex >= 0)
                SpriteBatch.Draw(tex, pos,frames[frameIndex], Color.White);
            foreach (Lazer l in bossLazerList)
                l.Draw(SpriteBatch);
            SpriteBatch.End();
            base.Draw(gameTime);
        }
        /// <summary>
        /// Gets boundry of BigBoss
        /// </summary>
        /// <returns>A rectangle with the same position and dimensions as the Bigboss</returns>
        public Rectangle getBounds()
        {
            return new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height / 4);
        }

        /// <summary>
        ///  Takes the spritesheet and splits it up frame by frame into a list
        /// </summary>
        public void CreateFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j < 1; j++)
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
