using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Game.Characters.Enemies
{
    class Charger : Character
    {
        private GameObject target;
        private float chargeSpeed;
        private float chargeDuration;
        private float chargeBuildup;
        private bool isBusy;
        private Dictionary<string, bool> states = new Dictionary<string, bool>();

        private readonly Color standardColor = new Color(255,255,255);
        private readonly Color chargeColor = new Color(255,0,0);

        
        void Update()
        {
            if (!isBusy)
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
            states.Add("isFollowing", false);
            states.Add("isWandering", false);
            states.Add("isCharging", false);
            states.Add("isIdling", true);
        }

        void ChangeState(string newState)
        {
            foreach (var state in states.ToArray())
            {
                states[state.Key] = false;
            }
            switch (newState)
            {
                case "Following":
                    states["isFollowing"] = true;
                    break;
                case "Charging":
                    states["isCharging"] = true;
                    break;
                case "Wandering":
                    states["isWandering"] = true;
                    break;
                case "Idling":
                    states["isIdling"] = true;
                    break;
            }
        }

        IEnumerator ChargeWarning(float length)
        {
            while (length > 0)
            {
                length -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
                StartCoroutine(BlinkRed(length/4));
            }
            transform.renderer.material.color = standardColor;
        }

        IEnumerator BlinkRed(float time)
        {
            transform.renderer.material.color = chargeColor;
            yield return new WaitForSeconds(time);
            transform.renderer.material.color = standardColor;
        }
        IEnumerator Charge()
        {
            StartCoroutine(ChargeWarning(chargeBuildup));
            transform.LookAt(target.transform);
            transform.rotation = Quaternion.Euler(90, transform.rotation.y, 0);

            Vector3 direction = target.transform.position - transform.position;
            direction.Normalize();
            yield return new WaitForSeconds(chargeBuildup);
            transform.rigidbody.velocity = direction * chargeSpeed * 3;
            yield return new WaitForSeconds(chargeDuration);
            transform.rigidbody.velocity = Vector3.zero;
            isBusy = false;
            StartCoroutine(Idle());
        }

        IEnumerator Idle()
        {
            ChangeState("Idling");
            Debug.Log("idling");
            yield return new WaitForSeconds(1f);
            isBusy = false;
        }
        IEnumerator Wander()
        {
            Vector3 rndPoint = UnityEngine.Random.insideUnitCircle;
            Vector3 direction = rndPoint - transform.position;
            direction.Normalize();
            transform.LookAt(direction);
            transform.rotation = Quaternion.Euler(90, transform.rotation.y, 0);
            transform.rigidbody.velocity = direction * MovementSpeed;
            yield return new WaitForSeconds(DetermineBreakTime());
            isBusy = false;
        }

        IEnumerator Follow()
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            while (distanceToTarget >= 2f && distanceToTarget <= 8f)
            {
                distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
                transform.LookAt(target.transform);
                transform.rotation = Quaternion.Euler(90, transform.rotation.y, 0);
                Vector3 direction = target.transform.position - transform.position;
                direction.Normalize();
                transform.rigidbody.velocity = direction * MovementSpeed;
                yield return new WaitForEndOfFrame();
            }
            isBusy = false;
        }
        private float DetermineBreakTime()
        {
            return UnityEngine.Random.Range(1.5f, 4f);
        }

        public float MovementSpeed { get; set; }

        void DecideActions()
        {
            if (states["isWandering"] || states["isFollowing"] || states["isIdling"])
            {
                isBusy = true;
                transform.rigidbody.velocity = Vector3.zero;
                float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
                if (distanceToTarget <= 2f)
                {
                    ChangeState("Charging");
                    StartCoroutine(Charge());
                }
                else if (distanceToTarget <= 8f)
                {
                    Debug.Log("following");
                    ChangeState("Following");
                    StartCoroutine(Follow());
                }
                else if (distanceToTarget <= 15f)
                {
                    Debug.Log("wandering");
                    ChangeState("Wandering");
                    StartCoroutine(Wander());
                }
                else
                {
                    ChangeState("Idling");
                    StartCoroutine(Idle());
                    Debug.Log("idling");
                }
            }
        }

        void LocateTarget()
        {
            this.target = GameObject.Find("Player");
        }
    }
}
