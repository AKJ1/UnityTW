using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Game.Equipment
{
    class Hammer : Weapon
    {
        private float Knockback;
        
        public override void Attack()
        {
            if (!this.OnCooldown)
            {
                Heat();
                this.OnCooldown = true;
                StartCoroutine(Heat());

                GameObject go = (GameObject)Instantiate(WeaponGlobals.HammerPrefab); // change with animation prefab;
                HammerHitEffects hhe = go.AddComponent<HammerHitEffects>(); // Done for collision handling
                SetupHitEffects(hhe);
                go.transform.parent = transform;
                go.transform.position = (transform.position + transform.up * 1.6f);
                go.transform.Rotate(0, transform.rotation.eulerAngles.y, 0);
                StartCoroutine(DestroyProjectile(go, 0));
            }
        }

        private void SetupHitEffects(HammerHitEffects hitEffects)
        {
            hitEffects.Damage = this.Damage;
            hitEffects.Duration = this.Duration;
            hitEffects.Knockback = this.Knockback;
        }
        void Start()
        {
            Damage = 80f;
            Cooldown = 1.2f;
            Duration = 0.4f;
            Knockback = 300f;
        }
    }
}
