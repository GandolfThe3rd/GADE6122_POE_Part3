using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero_Adventure
{
    internal class WarlockTile : EnemyTile
    {
        public WarlockTile(Position position, Level level) : base(position, 10, 5, level)
        {

        }

        public override char Display
        {
            get
            {
                switch (isDead)
                {
                    case false:
                        {
                            return Convert.ToChar("ᐂ");
                        }
                    default:
                        {
                            return Convert.ToChar("X");
                        }
                }
            }
        }

        public override bool GetMove(out Tile targetTile)
        {
            targetTile = null;
            return false;
        }

        public override CharacterTile[] GetTargets()
        {
            int noOfTargets = 0;

            CharacterTile[] targets = new CharacterTile[noOfTargets];

            if (vision[0] is CharacterTile)
            {
                if (targets[0] == null)
                {
                    targets[noOfTargets - 1] = (CharacterTile)vision[0];
                }
                else
                {
                    noOfTargets--;
                    targets[noOfTargets - 1] = (CharacterTile)vision[0];
                }
            }
            else if (vision[1] is CharacterTile)
            {
                if (targets[0] == null)
                {
                    targets[noOfTargets - 1] = (CharacterTile)vision[1];
                }
                else
                {
                    noOfTargets--;
                    targets[noOfTargets - 1] = (CharacterTile)vision[1];
                }
            }
            else if (vision[2] is CharacterTile)
            {
                if (targets[0] == null)
                {
                    targets[noOfTargets - 1] = (CharacterTile)vision[2];
                }
                else
                {
                    noOfTargets--;
                    targets[noOfTargets - 1] = (CharacterTile)vision[2];
                }
            }
            else if (targets[3] is CharacterTile)
            {
                if (vision[0] == null)
                {
                    targets[noOfTargets - 1] = (CharacterTile)vision[3];
                }
                else
                {
                    noOfTargets--;
                    targets[noOfTargets - 1] = (CharacterTile)vision[3];
                }
            }

            return targets;
        }
    }
}
