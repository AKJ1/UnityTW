using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Game.Equipment
{
    class Dagger : Sword
    {
        void Start()
        {
            Damage = 20f;
            Cooldown = 0.10f;
            Duration = 0.05f;
        }
        public override void Attack()
        {
            if (!this.OnCooldown)
            {
                Heat();
                this.OnCooldown = true;
                StartCoroutine(Heat());

                GameObject go = (GameObject)Instantiate(WeaponGlobals.DaggerPrefab); // change with animation prefab;
                DaggerHitEffects dhe = go.AddComponent<DaggerHitEffects>(); // Done for collision handling
                dhe.Damage = this.Damage;
                go.transform.parent = transform;
                go.transform.position = (transform.position + transform.up * 0.6f);
                go.transform.Rotate(0, transform.rotation.eulerAngles.y, 0);
                StartCoroutine(DestroyProjectile(go, 0));
            }
        }
    }
}
