using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RaceGameBase.GUI
{
    public class TextBox
    {
        public Vector2 Position { get; set; }
        public string Text { get; set; }
        
        private KeyboardState _currentKeyboardState;
        private KeyboardState _lastKeyboardState;
        private Rectangle _Rec;
        private double _countDown;

        private Texture2D _image;
        private Texture2D _hoverImage;
        private Vector2 _textPosition;
        private bool _selected = false;

        public TextBox(Vector2 position)
        {
            Position = position;
            _textPosition = new Vector2(Position.X + 10, Position.Y + 8);
            Text = "";
            _countDown = Game.totalMiliseconds;
            Load(Game.contentManager);
        }

        private void Load(ContentManager Content)
        {
            _image = Content.Load<Texture2D>("Menu/Lobby/textbox2");
            _hoverImage = Content.Load<Texture2D>("Menu/Lobby/textbox2_h");
            _Rec = new Rectangle((int)Position.X, (int)Position.Y, _image.Width, _image.Height);
            
        }

        public void Draw()
        {
            if (_selected == false)
            {
                Game.spriteBatch.Draw(_image, _Rec, Color.White);
                Game.spriteBatch.DrawString(Game.spriteFont, Text, _textPosition, Color.Black);
            }
            else
            {
                Game.spriteBatch.Draw(_hoverImage, _Rec, Color.White);
                Game.spriteBatch.DrawString(Game.spriteFont, Text, _textPosition, Color.Black);
            }
        }

        public void Update()
        {            
            UpdateText();
        }

        public void UpdateText()
        {
            _currentKeyboardState = Keyboard.GetState();

            if (_selected)
            {
                if (_currentKeyboardState.GetPressedKeys().Length > 0)
                {
                    if (_currentKeyboardState != _lastKeyboardState || Game.totalMiliseconds > _countDown)
                    {
                        if (((int)_currentKeyboardState.GetPressedKeys()[0] >= 48 && (int)_currentKeyboardState.GetPressedKeys()[0] <= 57) || ((int)_currentKeyboardState.GetPressedKeys()[0] >= 65 && (int)_currentKeyboardState.GetPressedKeys()[0] <= 90) || (int)_currentKeyboardState.GetPressedKeys()[0] == 160)
                        {
                            if (_currentKeyboardState.GetPressedKeys()[0].ToString().Length > 1 && (int)_currentKeyboardState.GetPressedKeys()[0] != 160)
                            {
                                if(Text.Length <= 12)
                                    Text += _currentKeyboardState.GetPressedKeys()[0].ToString().Replace("D", "");
                            }
                            else
                            {
                                if (_currentKeyboardState.GetPressedKeys().Length > 1)
                                {
                                    if ((int)_currentKeyboardState.GetPressedKeys()[0] == 160)
                                    {
                                        if (Text.Length <= 12)
                                            Text += _currentKeyboardState.GetPressedKeys()[1];
                                    }
                                    else if ((int)_currentKeyboardState.GetPressedKeys()[1] == 160)
                                    {
                                        if(Text.Length <= 12)
                                            Text += _currentKeyboardState.GetPressedKeys()[0];
                                    }
                                }
                                else
                                {
                                    if ((int)_currentKeyboardState.GetPressedKeys()[0] != 160)
                                    {
                                        if (Text.Length <= 12)
                                            Text += _currentKeyboardState.GetPressedKeys()[0].ToString().ToLower();
                                    }
                                }
                            }
                            _countDown = Game.totalMiliseconds + 175;
                        }
                        else if ((int)_currentKeyboardState.GetPressedKeys()[0] == 8)
                        {
                            if (Text.Length > 0)
                            {
                                Text = Text.Substring(0, Text.Length - 1);
                                _countDown = Game.totalMiliseconds + 120;
                            }
                        }
                    }
                    _lastKeyboardState = _currentKeyboardState;
                }
            }

            if (new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1).Intersects(_Rec))
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    _selected = true;
            }
            else
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    _selected = false;
            }
                
            

        }

    }
}
