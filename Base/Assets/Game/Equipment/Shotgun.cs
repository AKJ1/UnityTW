using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Game.Equipment
{
    class Shotgun : Weapon
    {
        private int projectileCount;
        private float scatterArc;
        private float projectileSpeed;
        private GameObject shotgunProjectile = WeaponGlobals.ShotgunProjectile;
        public override void Attack()
        {
            if (!OnCooldown)
            {
                StartCoroutine(Heat());
                StartCoroutine(SpawnProjectiles());
            }
        }

        IEnumerator SpawnProjectiles()
        {
            float currentAngle = -scatterArc/2;
            float angleStep = scatterArc/projectileCount;
            for (int i = 0; i < projectileCount; i++)
            {
                GameObject go = (GameObject) Instantiate(shotgunProjectile, transform.position + (transform.up * 0.5f), new Quaternion());
                go.transform.RotateAround(transform.position, Vector3.up, currentAngle);
                ShotgunHitEffects she = go.AddComponent<ShotgunHitEffects>();
                she.Damage = this.Damage;
                Vector3 direction = go.transform.position - transform.position;
                direction.Normalize();
                go.rigidbody.velocity = direction * 40f;
                currentAngle += angleStep;
                StartCoroutine(DestroyProjectile(go, 0f));
            }
            yield break;
        }

        void Start()
        {
            this.Damage = 20f;
            this.Cooldown = 1.5f;
            this.Duration = 0.2f;
            this.scatterArc = 60f;
            this.projectileCount = 8;
        }
    }
}
