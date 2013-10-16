using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RaceGameBase
{
    public class Credits
    {
        public event EventHandler ReturnEvent;

        private Rectangle backButtonRectangle;
        private Texture2D backButtonImage;
        private Texture2D backButtonHoverImage;
        private bool backButtonHover = false;

        private MouseState currentMouseState;
        private MouseState lastMouseState;

        private Texture2D creditsPanel;
        private Rectangle creditsPanelRectangle;



        public Credits()
        {
            Load(Game.contentManager);
        }

        private void Load(ContentManager Content)
        {
            backButtonImage = Content.Load<Texture2D>("Menu/back");
            backButtonHoverImage = Content.Load<Texture2D>("Menu/back_h");
            backButtonRectangle = new Rectangle(120, 250, backButtonImage.Width, backButtonImage.Height);
            creditsPanel = Content.Load<Texture2D>("Menu/creditspanel");
            creditsPanelRectangle = new Rectangle(630, 80, creditsPanel.Width, creditsPanel.Height);
        }

        public void Update()
        {
            currentMouseState = Mouse.GetState();

            //checkt als backbutton wordt ingedrukt
            if (backButtonRectangle.Intersects(new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1)))
            {
                backButtonHover = true;
                if (currentMouseState.LeftButton == ButtonState.Released && lastMouseState.LeftButton == ButtonState.Pressed)
                    ReturnEvent(this, new EventArgs());
            }
            else
                backButtonHover = false;

            if (Keyboard.GetState().GetPressedKeys().Length > 0)
                ReturnEvent(this, new EventArgs());
            lastMouseState = currentMouseState;
        }

        public void Draw()
        {
            Game.spriteBatch.Begin();

            //teken achtergrond, logo, panel
            Menu.DrawBasics();
            Game.spriteBatch.Draw(creditsPanel, creditsPanelRectangle, Color.White);
            
            //teken back button
            if (backButtonHover == false)
                Game.spriteBatch.Draw(backButtonImage, backButtonRectangle, Color.White);
            else
                Game.spriteBatch.Draw(backButtonHoverImage, backButtonRectangle, Color.White);

            Game.spriteBatch.End();
        }
    }
}
