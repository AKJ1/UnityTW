using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Game.Characters;
using UnityEngine;

namespace Assets.Game.Equipment
{
    class SwordHitEffects : MonoBehaviour
    {
        public float Damage;
        public float Duration;

        void OnTriggerEnter(Collider target)
        {
            if (target.transform.tag == "Enemy")
            {
                Character victim = target.gameObject.GetComponent<Character>();
                victim.TakeDamage(Damage);
                Debug.Log(victim.Health);
            }
        }
    }
}
