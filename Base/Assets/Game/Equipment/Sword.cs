using System;
using System.Collections;
using Assets.Controls;
using UnityEngine;

namespace Assets.Game.Equipment
{
    class Sword : Weapon
    {
        public GameObject SwordPrefab = WeaponGlobals.SwordPrefab;
        public override void Attack()
        {
            if (!this.OnCooldown)
            {
                Heat();
                this.OnCooldown = true;
                StartCoroutine(Heat());
                
                GameObject go = (GameObject) Instantiate(SwordPrefab); // change with animation prefab;
                SwordHitEffects she = go.AddComponent<SwordHitEffects>(); // Done for collision handling
                she.Damage = this.Damage;
                go.transform.parent = transform;
                go.transform.position = (transform.position + transform.up*0.6f);
                go.transform.Rotate(0,transform.rotation.eulerAngles.y,0);
                StartCoroutine(DestroyProjectile(go, 0));
            }
        }

        void Start()
        {
            Damage = 50f;
            Cooldown = 0.6f;
            Duration = 0.2f;
        }
     

    }
}
