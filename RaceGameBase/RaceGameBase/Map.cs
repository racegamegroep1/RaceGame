using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RaceGameBase
{
    public class Map
    {
        public event EventHandler returnEvent;

        private  List<Player> Players { get; set;}
        Texture2D map;
        Rectangle mapRectangle;
        Camera Camera;
        Player eele = new Player("Eele", new Car(100), 1, 1);
        
        public Map(List<Player> players)
        {
            this.Players = players;
            Load(Game.contentManager);
        }

        public void Load(ContentManager Content)
        {
            Camera = new Camera();
            eele.CarObject.Load(Content);
            map = Content.Load<Texture2D>("map");
            mapRectangle = new Rectangle(0, 0, map.Width, map.Height);
        }

        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                returnEvent(this, new EventArgs());
            eele.CarObject.Update(Camera);
            Camera.CameraControlls(Camera);
        }

        public void Draw()
        {
            Game.spriteBatch.Begin(SpriteSortMode.BackToFront,
                BlendState.AlphaBlend,
                null, null, null, null, Camera.Transform(Game.graphics.GraphicsDevice));
            eele.CarObject.Draw(Game.spriteBatch);
            Game.spriteBatch.Draw(map, mapRectangle, Color.White);
            Game.spriteBatch.End();
        }
    }
}
