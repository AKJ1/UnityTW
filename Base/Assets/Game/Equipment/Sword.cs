using System;
using System.Collections;
using UnityEngine;

namespace Assets.Game.Equipment
{
    class Sword : Weapon
    {
        public override void Attack()
        {
            if (!this.OnCooldown)
            {
                Heat();
                this.OnCooldown = true;
                StartCoroutine(Heat());
                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube); // change with animation prefab;
                go.collider.isTrigger = false;
                go.transform.parent = transform;
                go.transform.position = (transform.rotation * -transform.forward)+ transform.position;
                go.AddComponent<SwordHitEffects>(); // Done for collision handling
                StartCoroutine(DestroyProjectile(go));
            }
        }

        private IEnumerator DestroyProjectile(GameObject go)
        {
            yield return new WaitForSeconds(this.Duration);
            Destroy(go);
            
        }

    }
}
