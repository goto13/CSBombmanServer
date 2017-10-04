using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSBombmanServer
{
    public class Block
    {
        public Position pos;
        [NonSerialized]
        private Item item;

        public Item GetItem()
        {
            return item;
        }

        public void SetItem(Item item)
        {
            this.item = item;
        }

        public Block(Position pos)
        {
            this.pos = pos;
        }
        public bool equal(Block b)
        {
            return pos.Equals(b.pos);
        }
    }
}
