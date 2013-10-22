using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace RaceGameBase
{
    public class Car : IGameObject
    {
        public Texture2D[] texture = new Texture2D[2]; // Texture Array with stages of Car
        public Vector2 position; // position of car
        public float rotation = 0; // rotation of car
        public Rectangle rectangle; // rectangle of car
        public Vector2 origin;
        public float snelheid { get; set; }
        float minSpeed;
        float maxSpeed;
        float handling;
        public float posx;
        public float posy;
        float versnelling = 0.05f;
        public int health { get; set; }

        public Car(int Health)
        {
            health = Health;
        }

        public void Load(ContentManager Content)
        {
            // vars
            texture[0] = Content.Load<Texture2D>("car"); // default car
            texture[1] = Content.Load<Texture2D>("card"); // broken car, by health = 90
            position.X = 500;
            position.Y = 450;
            minSpeed = -2;
            maxSpeed = 3;
            handling = 0.05f;
        }
        
        public void Update(Camera cCamera)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture[0].Width, texture[0].Height);
            origin = new Vector2(rectangle.Width / 2, rectangle.Height / 2);
            
            cCamera.Position = position;
            
            KeyboardState keyState = Keyboard.GetState();

            // When pressing Up (gas)
            if (keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up))
            {
                if (snelheid < maxSpeed)
                {
                    snelheid = snelheid + versnelling;
                }
                else
                {
                    snelheid = maxSpeed;
                }
            }
            else if (keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down))
            {
                if (snelheid > 0)
                {
                    snelheid = snelheid - versnelling;
                }
                else
                {
                    snelheid = 0;
                }
            }

            // When pressing Down (Backwards)
            if (keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down))
            {
                if (snelheid > minSpeed)
                {
                    snelheid = snelheid - versnelling;
                }
                else
                {
                    snelheid = minSpeed;
                }
            }
            else if (keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up))
            {
                if (snelheid < 0)
                {
                    snelheid = snelheid + versnelling;
                }
                else
                {
                    snelheid = 0;
                }
            }

            // je kan niet sturen bij een speed van 0
            if (snelheid != 0)
            {
                if (keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right))
                {
                    rotation += handling;
                    cCamera.Rotation -= handling;
                }

                if (keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left))
                {
                    rotation -= handling;
                    cCamera.Rotation += handling;
                }
            }

            posx = (float)Math.Cos(rotation + 1.5 * Math.PI) * 5 * snelheid;
            posy = (float)Math.Sin(rotation + 1.5 * Math.PI) * 5 * snelheid;

            // Update position of car and Camera
            position = new Vector2(position.X + posx, position.Y + posy);
            cCamera.Move(new Vector2(posx, posy));
            
        }

        // draw functie voor auto
        public void Draw(SpriteBatch spriteBatch)
        {
            switch (health)
            {
                case 90:
                    spriteBatch.Draw(texture[1], position, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0);
                    break;
                default:
                    spriteBatch.Draw(texture[0], position, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0);
                    break;
            }
        }

        public static Vector2 GenerateSpawn()
        {
            return Vector2.Zero;
        }
    }
}