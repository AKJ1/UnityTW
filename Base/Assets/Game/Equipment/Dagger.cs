using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Assets.Game.Equipment
{
    class Dagger : Sword
    {
        private int reductionCount;
        private float reductionLength;
        private float timeWithoutHit;
        private float baseCooldown;
        private bool timerActive;
        void Start()
        {
            Damage = 1f;
            Cooldown = 0.40f;
            baseCooldown = Cooldown;
            Duration = 0.05f;
            reductionLength = 2f;
        }
        public override void Attack()
        {
            if (!this.OnCooldown)
            {
                this.OnCooldown = true;
                StartCoroutine(Heat());
                

                GameObject go = (GameObject)Instantiate(WeaponGlobals.DaggerPrefab); // change with animation prefab;
                DaggerHitEffects dhe = go.AddComponent<DaggerHitEffects>(); // Done for collision handling
                dhe.Damage = this.Damage;
                dhe.Source = this;
                go.transform.parent = transform;
                go.transform.position = (transform.position + transform.up * 0.6f);
                go.transform.Rotate(0, transform.rotation.eulerAngles.y, 0);
                StartCoroutine(DestroyProjectile(go, 0));
            }
        }

        public void HitAchieved()
        {
            if (!timerActive)
            {
                this.timeWithoutHit = 0;
                StartCoroutine(ReduceCooldown());
            }
            timerActive = true;
            if (Cooldown > 0.10f)
            {
                Cooldown -= Cooldown / 6;
            }
            this.timeWithoutHit = 0;
        }
        private IEnumerator ReduceCooldown()
        {
           
            while (timeWithoutHit < reductionLength)
            {
                timeWithoutHit += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            this.timerActive = false;
            Cooldown = baseCooldown;
        }

        void OnGUI()
        {
            GUI.Box(new Rect(10,10, 100, 50),this.timeWithoutHit.ToString());
            GUI.Box(new Rect(10, 120, 100, 50), this.Cooldown.ToString());
        }
    }
}
