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
        void OnCollisionEnter(Collision collision)
        {
            Debug.Log("something something dangerzone");
            Debug.Log("kek1");
            if (collision.transform.tag == "Character")
            {
                Debug.Log("kek2");
                Character victim = collision.gameObject.GetComponent<Character>();
                Debug.Log(victim.Health);
            }
        }
    }
}
