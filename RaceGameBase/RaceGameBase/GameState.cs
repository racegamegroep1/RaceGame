using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaceGameBase
{
    public enum GameState
    {
        Menu,
        Lobby,
        Options,
        Credits,
        Game
    }

    public class GameStateEventArgs : EventArgs
    {
        private GameState gameState;
        public GameStateEventArgs(GameState gamestate)
        {
            this.gameState = gamestate;
        }
        public GameState GetGameState()
        {
            return gameState;
        }
    }
}
