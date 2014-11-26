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
            this.Health = MaxHealth;
            this.InvulnerabilityTime = 0.10f;
        }
    }
}
