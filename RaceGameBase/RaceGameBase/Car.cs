using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RaceGameBase
{
    public class Car : IGameObject
    {
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        
        protected int maxSpeed;
        protected int handling;
        protected Texture2D texture;

        public Car()
        {

        }

        public void Update()
        {

        }

        public void Draw()
        {
            Game.spriteBatch.Begin();

            Game.spriteBatch.End();
        }

        public static Vector2 GenerateSpawn()
        {
            return Vector2.Zero;
        }
    }
}
