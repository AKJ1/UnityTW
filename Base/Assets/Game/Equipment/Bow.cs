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
        public float ElapsedTime;
        public float PerfectWindowLength;
        public int ProjectilePower;
        public float ChargeTime;

        private bool perfectShot;
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
                bool windowStarted = false;
                buttonHeld = Input.GetButton("Fire");
                if (this.ElapsedTime < this.ChargeTime)
                {
                    this.ElapsedTime += Time.deltaTime;
                }else if(!windowStarted)
                {
                    StartCoroutine(PerfectWindow());
                    windowStarted = true;
                }
                yield return new WaitForFixedUpdate();
            }
            ProjectilePower = (int) (this.ElapsedTime/this.ChargeTime*100);
            Debug.Log(perfectShot);
            FireProjectile();
            if (ProjectilePower == 100)
            {
                this.OnCooldown = false;
            }
            this.ProjectilePower = 0;
            this.ElapsedTime = 0;
            if (renderer.material.color == exactColor)
            {
                renderer.material.color = normalColor;
            }
        }

        private IEnumerator PerfectWindow()
        {
            this.perfectShot = true;
            renderer.material.color = exactColor;
            yield return new WaitForSeconds(PerfectWindowLength);
            renderer.material.color = normalColor;
            yield return new WaitForSeconds(0.05f);
            this.perfectShot = false;
        }

        private void FireProjectile()
        {

            float projectileRatio = DetermineRatio();
            this.ActiveDamage = this.Damage * projectileRatio;

            GameObject projectile = this.perfectShot ? (GameObject)Instantiate(ExactProjectile) : (GameObject)Instantiate(BowProjectile); // change with animation prefab;
            BowHitEffects hitEffects = projectile.AddComponent<BowHitEffects>(); // Done for collision handling


            projectile.transform.position = (transform.position + transform.up * 2f);
            projectile.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y - 90, 0);
            if (this.perfectShot)
            {
                hitEffects.CanPenetrate = true;
                this.perfectShot = false;
            }
            hitEffects.Damage = this.ActiveDamage;
            projectile.rigidbody.velocity = Quaternion.Euler(0, transform.rotation.y, 0) * transform.up * projectileRatio * 30;
            StartCoroutine(DestroyProjectile(projectile, 0));
        }

        private float DetermineRatio()
        {
            float projectileRatio = 0.50f;

            if (ProjectilePower < 50)
            {
                projectileRatio = 0.50f;
            }
            else if (this.perfectShot)
            {
                projectileRatio = 2f;
            }
            else
            {
                projectileRatio = 0.01f * ProjectilePower;
            }
            
            return projectileRatio;

        }
        void Start()
        {
            this.normalColor = renderer.material.color;
            Damage = 100f;
            Cooldown = 1.2f;
            Duration = 5.0f;
            ChargeTime = 1.2f;
            PerfectWindowLength = 0.2f;
        }
    }
}
