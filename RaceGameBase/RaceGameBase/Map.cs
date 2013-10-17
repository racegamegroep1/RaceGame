using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace RaceGameBase
{
    public class Map
    {
        public event EventHandler returnEvent;

        private  List<Player> Players { get; set;}

        public Map(List<Player> players)
        {
            this.Players = players;
            Load(Game.contentManager);
        }

        void Load(ContentManager Content)
        {

        }

        public void Draw()
        {
            Game.spriteBatch.Begin();

            Game.spriteBatch.End();
        }

        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                returnEvent(this, new EventArgs());
        }
    }
}
