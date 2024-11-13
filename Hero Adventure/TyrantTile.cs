using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero_Adventure
{
    public class TyrantTile : EnemyTile
    {
        public TyrantTile(Position position, Level level) : base(position, 15, 5, level)
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
            if (level.Hero.Y < Y && Vision[0] is EmptyTile)
            {
                targetTile = Vision[0];
                return true;
            }
            else if (level.Hero.X > X && Vision[1] is EmptyTile)
            {
                targetTile = Vision[1];
                return true;
            }
            else if (level.Hero.Y > Y && Vision[2] is EmptyTile)
            {
                targetTile = Vision[2];
                return true;
            }
            else if (level.Hero.X < X && Vision[3] is EmptyTile)
            {
                targetTile = Vision[3];
                return true;
            }
            else
            {
                targetTile = null;
                return false;
            }
        }

        public override CharacterTile[] GetTargets()
        {
            List<CharacterTile> targets = new List<CharacterTile>();

            for (int i = 0; i < level.Tiles.GetLength(0); i++)
            {
                if (level.Tiles[i, Y] is CharacterTile && i != X)
                {
                    targets.Add((CharacterTile)level.Tiles[i, Y]);
                }
            }

            for (int j = 0; j < level.Tiles.GetLength(1); j++)
            {
                if (level.Tiles[X, j] is CharacterTile && j != Y)
                {
                    targets.Add((CharacterTile)level.Tiles[X, j]);
                }
            }

            CharacterTile[] targetArray = targets.ToArray();
            return targetArray;
        }
    }
}