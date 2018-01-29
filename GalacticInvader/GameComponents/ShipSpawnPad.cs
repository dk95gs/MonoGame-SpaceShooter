using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace GalacticInvader.GameComponents
{
    /// <summary>
    /// Spawn pad for enemies
    /// </summary>
    public class ShipSpawnPad : DrawableGameComponent
    {
        private Enemy enemy;
        private Texture2D tex;
        private int x = 500;
        private float seconds = 0;
        public Vector2 pos;
        private float spawnTime;
        private SoundEffect death;
        private int row;
        private int collum;
        private int width;
        private int height;       
        private List<Enemy> enemyList;

        public SpriteBatch spriteBatch;
        public PlayerShip player;
        public Effect deathEffect;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="game">Instance of game</param>
        /// <param name="spriteBatch">Instance of spritebatch</param>
        /// <param name="player">Instance of playership</param>
        /// <param name="tex">Texture for the enemy</param>
        /// <param name="pos">Spawn location</param>
        /// <param name="spawnTime">Time between spawns</param>
        /// <param name="death">Sound effect for death</param>
        /// <param name="row">Spritesheet row</param>
        /// <param name="collum">Spritesheet collum</param>
        /// <param name="deathEffect">Effect for when enemy dies</param>
        /// <param name="width">Width of the frame</param>
        /// <param name="height">Height of the frame</param>
        public ShipSpawnPad(Game game, SpriteBatch spriteBatch, PlayerShip player,
                            Texture2D tex, Vector2 pos, float spawnTime, SoundEffect death,
                            int? row, int? collum, Effect deathEffect,int? width, int? height) : base(game)
        {
            this.player = player;
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.pos = pos;
            this.spawnTime = spawnTime;
            this.death = death;
         
            this.row = (int)row;
            this.collum = (int)collum;
            this.deathEffect = deathEffect;
            this.width = (int) width;
            this.height = (int) height;
            enemyList = new List<Enemy>();  
        }
        /// <summary>
        /// Removes any enemy that is invisible from the list
        /// </summary>
        public void UpdateShips()
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (!enemyList[i].isVisable)
                {
                    enemyList.RemoveAt(i);
                    i--;
                }
            }
        }
        /// <summary>
        /// Spawns the enemies
        /// </summary>
        /// <param name="gameTime"></param>
        public void SpawnShips(GameTime gameTime)
        {
            seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (seconds > spawnTime)
            {

                enemy = new Enemy(tex, player, pos , true, width, height,row,collum,death,deathEffect);
                enemyList.Add(enemy);
                seconds = 0;
            }
        }
        /// <summary>
        /// Updates the spawnpad
        /// </summary>
        /// <param name="gameTime">Instance of gametime</param>
        public override void Update(GameTime gameTime)
        {
            SpawnShips(gameTime);
            UpdateShips();

            foreach (Enemy e in enemyList)
                e.Update(gameTime);
            base.Update(gameTime);
        }
        /// <summary>
        /// Draws the enemies
        /// </summary>
        /// <param name="gameTime">Instance of gametime</param>
        public override void Draw(GameTime gameTime)
        {
            foreach (Enemy e in enemyList)
                e.Draw(spriteBatch);
            base.Draw(gameTime);
        }
        /// <summary>
        /// Makes every enemy in the list invisible; removing them from
        /// the list
        /// </summary>
        public void ClearList()
        {
            for (int i = 0; i < enemyList.Count(); i++)
            {
                enemyList[i].isVisable = false;
            }
        }
    }
}
