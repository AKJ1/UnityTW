using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Game.Equipment;
using UnityEngine;

namespace Assets.Game.Characters
{
    class GenericEnemy : Character
    {

        protected float MovementSpeed;
        void Start()
        {
            this.transform.tag = "Enemy";
            this.MaxHealth = 100;
            this.Health = MaxHealth;
            this.InvulnerabilityTime = 0.08f;
        }

        protected float DetermineBreakTime()
        {
            return UnityEngine.Random.Range(1.5f, 4f);
        }
    }
}
