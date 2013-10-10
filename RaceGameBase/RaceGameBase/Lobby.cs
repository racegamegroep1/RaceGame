using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RaceGameBase
{
    public class Lobby
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
            Game.spriteBatch.Draw(Game.player.GetTexture(), new Rectangle(0, 0, Game.graphics.PreferredBackBufferWidth, Game.graphics.PreferredBackBufferHeight), Color.White);
            Game.spriteBatch.DrawString(Game.spriteFont, "Lobby", new Vector2(10, 10), Color.White);
            Game.spriteBatch.End();
        }
    }
}
