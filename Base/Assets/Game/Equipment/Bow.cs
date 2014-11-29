using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Game.Equipment
{
    class Bow : Weapon
    {
        public GameObject BowProjectile = WeaponGlobals.BowProjectile;
        public GameObject ExactProjectile = WeaponGlobals.BowProjectileExact;
        public float ActiveDamage;
        public int ProjectilePower;

        private bool canPenetrate;
        private Color exactColor = new Color(0,255,255);
        private Color normalColor = new Color();

        public override void Attack()
        {
            if (!this.OnCooldown)
            {
                StartCoroutine(Charge());
                StartCoroutine(Heat());
            }
        }
        protected IEnumerator Charge()
        {
            bool buttonHeld = Input.GetButton("Fire");
            while (buttonHeld)
            {
                buttonHeld = Input.GetButton("Fire");
                if (this.ProjectilePower >= 75 && this.ProjectilePower < 90)
                {
                    renderer.material.color = exactColor;
                }
                else if (renderer.material.color == exactColor)
                {
                    renderer.material.color = normalColor;
                }

                if (this.ProjectilePower < 100)
                {
                    this.ProjectilePower += 1;
                }
                yield return new WaitForFixedUpdate();
            }
            Debug.Log(this.ProjectilePower);
            FireProjectile();
            this.ProjectilePower = 0;
            if (renderer.material.color == exactColor)
            {
                renderer.material.color = normalColor;
            }
        }

        private void FireProjectile()
        {

            float projectileRatio = DetermineRatio();

            GameObject projectile = this.canPenetrate ? (GameObject)Instantiate(ExactProjectile) : (GameObject)Instantiate(BowProjectile); // change with animation prefab;
            BowHitEffects hitEffects = projectile.AddComponent<BowHitEffects>(); // Done for collision handling
            projectile.transform.position = (transform.position + transform.up * 2f);
            projectile.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y - 90, 0);
            if (this.canPenetrate)
            {
                hitEffects.CanPenetrate = true;
                this.canPenetrate = false;
            }
            hitEffects.Damage = this.ActiveDamage;
            projectile.rigidbody.velocity = Quaternion.Euler(0, transform.rotation.y, 0) * transform.up * projectileRatio * 30;
            StartCoroutine(DestroyProjectile(projectile, 0));
        }

        private float DetermineRatio()
        {
            float projectileRatio = 0.50f;

            if (this.ProjectilePower > 0 && this.ProjectilePower < 25)
            {
                projectileRatio = 0.20f;
                this.ActiveDamage = this.Damage * 0.30f;
            }
            else if (this.ProjectilePower > 25 && this.ProjectilePower <= 50)
            {
                projectileRatio = 0.50f;
                this.ActiveDamage = this.Damage * 0.50f;
            }
            else if (this.ProjectilePower > 50 && this.ProjectilePower <= 75)
            {
                projectileRatio = 0.75f;
                this.ActiveDamage = this.Damage * 0.80f;
            }
            else if (this.ProjectilePower > 75 && this.ProjectilePower <= 95)
            {
                projectileRatio = 2.0f;
                this.ActiveDamage = this.Damage * 1.30f;
                canPenetrate = true;
            }
            else if (this.ProjectilePower > 95 && this.ProjectilePower <= 100)
            {
                projectileRatio = 1.00f;
                this.ActiveDamage = this.Damage * 1.0f;
            }
            return projectileRatio;

        }
        void Start()
        {
            this.normalColor = renderer.material.color;
            Damage = 150f;
            Cooldown = 1.2f;
            Duration = 5.0f;
        }
    }
}
