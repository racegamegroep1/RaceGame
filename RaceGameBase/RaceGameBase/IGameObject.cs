using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RaceGameBase
{
    public interface IGameObject
    {
        Vector2 Position { get; set; }
        float Rotation { get; set; }

        void Draw();
        void Update();
    }
}
