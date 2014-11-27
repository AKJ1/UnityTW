using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Game.Characters;
using UnityEngine;

namespace Assets.Game.Equipment
{
    class HammerHitEffects : MonoBehaviour
    {
        public float Damage;
        public float Duration;
        public float Knockback;
        private List<GameObject> alreadyHit = new List<GameObject>();
        void OnTriggerEnter(Collider target)
        {
            if (target.transform.tag == "Enemy" && !alreadyHit.Contains(target.gameObject))
            {
                Character victim = target.gameObject.GetComponent<Character>();
                Vector3 heading = target.transform.position - transform.position;
                float distance = heading.magnitude;
                Vector3 direction = heading / distance;
                target.transform.rigidbody.AddForce(new Vector3(direction.x, 0, direction.z) * Knockback);
                victim.TakeDamage(Damage);
                Debug.Log(victim.Health);
            }

            alreadyHit.Add(target.gameObject);
        }
    }
}
