using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RaceGameBase
{
    public class Credits
    {
        public event EventHandler ReturnEvent;

        public void Update()
        {
            if (Keyboard.GetState().GetPressedKeys().Length > 0)
                ReturnEvent(this, new EventArgs());
            /*if(Mouse.GetState().LeftButton == ButtonState.Pressed)
                ReturnEvent(this, new EventArgs());*/
        }

        public void Draw()
        {
            Game.spriteBatch.Begin();
            Game.spriteBatch.Draw(Game.filler, new Rectangle(0, 0, Game.graphics.PreferredBackBufferWidth, Game.graphics.PreferredBackBufferHeight), Color.Black);
            Game.spriteBatch.DrawString(Game.spriteFont, "Credits", new Vector2(10, 10), Color.White);

            Game.spriteBatch.End();
        }
    }
}
