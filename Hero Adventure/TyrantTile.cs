using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero_Adventure
{
    public class TyrantTile : EnemyTile
    {
        public TyrantTile(Position position, Level level) : base (position, 15, 5, level)
        {

        }

        Random random = new Random();

        public override char Display
        {
            get
            {
                switch (isDead)
                {
                    case false:
                        {
                            return Convert.ToChar("§");
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
            bool check = false;
            Tile result = null;


            if (vision[0] is EmptyTile || vision[1] is EmptyTile || vision[2] is EmptyTile || vision[3] is EmptyTile)
            {
                check = true;
            }

            if (check == false)
            {
                targetTile = null;
                return false;
            }
            else
            {
                bool loop = true;

                while (loop)
                {

                    result = Vision[random.Next(0, 4)];

                    if (result is EmptyTile)
                    {
                        loop = false;
                    }
                    else
                    {
                        loop = true;
                    }

                }
                targetTile = result;
                return true;
            }
        }

        public override CharacterTile[] GetTargets()
        {
            int noOfTargets = 0;

            if (vision[0] is HeroTile)
            {
                noOfTargets++;
            }
            else if (vision[1] is HeroTile)
            {
                noOfTargets++;
            }
            else if (vision[2] is HeroTile)
            {
                noOfTargets++;
            }
            else if (vision[3] is HeroTile)
            {
                noOfTargets++;
            }

            CharacterTile[] targets = new CharacterTile[noOfTargets];

            if (vision[0] is HeroTile)
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
            else if (vision[1] is HeroTile)
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
            else if (vision[2] is HeroTile)
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
            else if (targets[3] is HeroTile)
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
