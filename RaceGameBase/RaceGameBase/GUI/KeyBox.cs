using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RaceGameBase
{
    /* 
     * Dit is de KeyBox class
     * Deze class word gebuikt in het optie menu
     * Dit is de textbox die de keys kan aanpassen
     */

    class KeyBox
    {
        public Rectangle MouseRectangle { get; set; }
        public MouseState MouseState { get; set; }
        public MouseState LastState { get; set; }

        public Rectangle Rec { get; set; }
        public string Name { get; set; }

        private bool _selected = false;

        public KeyBox(Rectangle rec, string name)
        {
            Rec = rec;
            Name = name;
        }

        public void Draw()
        {
            if (_selected == false)
            {
                Game.spriteBatch.Draw(Options.textbox, Rec, Color.White);
                Game.spriteBatch.DrawString(Options.smallFont, GetKeyName((int)Settings.GetKey(this.Name)), new Vector2(Rec.X + 3, Rec.Y + 2), Color.Black);
            }
            else
            {
                Game.spriteBatch.Draw(Options.textboxHover, Rec, Color.White);
                Game.spriteBatch.DrawString(Options.smallFont, "Enter a key", new Vector2(Rec.X + 3, Rec.Y + 2), Color.Black);
            }
            
        }

        public void Update()
        {
            MouseRectangle = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);
            MouseState = Mouse.GetState();

            if (LastState.LeftButton == ButtonState.Pressed && MouseState.LeftButton == ButtonState.Released)
            {
                if (MouseRectangle.Intersects(Rec))
                {
                    _selected = true;
                }
                else
                    _selected = false;
            }

            if (_selected == true)
            {
                if (Keyboard.GetState().GetPressedKeys().Length > 0)
                {
                    Settings.ChangeKey(this.Name, (int)Keyboard.GetState().GetPressedKeys()[0]);
                    _selected = false;
                }
            }

            LastState = MouseState;
            
        }

        private string GetKeyName(int id)
        {
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                if ((int)key == id)
                    return key.ToString();
            }
            return null;
        }

    }
}
