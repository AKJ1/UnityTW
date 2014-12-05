using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Assets.Game.Equipment;
using Assets.Game.Equipment.Weapons;

namespace Assets.Game.Characters
{
    class Player : Character
    {
        public bool isBlocking;

        private void Dodge()
        {
            
        }

        void Move()
        {
            
        }

        void Start()
        {
            this.transform.tag = "Player";
            this.Weapon = gameObject.AddComponent<Shotgun>();
            this.Health = MaxHealth;
        }

        protected override void Attack()
        {
            this.Weapon.Attack();
        }
    }
}
