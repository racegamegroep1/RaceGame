using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace RaceGameBase
{
    public class Options
    {
        public event EventHandler ReturnEvent;

        private Rectangle selectFullscreen;
        private Rectangle selectSound;
        private Rectangle backButtonRectangle;
        private Texture2D backButtonImage;
        private Texture2D backButtonHoverImage;
        private bool backButtonHover = false;
        private Texture2D optionPanel;
        private MouseState lastMouseState;
        private MouseState currentMouseState;

        public Options()
        {
            selectFullscreen = new Rectangle(650, 90, 15, 15);
            selectSound = new Rectangle(650, 120, 15, 15);
            Load(Game.contentManager);
        }

        private void Load(ContentManager Content)
        {
            backButtonImage = Content.Load<Texture2D>("Menu/back");
            backButtonHoverImage = Content.Load<Texture2D>("Menu/back_h");

            backButtonRectangle = new Rectangle(120, 250, backButtonImage.Width, backButtonImage.Height);

            optionPanel = Content.Load<Texture2D>("Menu/optionpanel");
        }

        public void Update()
        {
            currentMouseState = Mouse.GetState();

            if (backButtonRectangle.Intersects(new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1)))
            {
                backButtonHover = true;
                if (currentMouseState.LeftButton == ButtonState.Released && lastMouseState.LeftButton == ButtonState.Pressed)
                    ReturnEvent(this, new EventArgs());
            }
            else
                backButtonHover = false;

            if(selectFullscreen.Intersects(new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1)))
            {
                if(currentMouseState.LeftButton == ButtonState.Released && lastMouseState.LeftButton == ButtonState.Pressed)
                {
                    Game.graphics.ToggleFullScreen();
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                ReturnEvent(this, new EventArgs());

            lastMouseState = currentMouseState;
        }

        public void Draw()
        {
            Game.spriteBatch.Begin();
            Game.spriteBatch.Draw(Game.player.GetTexture(), new Rectangle(0,0, Game.graphics.PreferredBackBufferWidth, Game.graphics.PreferredBackBufferHeight), Color.White);
            Game.spriteBatch.Draw(Game.logo, new Rectangle(0, 0, Game.logo.Width, Game.logo.Height), Color.White);
            Game.spriteBatch.Draw(optionPanel, new Rectangle(backButtonRectangle.X + backButtonRectangle.Width + 1, 35, optionPanel.Width, optionPanel.Height), Color.White);

            if(backButtonHover == false)
                Game.spriteBatch.Draw(backButtonImage, backButtonRectangle, Color.White);
            else
                Game.spriteBatch.Draw(backButtonHoverImage, backButtonRectangle, Color.White);

            Game.spriteBatch.Draw(Game.filler, selectSound, Color.Black);
            Game.spriteBatch.Draw(Game.filler, selectFullscreen, Color.Black);
            Game.spriteBatch.End();
        }
    }
}
