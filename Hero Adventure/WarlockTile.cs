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
            List<CharacterTile> targets = new List<CharacterTile>();

            if (level.Tiles[X - 1, Y] is CharacterTile)
            {
                targets.Add((CharacterTile)level.Tiles[X - 1, Y]);
            }
            else if (level.Tiles[X + 1, Y] is CharacterTile)
            {
                targets.Add((CharacterTile)level.Tiles[X + 1, Y]);
            }
            else if (level.Tiles[X, Y - 1] is CharacterTile)
            {
                targets.Add((CharacterTile)level.Tiles[X, Y -1]);
            }
            else if (level.Tiles[X, Y + 1] is CharacterTile)
            {
                targets.Add((CharacterTile)level.Tiles[X, Y + 1]);
            }
            else if (level.Tiles[X -1, Y - 1] is CharacterTile)
            {
                targets.Add((CharacterTile)level.Tiles[X - 1, Y - 1]);
            }
            else if (level.Tiles[X - 1, Y + 1] is CharacterTile)
            {
                targets.Add((CharacterTile)level.Tiles[X - 1, Y + 1]);
            }
            else if (level.Tiles[X + 1, Y - 1] is CharacterTile)
            {
                targets.Add((CharacterTile)level.Tiles[X + 1, Y - 1]);
            }
            else if (level.Tiles[X + 1, Y + 1] is CharacterTile)
            {
                targets.Add((CharacterTile)level.Tiles[X + 1, Y + 1]);
            }

            CharacterTile[] targetsArray = targets.ToArray();
            return targetsArray;
        }
    }
}
