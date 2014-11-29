using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Game.Equipment;

namespace Assets.Game.Characters
{
    class GenericEnemy : Character
    {
        void Start()
        {
            this.transform.tag = "Enemy";
            this.Weapon = gameObject.AddComponent<Sword>();
            this.MaxHealth = 100;
            this.Health = MaxHealth;
            this.InvulnerabilityTime = 0.02f;
        }
    }
}
