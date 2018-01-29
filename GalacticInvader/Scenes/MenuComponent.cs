
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GalacticInvader.Scenes
{
    //Main menu
    public class MenuComponent : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont regularFont, hilightFont;
        private List<string> menuItems;
        private Vector2 position;
        private Color regularColor = Color.LightBlue;
        private Color hilightColor = Color.Cyan;
        private KeyboardState oldState; 
        public int SelectedIndex { get; set; }      
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="game">Instance of game</param>
        /// <param name="spriteBatch">Instance of spritebatch</param>
        /// <param name="regularFont">font for the regular text</param>
        /// <param name="hilightFont">font for when the text it choosen</param>
        /// <param name="menus">String array containing what's on the main menu</param>
        public MenuComponent(Game game,
            SpriteBatch spriteBatch,
            SpriteFont regularFont,
            SpriteFont hilightFont,
            string[] menus) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.regularFont = regularFont;
            this.hilightFont = hilightFont;
            
            menuItems = menus.ToList(); 
            position = new Vector2(Shared.stage.X / 2 -60, Shared.stage.Y / 2 - 70);

        }
        /// <summary>
        /// Updates the menu; handling moving the cruser up and down
        /// </summary>
        /// <param name="gameTime">Instance of gametime</param>
        public override void Update(GameTime gameTime)
        {
            
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.S) && oldState.IsKeyUp(Keys.S))
            {
                SelectedIndex++;
                if (SelectedIndex == menuItems.Count)
                {
                    SelectedIndex = 0;
                }
            }
            if (ks.IsKeyDown(Keys.W) && oldState.IsKeyUp(Keys.W))
            {
                SelectedIndex--;
                if (SelectedIndex == -1)
                {
                    SelectedIndex = menuItems.Count - 1;
                }

            }
            oldState = ks;


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 tempPos = position;
            spriteBatch.Begin();
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (SelectedIndex == i)
                {
                    spriteBatch.DrawString(hilightFont, menuItems[i],
                        tempPos, hilightColor);
                    tempPos.Y += hilightFont.LineSpacing;
                }
                else
                {
                    spriteBatch.DrawString(regularFont, menuItems[i],
                        tempPos, regularColor);
                    tempPos.Y += regularFont.LineSpacing;
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
