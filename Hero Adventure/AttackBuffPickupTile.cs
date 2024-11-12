using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero_Adventure
{
    internal class AttackBuffPickupTile : PickupTile
    {
        public AttackBuffPickupTile(Position position) : base (position)
        {
            
        }

        public override void ApplyEffect(CharacterTile target)
        {
            target.SetDoubleDamage(3);
        }

        public override char Display
        {
            get
            {
                return '*';
            }
        }
    }
}
