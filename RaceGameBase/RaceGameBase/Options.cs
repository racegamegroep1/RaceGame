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

        public static SpriteFont smallFont;
        public static Texture2D textbox;
        public static Texture2D textboxHover;

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

        private KeyBox player1LeftKeyBox;
        private KeyBox player1RightKeyBox;
        private KeyBox player1GasKeyBox;
        private KeyBox player1BrakeKeyBox;
        private KeyBox player1NitroKeyBox;

        private KeyBox player2LeftKeyBox;
        private KeyBox player2RightKeyBox;
        private KeyBox player2GasKeyBox;
        private KeyBox player2BrakeKeyBox;
        private KeyBox player2NitroKeyBox;

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

            player1LeftKeyBox = new KeyBox(player1LeftRectangle, "P1left");
            player1RightKeyBox = new KeyBox(player1RightRectangle, "P1right");
            player1GasKeyBox = new KeyBox(player1GasRectangle, "P1gas");
            player1BrakeKeyBox = new KeyBox(player1BrakeRectangle, "P1brake");
            player1NitroKeyBox = new KeyBox(player1NitroRectangle, "P1nitro");

            player2LeftKeyBox = new KeyBox(player2LeftRectangle, "P2left");
            player2RightKeyBox = new KeyBox(player2RightRectangle, "P2right");
            player2GasKeyBox = new KeyBox(player2GasRectangle, "P2gas");
            player2BrakeKeyBox = new KeyBox(player2BrakeRectangle, "P2brake");
            player2NitroKeyBox = new KeyBox(player2NitroRectangle, "P2nitro");

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

            //checkt als backbutton wordt ingedrukt
            if (backButtonRectangle.Intersects(new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1)))
            {
                backButtonHover = true;
                if (currentMouseState.LeftButton == ButtonState.Released && lastMouseState.LeftButton == ButtonState.Pressed)
                    ReturnEvent(this, new EventArgs());
            }
            else
                backButtonHover = false;

            //fullscreen check
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

            //sound check
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

            //roep de update methode voor de Keyboxen aan
            player1LeftKeyBox.Update();
            player1RightKeyBox.Update();
            player1GasKeyBox.Update();
            player1BrakeKeyBox.Update();
            player1NitroKeyBox.Update();

            player2LeftKeyBox.Update();
            player2RightKeyBox.Update();
            player2GasKeyBox.Update();
            player2BrakeKeyBox.Update();
            player2NitroKeyBox.Update();

            //ga terug naar menu als escape wordt ingedrukt
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                ReturnEvent(this, new EventArgs());

            lastMouseState = currentMouseState;
        }

        public void Draw()
        {
            Game.spriteBatch.Begin();
            
            //teken achtergrond, logo, panel
            Menu.DrawBasics();
            Game.spriteBatch.Draw(optionPanel, new Rectangle(backButtonRectangle.X + backButtonRectangle.Width + 1, 35, optionPanel.Width, optionPanel.Height), Color.White);

            //teken back button
            if(backButtonHover == false)
                Game.spriteBatch.Draw(backButtonImage, backButtonRectangle, Color.White);
            else
                Game.spriteBatch.Draw(backButtonHoverImage, backButtonRectangle, Color.White);

            //teken fullscreen checkbox
            if (fullscreenState == 0)
                Game.spriteBatch.Draw(check, selectFullscreen, Color.White);
            else if (fullscreenState == 1)
                Game.spriteBatch.Draw(checkHover, selectFullscreen, Color.White);
            else
                Game.spriteBatch.Draw(checkChecked, selectFullscreen, Color.White);
            
            //teken sound checkbox
            if (soundState == 0)
                Game.spriteBatch.Draw(check, selectSound, Color.White);
            else if (soundState == 1)
                Game.spriteBatch.Draw(checkHover, selectSound, Color.White);
            else
                Game.spriteBatch.Draw(checkChecked, selectSound, Color.White);

            Game.spriteBatch.Draw(checkHover, selectMusic, Color.White);
            Game.spriteBatch.Draw(checkHover, selectEffects, Color.White);

            //Teken de KeyBoxes
            player1LeftKeyBox.Draw();
            player1RightKeyBox.Draw();
            player1GasKeyBox.Draw();
            player1BrakeKeyBox.Draw();
            player1NitroKeyBox.Draw();

            player2LeftKeyBox.Draw();
            player2RightKeyBox.Draw();
            player2GasKeyBox.Draw();
            player2BrakeKeyBox.Draw();
            player2NitroKeyBox.Draw();

            Game.spriteBatch.End();
        }
    }
}
