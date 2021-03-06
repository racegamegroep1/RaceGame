﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace RaceGameBase
{
    public class Menu
    {

        public event EventHandler<RaceGameBase.GameStateEventArgs> ClickEvent;

        private string[] MenuItems;
        private Texture2D[] MenuImages;
        private Texture2D[] MenuHoverImages;
        private Rectangle[] MenuRectangles;
        private bool[] MenuHover;
        private MouseState currentMouseState;
        private MouseState lastMouseState;

        public Menu(string[] menuItems)
        {
            MenuItems = menuItems;
            MenuImages = new Texture2D[menuItems.Length];
            MenuHoverImages = new Texture2D[menuItems.Length];
            MenuRectangles = new Rectangle[menuItems.Length];
            MenuHover = new bool[MenuItems.Length];
            Load(Game.contentManager);
        }

        private void Load(ContentManager Content)
        {            
            for (int i = 0; i < MenuItems.Length; i++)
            {
                    MenuImages[i] = Content.Load<Texture2D>("Menu/" + MenuItems[i].ToLower());
                    MenuHoverImages[i] = Content.Load<Texture2D>("Menu/" + MenuItems[i].ToLower() + "_h");
                    MenuRectangles[i] = new Rectangle(120, 250 + (i  * MenuImages[i].Height) + i, MenuImages[i].Width, MenuImages[i].Height);
            }
        }

        public void Draw()
        {
            Game.spriteBatch.Begin();
            DrawBasics();

            for (int i = 0; i < MenuItems.Length; i++)
            {
                if(MenuHover[i] == false)
                    Game.spriteBatch.Draw(MenuImages[i], MenuRectangles[i], Color.White);
                else
                    Game.spriteBatch.Draw(MenuHoverImages[i], MenuRectangles[i], Color.White);
            }
            
            Game.spriteBatch.End();
        }

        public static void DrawBasics()
        {
            Game.spriteBatch.Draw(Game.player.GetTexture(), new Rectangle(0, 0, Game.graphics.PreferredBackBufferWidth, Game.graphics.PreferredBackBufferHeight), Color.White);
            Game.spriteBatch.Draw(Game.logo, new Rectangle(0, 0, Game.logo.Width, Game.logo.Height), Color.White);
        }

        public void Update()
        {
            currentMouseState = Mouse.GetState();
            for (int i = 0; i < MenuItems.Length; i++)
            {
                if (MenuRectangles[i].Intersects(new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1)))
                    MenuHover[i] = true;
                else
                    MenuHover[i] = false;
            }
            if (lastMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released)
            {
                for (int i = 0; i < MenuItems.Length; i++)
                {
                    if (MenuRectangles[i].Intersects(new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1)))
                    {
                        switch (i)
                        {
                            case 0: ClickEvent(this, new GameStateEventArgs(GameState.Lobby)); break;
                            case 1: ClickEvent(this, new GameStateEventArgs(GameState.Options)); break;
                            case 2: ClickEvent(this, new GameStateEventArgs(GameState.Credits)); break;
                            case 3: Process.GetCurrentProcess().Kill(); break;
                        }
                    }
                    
                }
            }
            lastMouseState = currentMouseState;
        }
    }
}
