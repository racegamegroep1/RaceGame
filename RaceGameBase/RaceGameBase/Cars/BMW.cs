using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace RaceGameBase
{
    class BMW : Car
    {      
        public BMW()
        {
            maxSpeed = 230;
            handling = 5;
            //texture = Game.contentManager.Load<Texture2D>("");
        }
    }
}
