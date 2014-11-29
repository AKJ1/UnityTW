 using System;
 using System.Collections;
 using System.Collections.Generic;
using System.Linq;
using System.Text;
 using Assets.Game.Characters;
 using UnityEngine;

namespace Assets.Game.Equipment
{
    public abstract class Weapon : MonoBehaviour
    {
        public float Damage = 10f;
        public float Cooldown = 0.0f;
        public float Duration = 0.5f;
        public bool OnCooldown;
        public WeaponType Type;
        public abstract void Attack();


        protected virtual IEnumerator Heat()
        {
            this.OnCooldown = true;
            yield return new WaitForSeconds(this.Cooldown);
            this.OnCooldown = false;
        }

        protected void ApplyDamage(Character person)
        {
            person.Health -= this.Damage;
        }
        protected IEnumerator DestroyProjectile(GameObject go, float animationDelay)
        {
            go.gameObject.SetActive(false);
            yield return new WaitForSeconds(animationDelay);
            go.gameObject.SetActive(true);
            yield return new WaitForSeconds(this.Duration);
            Destroy(go);

        }
        //void OnCollisionEnter(Collision c)
        //{
        //    Debug.Log("fucko");
        //    if (c.gameObject.tag == "character")
        //    {
        //        Debug.Log("fucko");
        //        Character victim = c.gameObject.GetComponent<Character>();
        //        ApplyDamage(victim);
        //    }

        //}

    }
    public enum WeaponType
    {
        Sword,
        Hammer,
        Gun,

    }
}
