using System;
using System.Collections;
using Assets.Controls;
using UnityEditor;
using UnityEngine;

namespace Assets.Game.Equipment
{
    class Sword : Weapon
    {
        public GameObject SwordPrefab = WeaponGlobals.SwordPrefab;
        public override void Attack()
        {
            if (!this.OnCooldown)
            {
                Heat();
                this.OnCooldown = true;
                StartCoroutine(Heat());
                
                GameObject go = (GameObject) Instantiate(SwordPrefab); // change with animation prefab;
                SwordHitEffects she = go.AddComponent<SwordHitEffects>(); // Done for collision handling
                she.Damage = this.Damage;
                go.transform.position = (transform.position + transform.up*0.6f);
                go.transform.Rotate(0,transform.rotation.eulerAngles.y,0);
                StartCoroutine(DestroyProjectile(go, 0));
            }
        }

        void Start()
        {
            Damage = 50f;
            Cooldown = 0.2f;
            Duration = 1.3f;
        }
        private IEnumerator DestroyProjectile(GameObject go, float animationDelay)
        {
            go.gameObject.SetActive(false);
            yield return new WaitForSeconds(animationDelay);
            go.gameObject.SetActive(true);
            yield return new WaitForSeconds(this.Duration);
            Destroy(go);
            
        }

    }
}
