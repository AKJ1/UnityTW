using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Assets.Game.Equipment;

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
    }
}
