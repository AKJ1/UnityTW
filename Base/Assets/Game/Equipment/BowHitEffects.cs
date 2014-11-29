using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Game.Characters;
using UnityEngine;

namespace Assets.Game.Equipment
{
    class BowHitEffects : MonoBehaviour
    {
        private List<GameObject> alreadyHit = new List<GameObject>( );
        public float Damage;
        public bool CanPenetrate = false;

        void OnTriggerEnter(Collider target)
        {
            if (target.transform.tag == "Enemy" && !alreadyHit.Contains(target.gameObject))
            {
                Character victim = target.gameObject.GetComponent<Character>();
                victim.TakeDamage(Damage);
                Debug.Log(victim.Health);
                alreadyHit.Add(target.gameObject);
                if (!CanPenetrate)
                {
                    Destroy(this.gameObject);
                }
            }
            else if (target.transform.tag != "Terrain" && target.transform.tag != "Enemy")
            {
                Debug.Log(target.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}
