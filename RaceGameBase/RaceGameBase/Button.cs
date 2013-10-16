using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RaceGameBase
{
    class Button
    {
        public event EventHandler ClickEvent;

        public Rectangle Rec { get; set; }
        public bool Hover { get; set; }
        public bool Enabled { get; set; }

        private Texture2D image;
        private Texture2D hoverImage;
        private Texture2D disabledImage;

        private MouseState currentMouseState;
        private MouseState lastMouseState;

        public Button(string name, int x, int y)
        {
            image = Game.contentManager.Load<Texture2D>(name);
            hoverImage = Game.contentManager.Load<Texture2D>(name + "_h");

            try
            {
                disabledImage = Game.contentManager.Load<Texture2D>(name + "_c");
            }
            catch { }

            Rec = new Rectangle(x, y, image.Width, image.Height);
            Hover = false;
            Enabled = true;
        }

        public void Draw()
        {
            if (Enabled)
            {
                if (Hover == false)
                    Game.spriteBatch.Draw(image, Rec, Color.White);
                else
                    Game.spriteBatch.Draw(hoverImage, Rec, Color.White);
            }
            else
                Game.spriteBatch.Draw(disabledImage, Rec, Color.White);
        }

        public void Update()
        {
            currentMouseState = Mouse.GetState();

            if (Enabled)
            {
                if (Rec.Intersects(new Rectangle(currentMouseState.X, currentMouseState.Y, 1, 1)))
                {
                    Hover = true;
                    if (currentMouseState.LeftButton == ButtonState.Released && lastMouseState.LeftButton == ButtonState.Pressed)
                    {
                        try
                        {
                            ClickEvent(this, null);
                        }
                        catch { }
                    }
                }
                else
                    Hover = false;
            }

            lastMouseState = currentMouseState;
        }
    }
}
