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
    /// <summary>
    /// A scene
    /// </summary>
    public class GameScene : DrawableGameComponent
    {
        public List<GameComponent> Components { get; set; }

        /// <summary>
        /// Shows the scene
        /// </summary>
        public virtual void show()
        {
            this.Enabled = true;
            this.Visible = true;
        }
        /// <summary>
        /// Hides the scene
        /// </summary>
        public virtual void hide()
        {
            this.Enabled = false;
            this.Visible = false;
        }
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="game">Instance of game</param>
        public GameScene(Game game) : base(game)
        {
            Components = new List<GameComponent>();
            hide();
        }
        /// <summary>
        /// Updates the gamescene
        /// </summary>
        /// <param name="gameTime">Instance of gametime</param>
        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < Components.Count(); i++)
            {
                if (Components[i] == null)
                {
                    Components.RemoveAt(i);
                }
            }
            foreach (GameComponent item in Components)
            {
                if (item.Enabled)
                {
                    item.Update(gameTime);
                }
            }
            base.Update(gameTime);
        }
        /// <summary>
        /// Drwas the gamescene
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            //Goes through each game component and draws it if its a drawable game component and it's visible
            DrawableGameComponent comp = null;
            foreach (GameComponent item in Components)
            {
                if (item is DrawableGameComponent)
                {
                    comp = (DrawableGameComponent)item;
                    if (comp.Visible)
                    {
                        comp.Draw(gameTime);
                    }
                }
            }
            base.Draw(gameTime);
        }
    }
}
