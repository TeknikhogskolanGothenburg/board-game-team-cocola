using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class Ludo
    {
        public Ludo()
        {
            // Start a new Ludo game
        }

        Dice _dice = new Dice();

        public void RollDice()
        {
            _dice.RollDice();
        }

        public object GameStateModel { get; }

        public List<string> Players { get; }
        // list of players
        // which player is playing now
        // lasted dicevalue
        // where are all bricks placed on the board
        // move of brick
        // 
    }
}
