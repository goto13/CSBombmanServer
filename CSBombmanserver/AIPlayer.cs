using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSBombmanServer
{
    public class AIPlayer : Player
    {
        [NonSerialized]
        Random rand;

        public AIPlayer(string name) : base(name)
        {
            rand = new Random();
        }


        public async override Task<ActionData> Action(string mapData)
        {
            string[] moves = { "UP", "DOWN", "LEFT", "RIGHT" };
            return new ActionData(this, moves[rand.Next(0, 3)], false);
        }
    }
}
