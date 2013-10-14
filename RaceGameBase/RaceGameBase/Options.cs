using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace RaceGameBase
{
    public class Options
    {
        public event EventHandler ReturnEvent;

        private SpriteFont smallFont;

        private Rectangle selectFullscreen;
        private int fullscreenState = 0;

        private Rectangle selectSound;
        private int soundState = 0;

        private Rectangle selectMusic;
        private Rectangle selectEffects;

        private Rectangle backButtonRectangle;
        private Texture2D backButtonImage;
        private Texture2D backButtonHoverImage;

        private bool backButtonHover = false;
        private Texture2D optionPanel;

        private MouseState lastMouseState;
        private MouseState currentMouseState;

        private Texture2D check;
        private Texture2D checkHover;
        private Texture2D checkChecked;

        private Texture2D textbox;
        private Texture2D textboxHover;

        private bool selected = false;
        private Rectangle player1LeftRectangle;
        private Rectangle player1RightRectangle;
        private Rectangle player1GasRectangle;
        private Rectangle player1BrakeRectangle;
        private Rectangle player1NitroRectangle;

        private Rectangle player2LeftRectangle;
        private Rectangle player2RightRectangle;
        private Rectangle player2GasRectangle;
        private Rectangle player2BrakeRectangle;
        private Rectangle player2NitroRectangle;

        public Options()
        {
            selectFullscreen = new Rectangle(650, 90, 15, 15);
            selectSound = new Rectangle(650, 120, 15, 15);
            selectMusic = new Rectangle(650, 150, 15, 15);
            selectEffects = new Rectangle(650, 180, 15, 15);

            player1LeftRectangle = new Rectangle(650, 265, 80, 20);
            player1RightRectangle = new Rectangle(650, 295, 80, 20);
            player1GasRectangle = new Rectangle(650, 325, 80, 20);
            player1BrakeRectangle = new Rectangle(650, 355, 80, 20);
            player1NitroRectangle = new Rectangle(650, 385, 80, 20);

            player2LeftRectangle = new Rectangle(1050, 265, 80, 20);
            player2RightRectangle = new Rectangle(1050, 295, 80, 20);
            player2GasRectangle = new Rectangle(1050, 325, 80, 20);
            player2BrakeRectangle = new Rectangle(1050, 355, 80, 20);
            player2NitroRectangle = new Rectangle(1050, 385, 80, 20);

            Load(Game.contentManager);
        }

        private void Load(ContentManager Content)
        {
            smallFont = Content.Load<SpriteFont>("SmallFont");

            backButtonImage = Content.Load<Texture2D>("Menu/back");
            backButtonHoverImage = Content.Load<Texture2D>("Menu/back_h");

            backButtonRectangle = new Rectangle(120, 250, backButtonImage.Width, backButtonImage.Height);

            optionPanel = Content.Load<Texture2D>("Menu/optionpanel");
            check = Content.Load<Texture2D>("Menu/check");
            checkHover = Content.Load<Texture2D>("Menu/check_h");
            checkChecked = Content.Load<Texture2D>("Menu/check_c");

            textbox = Content.Load<Texture2D>("Menu/textbox");
            textboxHover = Content.Load<Texture2D>("Menu/textbox_h");
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

            if (selectFullscreen.Intersects(new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1)))
            {
                fullscreenState = 1;
                if (currentMouseState.LeftButton == ButtonState.Released && lastMouseState.LeftButton == ButtonState.Pressed)
                {
                    Game.graphics.ToggleFullScreen();
                    Settings.ChangeKey("Fullscreen", Game.graphics.IsFullScreen);

                }
            }
            else
            {
                if (Game.graphics.IsFullScreen)
                    fullscreenState = 2;
                else
                    fullscreenState = 0;
            }

            if (selectSound.Intersects(new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1)))
            {
                soundState = 1;
                if (currentMouseState.LeftButton == ButtonState.Released && lastMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (MediaPlayer.State == MediaState.Playing)
                    {
                        MediaPlayer.Pause();
                        Settings.ChangeKey("Sound", false);
                    }
                    else
                    {
                        MediaPlayer.Resume();
                        Settings.ChangeKey("Sound", true);
                    }
                }
            }
            else
            {
                if (MediaPlayer.State == MediaState.Playing)
                    soundState = 2;
                else
                    soundState = 0;
            }

            if (selected == false && lastMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released)
            {
                Rectangle mouseRectangle = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);

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

            if (fullscreenState == 0)
                Game.spriteBatch.Draw(check, selectFullscreen, Color.White);
            else if (fullscreenState == 1)
                Game.spriteBatch.Draw(checkHover, selectFullscreen, Color.White);
            else
                Game.spriteBatch.Draw(checkChecked, selectFullscreen, Color.White);


            if (soundState == 0)
                Game.spriteBatch.Draw(check, selectSound, Color.White);
            else if (soundState == 1)
                Game.spriteBatch.Draw(checkHover, selectSound, Color.White);
            else
                Game.spriteBatch.Draw(checkChecked, selectSound, Color.White);

            Game.spriteBatch.Draw(checkHover, selectMusic, Color.White);
            Game.spriteBatch.Draw(checkHover, selectEffects, Color.White);

            Game.spriteBatch.Draw(textbox, player1LeftRectangle, Color.White);
            Game.spriteBatch.DrawString(smallFont, "Left", new Vector2(player1LeftRectangle.X + 3, player1LeftRectangle.Y + 2), Color.Black);
            Game.spriteBatch.Draw(textbox, player1RightRectangle, Color.White);
            Game.spriteBatch.DrawString(smallFont, "Right", new Vector2(player1RightRectangle.X + 3, player1RightRectangle.Y + 2), Color.Black);
            Game.spriteBatch.Draw(textbox, player1GasRectangle, Color.White);
            Game.spriteBatch.DrawString(smallFont, "Up", new Vector2(player1GasRectangle.X + 3, player1GasRectangle.Y + 2), Color.Black);
            Game.spriteBatch.Draw(textbox, player1BrakeRectangle, Color.White);
            Game.spriteBatch.DrawString(smallFont, "Down", new Vector2(player1BrakeRectangle.X + 3, player1BrakeRectangle.Y + 2), Color.Black);
            Game.spriteBatch.Draw(textbox, player1NitroRectangle, Color.White);
            Game.spriteBatch.DrawString(smallFont, "R Shift", new Vector2(player1NitroRectangle.X + 3, player1NitroRectangle.Y + 2), Color.Black);

            Game.spriteBatch.Draw(textbox, player2LeftRectangle, Color.White);
            Game.spriteBatch.DrawString(smallFont, "A", new Vector2(player2LeftRectangle.X + 3, player2LeftRectangle.Y + 2), Color.Black);
            Game.spriteBatch.Draw(textbox, player2RightRectangle, Color.White);
            Game.spriteBatch.DrawString(smallFont, "D", new Vector2(player2RightRectangle.X + 3, player2RightRectangle.Y + 2), Color.Black);
            Game.spriteBatch.Draw(textbox, player2GasRectangle, Color.White);
            Game.spriteBatch.DrawString(smallFont, "W", new Vector2(player2GasRectangle.X + 3, player2GasRectangle.Y + 2), Color.Black);
            Game.spriteBatch.Draw(textbox, player2BrakeRectangle, Color.White);
            Game.spriteBatch.DrawString(smallFont, "S", new Vector2(player2BrakeRectangle.X + 3, player2BrakeRectangle.Y + 2), Color.Black);
            Game.spriteBatch.Draw(textbox, player2NitroRectangle, Color.White);
            Game.spriteBatch.DrawString(smallFont, "L  Shift", new Vector2(player2NitroRectangle.X + 3, player2NitroRectangle.Y + 2), Color.Black);

            Game.spriteBatch.End();
        }
    }
}
