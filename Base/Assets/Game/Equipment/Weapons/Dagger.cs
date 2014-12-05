namespace Assets.Game.Equipment.Weapons
{
    using System.Collections;
    using UnityEngine;
    using System.Collections.Generic;
    using Characters;
    class Dagger : Weapon
    {
        #region Variables
        private int reductionCount;
        private float reductionLength;
        private float timeWithoutHit;
        private float baseCooldown;
        private bool timerActive;
        #endregion

        #region Setup Methods
        void Start()
        {
            Damage = 20f;
            Cooldown = 0.40f;
            baseCooldown = Cooldown;
            Duration = 0.05f;
            reductionLength = 2f;
        }
        #endregion

        #region Attack Logic
        public override void Attack()
        {
            if (!this.OnCooldown)
            {
                this.OnCooldown = true;
                StartCoroutine(Heat());
                

                GameObject go = (GameObject)Instantiate(WeaponGlobals.DaggerPrefab); // change with animation prefab;
                AddHitEffects(go);
                go.transform.position = (transform.position + transform.up * 1f - transform.forward * 0.6f);
                go.transform.Rotate(0, transform.rotation.eulerAngles.y, 0);
            }
        }
        #endregion

        #region HitEffects
        protected override void HitEffects(Collider target, GameObject go)
        {
            List<GameObject> alreadyHit = new List<GameObject>();
            if (target.transform.tag == "Enemy" && !alreadyHit.Contains(target.gameObject))
            {
                Character victim = target.gameObject.GetComponent<Character>();
                victim.TakeDamage(Damage);
                HitAchieved();
            }
            alreadyHit.Add(target.gameObject);
        }
        #endregion

        #region Combo Hit Effects
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
        #endregion

        #region GUI
        void OnGUI()
        {
            GUI.Box(new Rect(10,10, 100, 50),this.timeWithoutHit.ToString());
            GUI.Box(new Rect(10, 120, 100, 50), this.Cooldown.ToString());
        }
        #endregion
    }   
}
