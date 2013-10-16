using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaceGameBase
{
    //De states van de game
    public enum GameState
    {
        Menu,
        Lobby,
        Options,
        Credits,
        Game
    }

    //Eventargs zodat de gamestate kan worden veranderd na een event
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
