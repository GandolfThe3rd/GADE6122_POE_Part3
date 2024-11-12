using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace Hero_Adventure
{
    public abstract class EnemyTile :CharacterTile
    {
        public EnemyTile(Position position, int hitPoints, int attackPower, Level level) : base(position, hitPoints, attackPower)
        {
            this.level = level;
        }

        protected Level level;

        public abstract bool GetMove(out Tile targetTile);

        public abstract CharacterTile[] GetTargets();

    }
}
