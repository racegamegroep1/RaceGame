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
        private Rectangle SelectFullscreen;
        private bool BackSelected = false;

        public Options()
        {
            SelectFullscreen = new Rectangle(700, 100, 20, 20); 

        }
        public void Update()
        {
            if (Game.backRectangle.Intersects(new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1)))
            {
                BackSelected = true;
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    ReturnEvent(this, new EventArgs());
            }
            else
                BackSelected = false;

            
            if(SelectFullscreen.Intersects(new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1)))
            {
                if(Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    Game.graphics.ToggleFullScreen();
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                ReturnEvent(this, new EventArgs());
        }

        public void Draw()
        {
            Game.spriteBatch.Begin();
            Game.spriteBatch.Draw(Game.player.GetTexture(), new Rectangle(0,0, Game.graphics.PreferredBackBufferWidth, Game.graphics.PreferredBackBufferHeight), Color.White);

            if (BackSelected == false)  
                Game.spriteBatch.Draw(Game.back, Game.backRectangle, Color.White);
            else
                Game.spriteBatch.Draw(Game.backHover, Game.backRectangle, Color.White);

            Game.spriteBatch.DrawString(Game.spriteFont, "Options", new Vector2(10, 10), Color.White);
            Game.spriteBatch.DrawString(Game.spriteFont, "Fullscreen: ", new Vector2((Game.graphics.PreferredBackBufferWidth / 2) - (Game.spriteFont.MeasureString("Fullscreen: ").X / 2), 100), Color.White);
            Game.spriteBatch.Draw(Game.filler, SelectFullscreen, Color.Red);
            Game.spriteBatch.End();
        }
    }
}
