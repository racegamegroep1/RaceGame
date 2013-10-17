using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using RaceGameBase.GUI;
using RaceGameBase.Cars;

namespace RaceGameBase
{
    public class Lobby
    {
        public event EventHandler ReturnEvent;
        public event EventHandler<GameStateEventArgs> GameStateEvent;

        private MouseState _currentMouseState;
        private MouseState _lastMouseState;

        private Button _startButton;
        private Button _backButton;
        private Button _addPlayerButton;
        private Button _nextCarButton;
        private Button _deleteButton;

        private Texture2D _lobbyPanel;
        private Rectangle _lobbyPanelRectangle;

        private Texture2D _bmw;
        private Texture2D _lambor;
        private Rectangle _carPreviewRectangle;
        private int _carPreviewIndex = 0;
        private int _carPreviewMax = 2;

        private Texture2D _xboxHover;
        private Texture2D _keyboardHover;
        private Texture2D _aiHover;
        private Rectangle _xboxRectangle;
        private Rectangle _keyboardRectangle;
        private Rectangle _aiRectangle;
        private int _selectedIndex = -1;
        private int _hoverIndex = -1;

        private bool _userWarning = false;
        private bool _controlWarning = false;
        private Texture2D _warning;

        private TextBox _usernameTextBox;

        private List<Rectangle> _playerRectangles = new List<Rectangle> { };
        private int _selectedPlayer = -1;

        public Lobby()
        {
            Load(Game.contentManager);
            if (Player.Players == null)
                Player.Players = new List<Player> { };
        }

        public void Load(ContentManager Content)
        {
            _startButton = new Button("Menu/Lobby/start", 120, 250);
            _startButton.Enabled = false;
            _startButton.ClickEvent += new EventHandler(_startButton_ClickEvent);

            _backButton = new Button("Menu/back", 120, 304);
            _backButton.ClickEvent += new EventHandler(_backButton_ClickEvent);

            _addPlayerButton = new Button("Menu/Lobby/addplayer", _backButton.Rec.X + _backButton.Rec.Width + 1, 35);
            _addPlayerButton.ClickEvent += new EventHandler(_addPlayerButton_ClickEvent);

            _nextCarButton = new Button("Menu/Lobby/nextcar", _addPlayerButton.Rec.X + _addPlayerButton.Rec.Width + 1, 35);
            _nextCarButton.ClickEvent += new EventHandler(_nextCarButton_ClickEvent);

            _lobbyPanel = Content.Load<Texture2D>("Menu/Lobby/lobbyPanel");
            _lobbyPanelRectangle = new Rectangle(_backButton.Rec.X + _backButton.Rec.Width + 1, 89, _lobbyPanel.Width, _lobbyPanel.Height);

            _deleteButton = new Button("Menu/Lobby/delete", _backButton.Rec.X + _backButton.Rec.Width + 1, _lobbyPanelRectangle.Y + _lobbyPanelRectangle.Height + 1);
            _deleteButton.ClickEvent += new EventHandler(_deleteButton_ClickEvent);

            _bmw = Content.Load<Texture2D>("Menu/Lobby/Cars/bmw");
            _lambor = Content.Load<Texture2D>("Menu/Lobby/Cars/lambor");
            _carPreviewRectangle = new Rectangle(800, 140, _bmw.Width, _bmw.Height);

            _usernameTextBox = new TextBox(new Vector2(495, 140));

            _xboxRectangle = new Rectangle(451, 230, 110, 127);
            _keyboardRectangle = new Rectangle(_xboxRectangle.X + _xboxRectangle.Width, _xboxRectangle.Y, 110, 127);
            _aiRectangle = new Rectangle(_keyboardRectangle.X + _keyboardRectangle.Width, _xboxRectangle.Y, 110, 127);

            _xboxHover = Content.Load<Texture2D>("Menu/Lobby/xbox_h");
            _keyboardHover = Content.Load<Texture2D>("Menu/Lobby/keyboard_h");
            _aiHover = Content.Load<Texture2D>("Menu/Lobby/ai_h");

            _warning = Content.Load<Texture2D>("Menu/Lobby/warning");
        }

