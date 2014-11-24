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
        void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.tag == "Character")
            {
                Character victim = collision.gameObject.GetComponent<Character>();
                victim.Health -= Damage;
                Debug.Log(victim.Health);
            }
        }
    }
}
