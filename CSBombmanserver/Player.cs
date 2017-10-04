using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CSBombmanServer
{
    public class Player
    {
        public const int DEFAULT_POWER = 2;
        public const int DEFAULT_BOMB_LIMIT = 2;
        [JsonProperty("name")]
        public string Name { get; set; }
        public Position pos;
        public int power;
        public int setBombLimit;
        public char ch;
        public bool isAlive;
        public int setBombCount;
        public int totalSetBombCount;
        [JsonProperty("id")]
        public int Id;

        public virtual void SetId(int value)
        {
            this.Id = value;
        }

        public Player(string name)
        {
            this.Name = name;
            this.power = DEFAULT_POWER;
            this.setBombLimit = DEFAULT_BOMB_LIMIT;
            this.ch = name.ToCharArray()[0];
            this.isAlive = true;
            this.setBombCount = 0;
        }

        public bool CanSetBomb()
        {
            return setBombCount < setBombLimit;
        }

        public virtual ActionData Action(string mapdata)
        {
            return new ActionData(this, "STAY", false);
        }

        public virtual void Dispose()
        {
        }
    }
}
