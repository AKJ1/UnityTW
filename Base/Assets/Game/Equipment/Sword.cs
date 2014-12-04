using System;
using System.Collections;
using Assets.Controls;
using UnityEngine;

namespace Assets.Game.Equipment
{
    class Sword : Weapon
    {

        private float influenceSphereRadius;
        public GameObject SwordPrefab = WeaponGlobals.SwordPrefab;
        public override void Attack()
        {
            if (!this.OnCooldown)
            {
                StartCoroutine(Heat());
                GameObject go = (GameObject) Instantiate(SwordPrefab, (transform.position + transform.up * 0.6f), new Quaternion() ); // change with animation prefab;
                SetupGameObject(go);
                SphereCollider swordCollider = go.AddComponent<SphereCollider>();
                setupCollider(swordCollider);
                SwordHitEffects effects = go.AddComponent<SwordHitEffects>(); // Done for collision handling
                SetupHitEffects(effects);
                StartCoroutine(GrowCollider(go, swordCollider));
                //StartCoroutine(DestroyProjectile(go, this.Duration));
            }
        }

        private IEnumerator GrowCollider(GameObject go, SphereCollider collider)
        {
            float elapsedTime = 0f;
            while (elapsedTime < Duration)
            {
                elapsedTime += Time.deltaTime;
                collider.radius = (elapsedTime / Duration) * influenceSphereRadius;
                if (elapsedTime > Duration)
                {
                    collider.radius = influenceSphereRadius;
                }
                Debug.Log(collider.radius);
                yield return new WaitForEndOfFrame();
            }
        }

        private void SetupGameObject(GameObject go)
        {
            go.transform.parent = transform;
            go.particleSystem.startRotation = (transform.rotation.eulerAngles.y - 180) * Mathf.Deg2Rad;
            go.particleSystem.startLifetime = Duration;
            go.particleSystem.startSize = influenceSphereRadius*1.35f;
        }
        private void setupCollider(SphereCollider collider)
        {
            collider.isTrigger = true;
        }

        private void SetupHitEffects(SwordHitEffects effects)
        {
            effects.Damage = this.Damage;
        }

        void Start()
        {
            Damage = 50f;
            Cooldown = 0.6f;
            Duration = 0.2f;
            influenceSphereRadius = .9f;
        }
     

    }
}
