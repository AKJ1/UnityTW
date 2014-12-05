namespace Assets.Game.Equipment.Weapons
{
    using System.Collections.Generic;
    using System.Collections;
    using Characters;
    using UnityEngine;
    class Hammer : Weapon
    {
        #region Variable
        private float knockback;
        private readonly GameObject projectile = WeaponGlobals.HammerPrefab;
        #endregion

        #region Attack Logic
        public override void Attack()
        {
            if (!this.OnCooldown)
            {
                Heat();
                this.OnCooldown = true;
                StartCoroutine(Heat());
                GameObject go = (GameObject)Instantiate(projectile); // change with animation prefab;
                StartCoroutine(RemoveCollider(go.GetComponent<SphereCollider>()));
                AddHitEffects(go);
                go.transform.position = (transform.position + transform.up * 1.6f);
                go.transform.Rotate(0, transform.rotation.eulerAngles.y, 0);
            }
        }
        IEnumerator RemoveCollider(SphereCollider objCol)
        {
            yield return new WaitForSeconds(this.Duration);
            Destroy(objCol);
        }
        #endregion

        #region SetupMethods
        void Start()
        {
            Damage = 80f;
            Cooldown = 1.2f;
            Duration = 0.1f;
            knockback = 300f;
        }
        #endregion

        #region HitEffects
        protected override void HitEffects(Collider target, GameObject go)
        {
            List<GameObject> alreadyHit = new List<GameObject>();
            if (target.transform.tag == "Enemy" && !alreadyHit.Contains(target.gameObject))
            {
                Character victim = target.gameObject.GetComponent<Character>();
                Vector3 heading = target.transform.position - go.transform.position;
                float distance = heading.magnitude;
                Vector3 direction = heading / distance;
                target.transform.rigidbody.AddForce(new Vector3(direction.x, 0, direction.z) * knockback);
                victim.TakeDamage(Damage);
            }
            alreadyHit.Add(target.gameObject);
        }
        #endregion
    }
}
