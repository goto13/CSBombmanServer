using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSBombmanServer
{
    public class You : Player
    {
        [NonSerialized]
        public string direction;
        [NonSerialized]
        public bool[] keyStates;
        [NonSerialized]
        public bool putBomb;


        public You(string name) : base(name)
        {
            direction = "";
            //４方向（上=0,下=1,左=2,右=3）＋Ｚキー=4
            keyStates = new bool[5] { false, false, false, false, false };
            putBomb = false;
        }
        public async override Task<ActionData> Action(string mapData)
        {
            string nextMove = "STAY";
            if (direction == "UP" || keyStates[0])
            {
                nextMove = "UP";
            }
            else if (direction == "DOWN" || keyStates[1])
            {
                nextMove = "DOWN";
            }
            else if (direction == "LEFT" || keyStates[2])
            {
                nextMove = "LEFT";
            }
            else if (direction == "RIGHT" || keyStates[3])
            {
                nextMove = "RIGHT";
            }
            ActionData result = new ActionData(this, nextMove, putBomb);
            direction = "";
            putBomb = false;
            return result;
        }
    }
}
