namespace Assets.Game.Equipment.Weapons
{
    using System.Collections;
    using System.Collections.Generic;
    using Characters;
    using UnityEngine;
    class Sword : Weapon
    {
        #region Variables
        private float influenceSphereRadius;
        public GameObject SwordPrefab = WeaponGlobals.SwordPrefab;
        #endregion

        #region Attack Logic
        public override void Attack()
        {
            if (!this.OnCooldown)
            {
                StartCoroutine(Heat());
                GameObject go = (GameObject) Instantiate(SwordPrefab, (transform.position + transform.up * 0.6f), new Quaternion() ); // change with animation prefab;
                SetupGameObject(go);
                AddHitEffects(go);
                SphereCollider swordCollider = SetupCollider(go); 
                StartCoroutine(GrowCollider(go, swordCollider));
            }
        }
        private IEnumerator GrowCollider(GameObject go, SphereCollider objCol)
        {
            float elapsedTime = 0f;
            while (elapsedTime < Duration)
            {
                elapsedTime += Time.deltaTime;
                objCol.radius = (elapsedTime / Duration) * influenceSphereRadius;
                if (elapsedTime > Duration)
                {
                    objCol.radius = influenceSphereRadius;
                }
                yield return new WaitForEndOfFrame();
            }
        }
        #endregion

        #region Setup Methods
        private void SetupGameObject(GameObject go)
        {
            go.transform.parent = transform;
            go.particleSystem.startRotation = (transform.rotation.eulerAngles.y - 180) * Mathf.Deg2Rad;
            go.particleSystem.startLifetime = Duration;
            go.particleSystem.startSize = influenceSphereRadius*1.35f;
        }
        private SphereCollider SetupCollider(GameObject go)
        {
            SphereCollider objCollider = go.AddComponent<SphereCollider>();
            objCollider.isTrigger = true;
            return objCollider;
        }
        void Start()
        {
            Damage = 50f;
            Cooldown = 0.6f;
            Duration = 0.2f;
            influenceSphereRadius = .9f;
        }
        #endregion

        #region Hit Effects
        protected override void HitEffects(Collider target, GameObject go)
        {
            List<GameObject> alreadyHit = new List<GameObject>();
            if (target.transform.tag == "Enemy" && !alreadyHit.Contains(target.gameObject))
            {
                Character victim = target.gameObject.GetComponent<Character>();
                Vector3 heading = target.transform.position - go.transform.position;
                float distance = heading.magnitude;
                Vector3 direction = heading / distance;
                target.transform.rigidbody.AddForce(new Vector3(direction.x, 0, direction.z) * 200f);
                victim.TakeDamage(Damage);
            }
            alreadyHit.Add(target.gameObject);
            Destroy(go, this.Duration + go.particleSystem.time + 2f);
        }
        #endregion
    }
}
