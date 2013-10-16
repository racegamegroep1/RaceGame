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
    public class Lobby
    {
        public event EventHandler ReturnEvent;

        private MouseState currentMouseState;
        private MouseState lastMouseState;

        private Button startButton;
        private Button backButton;
        private Button addPlayerButton;
        private Button nextCarButton;
        private Button deleteButton;

        private Texture2D lobbyPanel;
        private Rectangle lobbyPanelRectangle;

        private Texture2D bmw;
        private Texture2D lambor;
        private Rectangle carPreviewRectangle;
        private int carPreviewIndex = 0;
        private int carPreviewMax = 2;

        public Lobby()
        {
            Load(Game.contentManager);
        }

        public void Load(ContentManager Content)
        {
            startButton = new Button("Menu/Lobby/start", 120, 250);

            backButton = new Button("Menu/back", 120, 304);
            backButton.ClickEvent += new EventHandler(backButton_ClickEvent);

            addPlayerButton = new Button("Menu/Lobby/addplayer", backButton.Rec.X + backButton.Rec.Width + 1, 35);

            nextCarButton = new Button("Menu/Lobby/nextcar", addPlayerButton.Rec.X + addPlayerButton.Rec.Width + 1, 35);
            nextCarButton.ClickEvent += new EventHandler(nextCarButton_ClickEvent);

            lobbyPanel = Content.Load<Texture2D>("Menu/Lobby/lobbypanel");
            lobbyPanelRectangle = new Rectangle(backButton.Rec.X + backButton.Rec.Width + 1, 89, lobbyPanel.Width, lobbyPanel.Height);

            deleteButton = new Button("Menu/Lobby/delete", backButton.Rec.X + backButton.Rec.Width + 1, lobbyPanelRectangle.Y + lobbyPanelRectangle.Height + 1);

            bmw = Content.Load<Texture2D>("Menu/Lobby/Cars/bmw");
            lambor = Content.Load<Texture2D>("Menu/Lobby/Cars/lambor");
            carPreviewRectangle = new Rectangle(800, 140, bmw.Width, bmw.Height);

        }

        public void Update()
        {
            currentMouseState = Mouse.GetState();

            backButton.Update();
            startButton.Update();
            addPlayerButton.Update();
            nextCarButton.Update();
            deleteButton.Update();

            deleteButton.Enabled = false;

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                ReturnEvent(this, new EventArgs());

            lastMouseState = currentMouseState;
        }

        public void Draw()
        {
            Game.spriteBatch.Begin();
            Menu.DrawBasics();
            Game.spriteBatch.Draw(lobbyPanel, lobbyPanelRectangle, Color.White);

            backButton.Draw();
            startButton.Draw();
            addPlayerButton.Draw();
            nextCarButton.Draw();
            deleteButton.Draw();

            switch (carPreviewIndex)
            {
                case 0: Game.spriteBatch.Draw(bmw, carPreviewRectangle, Color.White); break;
                case 1: Game.spriteBatch.Draw(lambor, carPreviewRectangle, Color.White); break;
            }

            Game.spriteBatch.End();
        }

        private void backButton_ClickEvent(object sender, EventArgs e)
        {
            ReturnEvent(this, new EventArgs());
        }

        void nextCarButton_ClickEvent(object sender, EventArgs e)
        {
            carPreviewIndex++;
            if (carPreviewIndex > (carPreviewMax - 1))
                carPreviewIndex = 0;
        }
    }
}
