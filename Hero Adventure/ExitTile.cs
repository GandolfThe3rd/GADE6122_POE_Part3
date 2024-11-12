﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero_Adventure
{
    public class ExitTile : Tile
    {
        public ExitTile(Position aPosition) : base(aPosition)
        {

        }

        public override char Display
        {
            get { return Convert.ToChar("▒"); }
        }
    }
}
