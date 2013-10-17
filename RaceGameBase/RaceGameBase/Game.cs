using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace RaceGameBase
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public static ContentManager contentManager;
        public static double totalMiliseconds;
        public static TimeSpan totalTimeSpan;

        public static GameState gameState = GameState.Menu;
        public static SpriteFont spriteFont;
        public static Texture2D filler;
        public static Texture2D background;
        public static Texture2D logo;
        public static Texture2D back;
        public static Texture2D backHover;
        public static Rectangle backRectangle;

        public static VideoPlayer player;
        public static Video video;
        public static Song lobbySong;

        public Menu menu;
        public Options options;
        public Lobby lobby;
        public Credits credits;
        public Map map;
        
        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            contentManager = Content;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            this.IsMouseVisible = true;
            ApplySettings();

            totalMiliseconds = 0;

            menu = new Menu(new string[] {"Play","Options","Credits","Exit"});
            menu.ClickEvent += new EventHandler<GameStateEventArgs>(Menu_ClickEvent);

            options = new Options();
            options.ReturnEvent += new EventHandler(ReturnEvent);

            lobby = new Lobby();
            lobby.ReturnEvent += new EventHandler(ReturnEvent);
            lobby.GameStateEvent += new EventHandler<GameStateEventArgs>(lobby_GameStateEvent);

            credits = new Credits();
            credits.ReturnEvent += new EventHandler(ReturnEvent);

            map = new Map(Player.Players);
            map.returnEvent += new EventHandler(map_returnEvent);
            base.Initialize();
        }

        private void ApplySettings()
        {
            if (Convert.ToBoolean(Settings.GetKey("Fullscreen")) == true)
                Game.graphics.ToggleFullScreen();
        }

        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = Content.Load<SpriteFont>("SpriteFont");
            filler = Content.Load<Texture2D>("filler");
            logo = Content.Load<Texture2D>("Menu/logo");
            back = Content.Load<Texture2D>("Menu/back");
            backHover = Content.Load<Texture2D>("Menu/back_h");
            backRectangle = new Rectangle(10, Game.graphics.PreferredBackBufferHeight - back.Height - 20, back.Width, back.Height);
            lobbySong = Content.Load<Song>("Menu/lobby");

            MediaPlayer.Play(lobbySong);


            if (Convert.ToBoolean(Settings.GetKey("Sound")) == false)
                MediaPlayer.Pause();

            player = new VideoPlayer();
            player.IsMuted = true;

            video = Content.Load<Video>("Menu/background");
            player.Play(video);
            player.IsLooped = true;

        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            totalMiliseconds = gameTime.TotalGameTime.TotalMilliseconds;
            totalTimeSpan = gameTime.TotalGameTime;

            switch (gameState)
            {
                case GameState.Menu:
                    menu.Update();
                    break;
                case GameState.Lobby:
                    lobby.Update();
                    break;
                case GameState.Options:
                    options.Update();
                    break;
                case GameState.Credits:
                    credits.Update();
                    break;
                case GameState.Game:
                    map.Update();
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            switch (gameState)
            {
                case GameState.Menu:
                    menu.Draw();
                    break;
                case GameState.Options:
                    options.Draw();
                    break;
                case GameState.Lobby:
                    lobby.Draw();
                    break;
                case GameState.Credits:
                    credits.Draw();
                    break;
                case GameState.Game:
                    map.Draw();
                    break;
            }

            base.Draw(gameTime);
        }

        public void Menu_ClickEvent(object sender, GameStateEventArgs e)
        {
            gameState = e.GetGameState();
        }

        public void ReturnEvent(object sender, EventArgs e)
        {
            gameState = GameState.Menu;
        }

        void lobby_GameStateEvent(object sender, GameStateEventArgs e)
        {
            gameState = e.GetGameState();
        }

        void map_returnEvent(object sender, EventArgs e)
        {
            gameState = GameState.Menu;
            Player.Players.Clear();
            lobby.ReorderRectangles();
        }
    }

}
