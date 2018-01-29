using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalacticInvader.GameComponents;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;

namespace GalacticInvader.Scenes
{
    /// <summary>
    /// Scene that plays the game
    /// </summary>
    public class ActionScene : GameScene
    {
        private Texture2D bTex;
        private Boss boss;
        public Texture2D bossShot;
        private bool spawnBigBoss = false;
        private ShipSpawnPad spawnLocation3;
        private ShipSpawnPad spawnLocation2;
        private ShipSpawnPad spawnLocation;
        private UserInterface ui;
        private SoundEffect deathSound;
        private GameComponents.Effect deathEffect;
        private TripleShotPowerUp tps;
        private RapidFirePowerUp rfp;
        private Astroid astroid;
        private SpriteBatch spriteBatch;
        private PlayerShip playerShip;
        private GalacticInvader g;
        private Texture2D lifeTex;
        private BigBoss bigBoss;
        private Texture2D bigBosTex;
        private float sec;
        private float timer;
        public float rapidFireTimer;
        public float tripleFireTimer;
        public int spawnPoints;
        public static bool spawnBoss = true;
        public bool gameStart = true;
        public static bool bossText = false;
        public static bool bossWinText = false;
        public float bossTextTime;    
        public Texture2D pShipTex;
        public Texture2D pShipShot;
        public static bool resume = false;
        public bool spawned = false;
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="game">Instance of game</param>
        /// <param name="graphics">Instance of GraphicsDeviceManager</param>
        public ActionScene(Game game,GraphicsDeviceManager graphics) : base(game)
        {
            //Adds the background and starts the game
            spawnBoss = true;
            g = (GalacticInvader)game;
            this.spriteBatch = g.spriteBatch;
            Texture2D tex = g.Content.Load<Texture2D>("Images/back");
            Texture2D tex2 = g.Content.Load<Texture2D>("Images/b2");
            Vector2 stage = new Vector2(graphics.PreferredBackBufferWidth,
                graphics.PreferredBackBufferHeight);
            Rectangle srcRect = new Rectangle(0, 0, tex.Width, tex.Height);
            Vector2 pos1 = new Vector2(0, stage.Y - srcRect.Height);
            Vector2 speed1 = new Vector2(4, 0);
            ScrollingBackground sb1 = new ScrollingBackground(game, spriteBatch,
                tex, pos1, srcRect, speed1);
            Vector2 speed2 = new Vector2(3, 0);
            ScrollingBackground sb2 = new ScrollingBackground(game, spriteBatch,
                tex2, pos1, srcRect, speed2);
            this.Components.Add(sb1);
            this.Components.Add(sb2);
       
            spawnPoints = 500;
            StartGame();
      
            Song song = g.Content.Load<Song>("Effects/background");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(song);
        }
        /// <summary>
        /// Removes all game compoennts
        /// </summary>
        public void EndGame()
        {
            this.Components.Remove(astroid);
            this.Components.Remove(spawnLocation);
            this.Components.Remove(spawnLocation2);
            this.Components.Remove(spawnLocation3);
            this.Components.Remove(playerShip);
            this.Components.Remove(deathEffect);
            this.Components.Remove(tps);
            this.Components.Remove(rfp);
            this.Components.Remove(bigBoss);
          
        }
        /// <summary>
        /// Adds game components
        /// </summary>
        public void StartGame()
        {
            spawned = false;
            Texture2D ex = g.Content.Load<Texture2D>("Images/Explosion");
            deathEffect = new GameComponents.Effect(g, spriteBatch, ex, Vector2.Zero);
            pShipTex = g.Content.Load<Texture2D>("Images/p1");
            pShipShot = g.Content.Load<Texture2D>("Images/Shoot");
            lifeTex = g.Content.Load<Texture2D>("Images/lives");
            playerShip = new PlayerShip(g, spriteBatch, pShipTex, pShipShot,lifeTex);

            Texture2D tex3 = g.Content.Load<Texture2D>("Images/enem");
            deathSound = g.Content.Load<SoundEffect>("Effects/eDeath");
            spawnLocation = new ShipSpawnPad(g, spriteBatch, playerShip, tex3, new Vector2(1200, -150), 2, deathSound, 3, 1, deathEffect,1223,80);
            Texture2D e2 = g.Content.Load<Texture2D>("Images/e2");
            spawnLocation2 = new ShipSpawnPad(g, spriteBatch, playerShip, e2, new Vector2(1200, 200), 2, deathSound, 3, 1, deathEffect,128,80);
            Texture2D ufo = g.Content.Load<Texture2D>("Images/UFO");
            spawnLocation3 = new ShipSpawnPad(g, spriteBatch, playerShip, ufo, new Vector2(1200, 500), 2, deathSound, 3, 1, deathEffect,123,80);
            Texture2D astroidTex = g.Content.Load<Texture2D>("Images/astroid");

            astroid = new Astroid(g, spriteBatch, astroidTex, new Vector2(1200, 500),playerShip,deathEffect,deathSound);

            Texture2D rapidFireTex = g.Content.Load<Texture2D>("Images/powerup");
            rfp = new RapidFirePowerUp(g, spriteBatch, rapidFireTex, new Vector2(1200, 400), playerShip);
            Texture2D triplePowerTex = g.Content.Load<Texture2D>("Images/triple");
            tps = new TripleShotPowerUp(g, spriteBatch, triplePowerTex, new Vector2(1200, 100), playerShip);
            SpriteFont font = g.Content.Load<SpriteFont>("Fonts/regularFont");
            SpriteFont font2 = g.Content.Load<SpriteFont>("Fonts/font");
            ui = new UserInterface(g, spriteBatch, font2, playerShip);
            this.Components.Add(tps);
            this.Components.Add(rfp);
            bigBosTex = g.Content.Load<Texture2D>("Images/para");
            bossShot = g.Content.Load<Texture2D>("Images/shotboss");
            bigBoss = new BigBoss(g, spriteBatch, bigBosTex,bossShot,playerShip,deathEffect,deathSound);
            bTex = g.Content.Load<Texture2D>("Images/drag");

            this.Components.Add(ui);
            this.Components.Add(spawnLocation);
            this.Components.Add(spawnLocation2);
            this.Components.Add(spawnLocation3);

            this.Components.Add(astroid);
            this.Components.Add(playerShip);
            this.Components.Add(deathEffect);
        }
        /// <summary>
        /// Updates the actionscene
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            //Displays a message after the big boss i defeated
            KeyboardState ks = Keyboard.GetState();
            if (bossWinText)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(timer > 2)
                {
                    bossWinText = false;
                    timer = 0;
                    resume = true;
                }
            }
            //Resumes the game after the big boss dies; adding the game components back
            if (BigBoss.isDead && resume)
            {
                this.Components.Add(spawnLocation);
                this.Components.Add(spawnLocation2);
                this.Components.Add(spawnLocation3);
                this.Components.Add(astroid);
                this.Components.Add(boss);
                
                resume = false;
            }
            //Removes all damagables and enemies and enables the big boss warning text
            if(UserInterface.score > 2000 && spawned == false)
            {
                this.Components.Remove(astroid);
                spawnLocation.ClearList();
                spawnLocation2.ClearList();
                spawnLocation3.ClearList();
                
                this.Components.Remove(spawnLocation);
                this.Components.Remove(spawnLocation2);
                this.Components.Remove(spawnLocation3);
                this.Components.Remove(boss);
                bossText = true;
                sec += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            //Warns the player of the incoming boss then spawns it
            if(sec > 4 && bossText == true)
            {
                this.Components.Add(bigBoss);
                spawned = true;
                spawnBigBoss = false;
                bossText = false;
                sec = 0;
            }
            //Repositions the asteroid to a random position after it moves off the screen
            if (astroid.pos.X < -astroid.tex.Width/16)
            {
                int num = 0;
                Random rand = new Random();
                num = rand.Next(0, 500);
                astroid.pos = new Vector2(1200, num);
            }
            //Repositions the asteroid to a random position after it gets destroyed
            if (astroid.Visible == false)
            {
                int num = 0;
                Random rand = new Random();
                num = rand.Next(0, 500);
                astroid.pos = new Vector2(1200, num);
                astroid.Visible = true;
            }
            //If the triple fire power up moves off screen it respawns in a few seconds at a new random position
            if (tps.pos.X < -tps.tex.Width)
            {
                tripleFireTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (tripleFireTimer > 6)
                {
                    int num = 0;
                    Random rand = new Random();
                    num = rand.Next(0, 500);
                    tps.pos = new Vector2(1200, num);
                    tps.Visible = true;
                    tripleFireTimer = 0;
                }
            }
            //If the rapid fire power up moves off screen it respawns in a few seconds at a new random position
            if (rfp.pos.X < -rfp.tex.Width)
            {
                rapidFireTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (rapidFireTimer > 6)
                {
                    int num = 0;
                    int num2 = 0;
                    Random rand = new Random();
                    num = rand.Next(0, 500);
                    num2 = rand.Next(0, 500);
                    rfp.pos = new Vector2(1200, num);
                    rfp.Visible = true;
               
                    rapidFireTimer = 0;        
                    
                }
            }
            //If the triple power fire up gets picked up by the player it respawns in a few seconds
            if(tps.Visible == false)
            {
                tripleFireTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (tripleFireTimer > 6)
                {
                    int num = 0;

                    Random rand = new Random();
                    tps.pos = new Vector2(1200, num);
                    tps.Visible = true;
                    tripleFireTimer = 0;
                }
            }
            //If the rapid power fire up gets picked up by the player it respawns in a few seconds
            if (rfp.Visible == false)
            {
                rapidFireTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (rapidFireTimer > 6)
                {
                    int num = 0;
                    int num2 = 0;
                    Random rand = new Random();
                    num = rand.Next(0, 500);
                    num2 = rand.Next(0, 500);
                    rfp.pos = new Vector2(1200, num);
                   
                    rfp.Visible = true;
                    rapidFireTimer = 0;
                }

            }
           //Ends the game if the players health reaches 0
            if(PlayerShip.health == 0)
            {
                EndGame();
                spawnPoints = 500;
                UserInterface.rapidFireStatus = "Offline";
                UserInterface.tripleFireStatus = "Offline";
            }
            //Restarts game 
            if (ks.IsKeyDown(Keys.X) && PlayerShip.health == 0)
            {
                StartGame();
                PlayerShip.health = 5;
                spawnPoints = 500;
                spawnBoss = true;
            }
            //Spawns mini boss
            if (UserInterface.score > spawnPoints && spawnBoss == true)
            {
                
                bossTextTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (bossTextTime > 4)
                    SpawnBoss();
            }

            if(Boss.isDead == true)
            {
                
                spawnBoss = true;
                Boss.isDead = false;
            }
            base.Update(gameTime);
        }
        /// <summary>
        /// Spawns the boss and increases the points needed to spawn it again
        /// </summary>
        public void SpawnBoss()
        {
            
            boss = new Boss(g, spriteBatch, bTex, playerShip, new Vector2(Shared.stage.X - 200, 200), true, 1, 3, deathSound, deathEffect);
            this.Components.Add(boss);
            spawnPoints = spawnPoints + 500;
            spawnBoss = false;
            bossText = false;
            bossTextTime = 0;
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
