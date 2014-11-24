using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Controls;
using UnityEngine;

namespace Assets.Control_Scripts.Control
{
    class DodgeHandler : MonoBehaviour
    {
        #region Variables
        public void Deactivate()
        {
            this.isActive = false;
        }

        public void Activate()
        {
            this.isActive = true;
        }

        private bool isActive;
        private float dodgeSpeed;
        private float dodgeTime;
        private float dodgeCooldown;
        private bool dodgeAvailable;
        #endregion
        #region Unity Methods
        void FixedUpdate()
        {
            if (isActive)
            {
                DodgeTrigger();   
            }
        }

        void Start()
        {
            InitializeValues();
        }
        #endregion
        #region Variable Setup Methods
        void InitializeValues()
        {
            this.dodgeSpeed = MovementVariables.DodgeSpeed;
            this.dodgeTime = MovementVariables.DodgeTime;
            this.dodgeCooldown = MovementVariables.DodgeCooldown;
            this.Activate();
            this.dodgeAvailable = true;
        }
        #endregion
        #region Dodge Logic
        void DodgeTrigger()
        {
            if (Input.GetButton("Dodge") && MovementVariables.ControlsAvailable && this.dodgeAvailable )
            {
                StartCoroutine(Dodge());
            }   
        }

        private IEnumerator Dodge()
        {
            
            transform.rigidbody.velocity = gameObject.GetComponent<MovementHandler>().MovementDirection.normalized * dodgeSpeed;
            MovementVariables.ControlsAvailable = false;
            this.dodgeAvailable = false;
            StartCoroutine(Animate());
            yield return new WaitForSeconds(this.dodgeTime);
            transform.rigidbody.velocity = Vector3.zero;
            MovementVariables.ControlsAvailable = true;
            yield return new WaitForSeconds(this.dodgeCooldown);
            this.dodgeAvailable = true;
            yield break;
        }

        private IEnumerator Animate()
        {
            TrailRenderer trail = gameObject.GetComponent<TrailRenderer>();
            trail.enabled = true;
            yield return new WaitForSeconds(dodgeTime + trail.time*1.2f);
            trail.enabled = false;
            yield break;
        }
        #endregion
    }
}
