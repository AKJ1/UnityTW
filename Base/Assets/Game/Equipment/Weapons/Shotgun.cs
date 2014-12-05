namespace Assets.Game.Equipment.Weapons
{
    using System.Collections;
    using System.Collections.Generic;
    using Characters;
    using UnityEngine;
    class Shotgun : Weapon
    {
        #region Varibles
        private int projectileCount;
        private float scatterArc;
        private float projectileSpeed;
        private readonly GameObject shotgunProjectile = WeaponGlobals.ShotgunProjectile;
        #endregion

        #region Attack Logic
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

                Vector3 direction = go.transform.position - transform.position;
                direction.Normalize();
                go.rigidbody.velocity = direction * 40f;

                AddHitEffects(go);
                
                currentAngle += angleStep;
            }
            
            yield break;
        }
        #endregion

        #region Setup Methods
        void Start()
        {
            this.Damage = 20f;
            this.Cooldown = 1.5f;
            this.Duration = 0.2f;
            this.scatterArc = 60f;
            this.projectileCount = 8;
        }
        #endregion

        #region Hit Effects
        protected override void HitEffects(Collider target, GameObject go)
        {
            List<GameObject> alreadyHit = new List<GameObject>();
            if (target.transform.tag == "Enemy" && !alreadyHit.Contains(target.gameObject))
            {
                alreadyHit.Add(target.gameObject);
                Character victim = target.transform.GetComponent<Character>();
                victim.TakeMinorDamage(Damage);
                go.SetActive(false);
                Destroy(go, 0.2f);
            }
        }
        #endregion
    }
}
