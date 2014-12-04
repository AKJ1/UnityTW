using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Game.Characters;
using UnityEngine;

namespace Assets.Game.Equipment
{
    class ShotgunHitEffects : MonoBehaviour
    {

        public float Damage;
        private List<GameObject> alreadyHit = new List<GameObject>();
        private void OnTriggerEnter(Collider c)
        {
            if (c.transform.tag == "Enemy" && !alreadyHit.Contains(c.gameObject))
            {
                alreadyHit.Add(c.gameObject);
                Character victim = c.transform.GetComponent<Character>();
                victim.TakeMinorDamage(this.Damage);
                this.gameObject.SetActive(false);
                Destroy(this.gameObject, 0.2f);
            }
        }

    }
}
