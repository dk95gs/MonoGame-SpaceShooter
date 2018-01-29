using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GalacticInvader.GameComponents;

namespace GalacticInvader.Scenes
{
    /// <summary>
    /// The scene the game begins on 
    /// </summary>
    public class StartScene : GameScene
    {
        public MenuComponent Menu { get; set; }
        public GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        string[] menuItems = {"Start Game",
                                "Help",
                                "Credit",
                                "Quit"};
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="game">Instance of game</param>
        /// <param name="graphics">Instance of graphicsdevicemanager</param>
        public StartScene(Game game, GraphicsDeviceManager graphics) : base(game)
        {
            GalacticInvader g = (GalacticInvader)game;

            this.spriteBatch = g.spriteBatch;
            SpriteFont regularFont = g.Content.Load<SpriteFont>("Fonts/regularFont");
            SpriteFont highlightFont = game.Content.Load<SpriteFont>("Fonts/hilightFont");

            Menu = new MenuComponent(game, spriteBatch, regularFont, highlightFont, menuItems);      

            Texture2D tex = g.Content.Load<Texture2D>("Images/back2");
            //Texture2D tex2 = g.Content.Load<Texture2D>("Images/back2");
            Vector2 stage = new Vector2(graphics.PreferredBackBufferWidth,
                graphics.PreferredBackBufferHeight);
            Rectangle srcRect = new Rectangle(0, 0, tex.Width, tex.Height);

            Vector2 pos1 = new Vector2(0, stage.Y - srcRect.Height);
            Vector2 speed1 = new Vector2(4, 0);
            ScrollingBackground sb1 = new ScrollingBackground(game, spriteBatch,
                tex, pos1, srcRect, speed1);
            Vector2 speed2 = new Vector2(3, 0);
            this.Components.Add(sb1);
            this.Components.Add(Menu);
        }

    }
}
