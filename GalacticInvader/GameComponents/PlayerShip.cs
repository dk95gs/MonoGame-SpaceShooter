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
    /// the player
    /// </summary>
    public class PlayerShip : DrawableGameComponent, IAnimate
    {
        //Variables
        private int x = 280;
        private Lives life;
        private Texture2D livesTex;
        private float seconds;
        private float rapidFireDuration;
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        public bool doublePowerUp = false;
        public float timer = 0;
        public static List<Lazer> tripleLazerList;
        public bool triplePowerUp = false;
        public static int health = 5;       
        public static Vector2 position;
        public static int SPEED = 5;
        public Lazer lazer;
        public Texture2D lazerTex;
        public static List<Lazer> lazerList;
        public GalacticInvader gi = new GalacticInvader();
        public KeyboardState pastKey;
        public float sec = 3;
        public float bossSec;
        public bool rapidFire = false;
        public float powerUpDuration;      
        public static List<Lives> livesList;
        public static bool isDamagable = true;
        public static bool isDamagableByBoss = true;

        public Vector2 dimension { get; set; }
        public List<Rectangle> frames { get; set; }
        public int frameIndex { get; set; }
        public int delay { get; set; }
        public int delayCounter { get; set; }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="game">Instance of the game</param>
        /// <param name="spriteBatch">Instance of spritebatch</param>
        /// <param name="tex">Spritesheet for the player</param>
        /// <param name="lazerTex">Texture for lazer</param>
        /// <param name="livesTex">Spritesheet for lives</param>
        public PlayerShip(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex, Texture2D lazerTex, Texture2D livesTex): base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.lazerTex = lazerTex;
            lazerList = new List<Lazer>();
            tripleLazerList = new List<Lazer>();
            position = new Vector2(0, 0);
            livesList = new List<Lives>();
            this.livesTex = livesTex;
            dimension = new Vector2(100, 100);
            frameIndex = 0;
            delay = 10;
            CreateFrames();
            health = 5;
            for (int i = 0; i < 5; i++)
            {
                life = new Lives(livesTex, new Vector2(x, 0), 5);
                livesList.Add(life);
                x = x - 50;
            }
        }

        /// <summary>
        /// Makes the lazer invisible if it leaves the screen
        /// and removes any lazer thats invisible from the list
        /// </summary>
        public void UpdateLazers()
        {
            foreach(Lazer l in lazerList)
            {
                if (l.lazerPos.X > Shared.stage.X)
                    l.isVisable = false;
            }
            for (int i = 0; i < lazerList.Count; i++)
            {
                if(!lazerList[i].isVisable)
                {
                    lazerList.RemoveAt(i);
                    i--;
                }
            }
        }
        /// <summary>
        /// Updates the lazer
        /// </summary>
        /// <param name="gameTime">Instance of gametime</param>
        public override void Update(GameTime gameTime)
        {
            PlayFrames(3, 3);
            sec += (float) gameTime.ElapsedGameTime.TotalSeconds;
            powerUpDuration += (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState ks = Keyboard.GetState();
            //Makes rapid fire last 3 seconds
            if (rapidFire == true)
            {
                rapidFireDuration += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (rapidFireDuration > 3)
                {
                    rapidFire = false;
                    rapidFireDuration = 0;
                }
            }
            //When the player picks up the triple fire power up it lasts for 3 seconds
            if (triplePowerUp)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer > 3)
                {
                    triplePowerUp = false;
                    timer = 0;
                }
            }
            //Enables both power ups if the player obtains them both
            if ( triplePowerUp == true && rapidFire == true)
            {
                doublePowerUp = true;
            }
            else
            {
                doublePowerUp = false;
            }
            //Normal fire
            if (!rapidFire && doublePowerUp == false)
            {
                if (triplePowerUp == false)
                {
                    if (ks.IsKeyDown(Keys.Space) && pastKey.IsKeyUp(Keys.Space) && sec > .5)
                    {
                        lazer = new Lazer(lazerTex);
                        lazer.isVisable = true;
                        lazerList.Add(lazer);
                        sec = 0;
                    }
                }
                //Triple fire
                if (triplePowerUp)
                {
                    if (ks.IsKeyDown(Keys.Space) && pastKey.IsKeyUp(Keys.Space) && sec > .5)
                    {
                        lazer = new Lazer(lazerTex);
                        var laz2 = new Lazer(lazerTex);
                        var laz3 = new Lazer(lazerTex);
                        laz2.isVisable = true;
                        laz3.isVisable = true;
                        lazer.isVisable = true;

                        laz2.y = -5;
                        laz3.y = 5;

                        lazerList.Add(laz2);
                        lazerList.Add(laz3);
                        lazerList.Add(lazer);
                        sec = 0;
                    }
                }
                pastKey = Keyboard.GetState();
            }
            //Rapid fire
            else if (rapidFire && doublePowerUp == false)
            {
                if (ks.IsKeyDown(Keys.Space) && sec > .1)
                {
                    lazer = new Lazer(lazerTex);
                    lazer.isVisable = true;
                    lazerList.Add(lazer);
                    sec = 0;
                }
                if (powerUpDuration > 3)
                {
                    rapidFire = true;
                    
                }
                pastKey = Keyboard.GetState();
            }
            //Rapid and triple fire
            else if( rapidFire == true && triplePowerUp == true)
            {
                doublePowerUp = true;
                if (ks.IsKeyDown(Keys.Space)  && sec > .1)
                {
                    lazer = new Lazer(lazerTex);
                    var laz2 = new Lazer(lazerTex);
                    var laz3 = new Lazer(lazerTex);
                    laz2.isVisable = true;
                    laz3.isVisable = true;
                    lazer.isVisable = true;

                    laz2.y = -5;
                    laz3.y = 5;

                    lazerList.Add(laz2);
                    lazerList.Add(laz3);
                    lazerList.Add(lazer);
                    sec = 0;
                }  
            }
            //Player movment and boundries
            if (ks.IsKeyDown(Keys.D))
            {
                position.X += SPEED;
                if (position.X > Shared.stage.X - tex.Width)
                {
                    position.X = Shared.stage.X - tex.Width;
                }
            }
            if (ks.IsKeyDown(Keys.A))
            {
                position.X -= SPEED;
               
                if (position.X < 0)
                {
                    position.X = 0;
                }
            }
            if (ks.IsKeyDown(Keys.W))
            {
                position.Y -= SPEED;
                
                if (position.Y < 0)
                {
                    position.Y = 0;
                }
            }
            if (ks.IsKeyDown(Keys.S))
            {
                position.Y += SPEED;
                
                if (position.Y + (tex.Height /4)  > Shared.stage.Y)
                {
                    position.Y = Shared.stage.Y - (tex.Height /4);
                }
            }
            //Updates player lazer
            foreach (Lazer l in lazerList)
                l.Update();
            //Updates player lives
            foreach (Lives li in livesList)
                li.Update();
            UpdateLazers();
            seconds += (float) gameTime.ElapsedGameTime.TotalSeconds;
            bossSec += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //Delays being damaged 
            if(isDamagable == false)
            {
                if (seconds > 1)
                {
                    isDamagable = true;
                    seconds = 0;
                }
            }
            if (isDamagableByBoss == false)
            {
                if (bossSec > 3)
                {
                    isDamagableByBoss = true;
                    bossSec = 0;
                }
            }
            
            base.Update(gameTime);
        }
        /// <summary>
        /// Draws the playership
        /// </summary>
        /// <param name="gameTime">Instance of gametime</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            foreach (Lazer l in lazerList)
                l.Draw(spriteBatch);

            foreach (Lives li in livesList)
                li.Draw(spriteBatch);

            spriteBatch.Draw(tex, position,frames[frameIndex], Color.White);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
        /// <summary>
        /// Gets the playerships boundry
        /// </summary>
        /// <returns>A rectangle with the same position and dimensions as the playership</returns>
        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height /4);
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
