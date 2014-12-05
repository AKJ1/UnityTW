using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Game.Characters.Enemies
{
    class Charger : Enemy
    {
        private float chargeSpeed;
        private float chargeDuration;
        private float chargeBuildup;

        private readonly Color standardColor = new Color(255,255,255);
        private readonly Color chargeColor = new Color(255,0,0);

        
        void Update()
        {
            if (!IsBusy)
            {
                DecideActions();
            }
        }

        void Start()
        {
            LocateTarget();
            this.transform.tag = "Enemy";
            this.MaxHealth = 100;
            this.Health = MaxHealth;
            this.InvulnerabilityTime = 0.08f;
            this.MovementSpeed = 2.0f;
            this.chargeSpeed = 6f;
            this.chargeDuration = .4f;
            this.chargeBuildup = 0.8f;
            this.Invulnerable = false;
            States.Add("isFollowing", false);
            States.Add("isWandering", false);
            States.Add("isCharging", false);
            States.Add("isIdling", true);
        }

        protected override void Attack()
        {
            StartCoroutine(Charge());
        }

        protected IEnumerator Charge()
        {
            StartCoroutine(ChargeWarning(chargeBuildup));
            transform.LookAt(Target.transform);
            transform.rotation = Quaternion.Euler(90, transform.rotation.y, 0);

            Vector3 direction = Target.transform.position - transform.position;
            direction.Normalize();
            yield return new WaitForSeconds(chargeBuildup);
            transform.rigidbody.velocity = direction * chargeSpeed * 3;
            yield return new WaitForSeconds(chargeDuration);
            transform.rigidbody.velocity = Vector3.zero;
            IsBusy = false;
            StartCoroutine(Idle());
        }

        IEnumerator ChargeWarning(float length)
        {
            while (length > 0)
            {
                length -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
                StartCoroutine(BlinkRed(length / 4));
            }
            transform.renderer.material.color = standardColor;
        }

        IEnumerator BlinkRed(float time)
        {
            transform.renderer.material.color = chargeColor;
            yield return new WaitForSeconds(time);
            transform.renderer.material.color = standardColor;
        }

        protected override void DecideActions()
        {
            if (States["isWandering"] || States["isFollowing"] || States["isIdling"])
            {
                IsBusy = true;
                transform.rigidbody.velocity = Vector3.zero;
                float distanceToTarget = Vector3.Distance(transform.position, Target.transform.position);
                if (distanceToTarget <= 2f)
                {
                    ChangeState("Attacking");
                    Attack();
                }
                else if (distanceToTarget <= 8f)
                {
                    ChangeState("Following");
                    StartCoroutine(Follow());
                    Debug.Log("following");
                }
                else if (distanceToTarget <= 15f)
                {
                    ChangeState("Wandering");
                    StartCoroutine(Wander());
                    Debug.Log("wandering");
                }
                else
                {
                    ChangeState("Idling");
                    StartCoroutine(Idle());
                    Debug.Log("idling");
                }
            }
        }

    }
}