        public void Update()
        {
            _currentMouseState = Mouse.GetState();

            _backButton.Update();
            _startButton.Update();
            _addPlayerButton.Update();
            _nextCarButton.Update();
            _deleteButton.Update();

            _deleteButton.Enabled = false;

            if (_xboxRectangle.Intersects(new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1)))
            {     
                _hoverIndex = 0;
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    _selectedIndex = 0;
            }
            else if (_keyboardRectangle.Intersects(new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1)))
            {
                _hoverIndex = 1;
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    _selectedIndex = 1;
            }
            else if (_aiRectangle.Intersects(new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1)))
            {
                _hoverIndex = 2;
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    _selectedIndex = 2;
            }
            else
                _hoverIndex = -1;

            _usernameTextBox.Update();

            if (Player.Players.Count > 0)
                _deleteButton.Enabled = true;
            else
                _deleteButton.Enabled = false;

            if (Player.Players.Count > 1)
                _startButton.Enabled = true;
            else
                _startButton.Enabled = false;

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                ReturnEvent(this, new EventArgs());

            _lastMouseState = _currentMouseState;
        }

        public void Draw()
        {
            Game.spriteBatch.Begin();
            Menu.DrawBasics();
            Game.spriteBatch.Draw(_lobbyPanel, _lobbyPanelRectangle, Color.White);

            _backButton.Draw();
            _startButton.Draw();
            _addPlayerButton.Draw();
            _nextCarButton.Draw();
            _deleteButton.Draw();

            switch (_carPreviewIndex)
            {
                case 0: Game.spriteBatch.Draw(_bmw, _carPreviewRectangle, Color.White); break;
                case 1: Game.spriteBatch.Draw(_lambor, _carPreviewRectangle, Color.White); break;
            }

            _usernameTextBox.Draw();

            switch (_hoverIndex)
            {
                case 0: Game.spriteBatch.Draw(_xboxHover, _xboxRectangle, Color.White); break;
                case 1: Game.spriteBatch.Draw(_keyboardHover, _keyboardRectangle, Color.White); break;
                case 2: Game.spriteBatch.Draw(_aiHover, _aiRectangle, Color.White); break;
            }

            switch (_selectedIndex)
            {
                case 0: Game.spriteBatch.Draw(_xboxHover, _xboxRectangle, Color.White); break;
                case 1: Game.spriteBatch.Draw(_keyboardHover, _keyboardRectangle, Color.White); break;
                case 2: Game.spriteBatch.Draw(_aiHover, _aiRectangle, Color.White); break;
            }

            for (int i = 0; i < Player.Players.Count; i++)
            {
                string control = "";

                if(Player.Players[i].UseKeyboard)
                    control = "Keyboard";
                else
                {
                    if(Player.Players[i].IsAI)
                        control = "AI";
                    else
                        control = "Xbox";
                }

                if(i % 2 == 0)
                    Game.spriteBatch.Draw(Game.filler, _playerRectangles[i], Color.FromNonPremultiplied(255,255,255,80));
                else
                    Game.spriteBatch.Draw(Game.filler, _playerRectangles[i], Color.FromNonPremultiplied(255, 255, 255, 120));

                if (_playerRectangles[i].Intersects(new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1)))
                {
                    Game.spriteBatch.Draw(Game.filler, _playerRectangles[i], Color.FromNonPremultiplied(255, 255, 255, 50));
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                        _selectedPlayer = i;
                }

                if(_selectedPlayer == i)
                    Game.spriteBatch.Draw(Game.filler, _playerRectangles[i], Color.FromNonPremultiplied(255, 255, 255, 240));

                Game.spriteBatch.DrawString(Game.spriteFont, Player.Players[i].Name + " " + control, new Vector2(480 + 1, 371 + (50 * i) + 1), Color.Blue);
                Game.spriteBatch.DrawString(Game.spriteFont, Player.Players[i].Name + " " + control, new Vector2(480, 371 + (50 * i)), Color.White);
            }

            //Laat de waarschuwingen zien
            if (_userWarning)
                Game.spriteBatch.Draw(_warning, new Rectangle(451, 89, _warning.Width, _warning.Height), Color.White);
            if (_controlWarning)
                Game.spriteBatch.Draw(_warning, new Rectangle(451, 203, _warning.Width, _warning.Height), Color.White);

            Game.spriteBatch.End();
        }

        private void _backButton_ClickEvent(object sender, EventArgs e)
        {
            ReturnEvent(this, new EventArgs());
        }

        void _nextCarButton_ClickEvent(object sender, EventArgs e)
        {
            _carPreviewIndex++;
            if (_carPreviewIndex > (_carPreviewMax - 1))
                _carPreviewIndex = 0;
        }

        void _addPlayerButton_ClickEvent(object sender, EventArgs e)
        {
            if (_usernameTextBox.Text == "")
                _userWarning = true;
            else if (_selectedIndex == -1)
                _controlWarning = true;
            else
            {
                Car c = new Car();

                switch (_carPreviewIndex)
                {
                    case 0: c = new BMW(); break;
                    case 1: c = new Lambor(); break;
                }

                Player p = new Player(_usernameTextBox.Text, c, _selectedIndex, Player.GetInputId(Player.Players, 1));
                Player.Players.Add(p);
                _playerRectangles.Add(new Rectangle(451, 358 + (_playerRectangles.Count * 50), 827, 50));

                _selectedIndex = -1;
                _usernameTextBox.Text = "";
            }
        }
        void _deleteButton_ClickEvent(object sender, EventArgs e)
        {
            if (_selectedPlayer >= 0 && _selectedPlayer < Player.Players.Count)
            {
                Player.Players.RemoveAt(_selectedPlayer);
                ReorderRectangles();               
            }
        }

        void _startButton_ClickEvent(object sender, EventArgs e)
        {
            GameStateEvent(this, new GameStateEventArgs(GameState.Game));
        }

        public void ReorderRectangles()
        {
            _playerRectangles.Clear();
            for (int i = 0; i < Player.Players.Count; i++)
            {
                _playerRectangles.Add(new Rectangle(451, 358 + (i * 50), 827, 50));
            }
        }
    }
}
