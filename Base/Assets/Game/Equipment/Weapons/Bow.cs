namespace Assets.Game.Equipment.Weapons
{
    using System.Collections;
    using System.Collections.Generic;
    using Characters;
    using UnityEngine;
    class Bow : Weapon
    {
        #region Variables
        
        public GameObject BowProjectile = WeaponGlobals.BowProjectile;
        public GameObject ExactProjectile = WeaponGlobals.BowProjectileExact;
        public float ActiveDamage;
        public float ElapsedTime;
        public float PerfectWindowLength;
        public int ProjectilePower;
        public float ChargeTime;


        public bool CanPenetrate { get; set; }
        private bool perfectShot;
        private Color exactColor = new Color(0,255,255);
        private Color normalColor = new Color();

        #endregion

        #region AttackLogic
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
                }
                else if (!windowStarted)
                {
                    StartCoroutine(PerfectWindow());
                    windowStarted = true;
                }
                yield return new WaitForFixedUpdate();
            }
            ProjectilePower = (int)(this.ElapsedTime / this.ChargeTime * 100);
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
            AddHitEffects(projectile);

            projectile.transform.position = (transform.position + transform.up * 2f);
            projectile.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y - 90, 0);
            if (this.perfectShot)
            {
                this.CanPenetrate = true;
                this.perfectShot = false;
            }
            else
            {
                this.CanPenetrate = false;
            }
            projectile.rigidbody.velocity = Quaternion.Euler(0, transform.rotation.y, 0) * transform.up * projectileRatio * 30;
            Destroy(projectile, this.Duration);
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
        #endregion

        #region Hit Effects
        protected override void HitEffects(Collider target, GameObject go)
        {
            List<GameObject> alreadyHit = new List<GameObject>();
            if (target.transform.tag == "Enemy" && !alreadyHit.Contains(target.gameObject))
            {
                Character victim = target.gameObject.GetComponent<Character>();
                victim.TakeDamage(Damage);
                alreadyHit.Add(target.gameObject);
                if (!CanPenetrate)
                {
                    Destroy(go);
                }
            }
            else if (target.transform.tag != "Terrain" && target.transform.tag != "Enemy")
            {
                Debug.Log(target.gameObject);
                Destroy(go);
            }
        }
        #endregion

        #region Setup Methods
        
        void Start()
        {
            this.normalColor = renderer.material.color;
            Damage = 100f;
            Cooldown = 1.2f;
            Duration = 5.0f;
            ChargeTime = .9f;
            PerfectWindowLength = 0.2f;
        }

        #endregion
    }
}
