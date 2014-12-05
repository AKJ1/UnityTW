using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Game.Equipment;
using UnityEngine;

namespace Assets.Game.Characters
{
    abstract class Enemy : Character
    {
        #region Variables
        public float MovementSpeed { get; set; }
        protected bool IsBusy;        
        protected Dictionary<string, bool> States = new Dictionary<string, bool>();
        protected GameObject Target;
        protected Color StandardColor = new Color(255, 255, 255);
        protected Color AttackColor = new Color(255, 0, 0);
        #endregion

        #region Unity Methods
        //Default stats - Start is overriden on every other enemy.
        void Start()
        {
            LocateTarget();
            this.transform.tag = "Enemy";
            this.MaxHealth = 100;
            this.Health = MaxHealth;
            this.InvulnerabilityTime = 0.08f;
            this.MovementSpeed = 2.0f;
            this.Invulnerable = false;
            States.Add("isFollowing", false);
            States.Add("isWandering", false);
            States.Add("isCharging", false);
            States.Add("isIdling", true);
        }
        #endregion
        
        #region Behaviours
        protected IEnumerator Idle()
        {
            ChangeState("Idling");
            Debug.Log("idling");
            yield return new WaitForSeconds(1f);
            IsBusy = false;
        }
        protected IEnumerator Wander()
        {
            Vector3 rndPoint = UnityEngine.Random.insideUnitCircle;
            Vector3 direction = rndPoint - transform.position;
            direction.Normalize();
            transform.LookAt(direction);
            transform.rotation = Quaternion.Euler(90, transform.rotation.y, 0);
            transform.rigidbody.velocity = direction * MovementSpeed;
            yield return new WaitForSeconds(DetermineBreakTime());
            IsBusy = false;
        }
        protected IEnumerator Follow()
        {
            float distanceToTarget = Vector3.Distance(transform.position, Target.transform.position);
            while (distanceToTarget >= 2f && distanceToTarget <= 8f)
            {
                distanceToTarget = Vector3.Distance(transform.position, Target.transform.position);
                transform.LookAt(Target.transform);
                transform.rotation = Quaternion.Euler(90, transform.rotation.y, 0);
                Vector3 direction = Target.transform.position - transform.position;
                direction.Normalize();
                transform.rigidbody.velocity = direction * MovementSpeed;
                yield return new WaitForEndOfFrame();
            }
            IsBusy = false;
        }
        #endregion

        #region Helper Methods
        protected float DetermineBreakTime()
        {
            return UnityEngine.Random.Range(1.5f, 4f);
        }

        protected void ChangeState(string newState)
        {
            foreach (var state in States.ToArray())
            {
                States[state.Key] = false;
            }
            switch (newState)
            {
                case "Following":
                    States["isFollowing"] = true;
                    break;
                case "Charging":
                    States["isCharging"] = true;
                    break;
                case "Wandering":
                    States["isWandering"] = true;
                    break;
                case "Idling":
                    States["isIdling"] = true;
                    break;
            }
        }
        protected void LocateTarget()
        {
            this.Target = GameObject.Find("Player");
        }
        #endregion  

        #region Abstract Methods
        protected abstract void DecideActions();
        #endregion
    }
}
