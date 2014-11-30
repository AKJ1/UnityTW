using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Game.Characters;
using UnityEngine;

namespace Assets.Game.Equipment
{
    class DaggerHitEffects : MonoBehaviour
    {
        public float Damage;
        public float Duration;
        private List<GameObject> alreadyHit = new List<GameObject>();
        public Dagger Source;
        void OnTriggerEnter(Collider target)
        {
            if (target.transform.tag == "Enemy" && !alreadyHit.Contains(target.gameObject))
            {
                Character victim = target.gameObject.GetComponent<Character>();
                victim.TakeDamage(Damage);
                Debug.Log(victim.Health);
                Source.HitAchieved();
            }

            alreadyHit.Add(target.gameObject);
        }
    }
}
