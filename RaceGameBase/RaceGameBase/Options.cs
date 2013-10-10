using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RaceGameBase
{
    public class Options
    {
        public event EventHandler ReturnEvent;

        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                ReturnEvent(this, new EventArgs());
        }

        public void Draw()
        {
            Game.spriteBatch.Begin();
            Game.spriteBatch.DrawString(Game.spriteFont, "Options", new Vector2(10, 10), Color.Black);
            Game.spriteBatch.End();
        }
    }
}
