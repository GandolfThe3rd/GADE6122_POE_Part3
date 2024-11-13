using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero_Adventure
{
    public class ExitTile : Tile
    {
        private bool doorLock;

        public bool DoorLock
        {
        get { return doorLock; }
        set { doorLock = value; }
        }

        public ExitTile(Position aPosition) : base(aPosition)
        {

        }

        public override char Display
        {
            get
            {
                if (doorLock)
                {
                    return Convert.ToChar("▓");
                }
                else
                {
                    return Convert.ToChar("▒");
                }
            }
        }
    }
}
