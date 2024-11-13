﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero_Adventure
{
    public abstract class CharacterTile : Tile
    {
        public int hitPoints;
        public int maximumHitPoints;
        public int attackPower;
        public Tile[] vision;
        public bool isDead;
        private int doubleDamageCount = 0;

        public Position characterPosition;

        public CharacterTile(Position aPosition, int aHitPoints, int aAttackPower) : base(aPosition)
        {
            hitPoints = aHitPoints;
            maximumHitPoints = aHitPoints;
            attackPower = aAttackPower;
            vision = new Tile[4];

            characterPosition = aPosition;
        }

        public void UpdateVision(Level aLevel)
        {
            //Position heroPos = aLevel.Hero.Position;
            Position heroPos = Position; // fixed this

            vision[0] = aLevel.tiles[heroPos.X, heroPos.Y - 1]; // 0
            vision[1] = aLevel.tiles[heroPos.X + 1, heroPos.Y]; // 1
            vision[2] = aLevel.tiles[heroPos.X, heroPos.Y + 1]; // 2
            vision[3] = aLevel.tiles[heroPos.X - 1, heroPos.Y]; // 3

            //      0
            //  3   C   1
            //      2
        }

        public void TakeDamage(int incomingDamage)
        {
            hitPoints -= incomingDamage;

            if (hitPoints < 0)
            {
                hitPoints = 0;
            }
        }

        public void Attack(CharacterTile opponent)
        {
            if(doubleDamageCount > 0)
            {
                opponent.TakeDamage(attackPower * 2);
                doubleDamageCount--;
            }
            else
            {
                opponent.TakeDamage(attackPower);
            }
        }

        public bool IsDead
        {
            get
            {
                if (hitPoints <= 0)
                    return true;
                else
                    return false;
            }
        }

        public Tile[] Vision
        {
            get { return vision; }
            set { vision = value; }
        }

        public int DoubleDamageCount
        {
            get { return doubleDamageCount; }
            set { doubleDamageCount = value; }
        }

        public void Heal(int healAmmount)
        {
            hitPoints += healAmmount;
            if (hitPoints > maximumHitPoints)
            {
                hitPoints = maximumHitPoints;
            }
        }

        public void SetDoubleDamage(int noOfAttacks)
        {
            doubleDamageCount += noOfAttacks;
        }
    }
}
