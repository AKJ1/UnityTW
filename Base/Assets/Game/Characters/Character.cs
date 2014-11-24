using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Game.Equipment;
using UnityEngine;

namespace Assets.Game.Characters
{
    public abstract class Character : MonoBehaviour
    {
        //public abstract void Move(); Implemented 
        public event Action DamageTaken;
        public const float InvulnerabilityTime = 0.5f;

        public float Health
        {
            get { return this.health; }
            set
            {
                this.DamageTaken();
                if (value <= 0)
                {
                    this.Kill();
                }
                else
                {
                    this.health = value;
                }
                
            }
        }

        public int MaxHealth;
        public float Energy;
        public float Stamina;
        public bool Invulnerable;

        public Weapon Weapon;
        private float health;

        public void Awake()
        {
            transform.tag = "Character";
            DamageTaken += () => StartCoroutine(GoInvulnerable());
        }
        public void Attack()
        {
            Weapon.Attack();
        }
        public void Kill()
        {
            Destroy(this.gameObject);
        }

        public IEnumerator GoInvulnerable()
        {
            this.Invulnerable = true;
            yield return new WaitForSeconds(InvulnerabilityTime);
            this.Invulnerable = false;
        }
    }
}
