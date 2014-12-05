using System.Collections;
using UnityEngine;

namespace Assets.Controls.Control
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
        private bool init;
        #endregion
        #region Unity Methods
        void FixedUpdate()
        {

            if (!init)
            {
                init = true;
                InitializeValues();
            }
            if (isActive)
            {
                DodgeTrigger();
            }
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
            if (Input.GetButton("Dodge") && MovementVariables.ControlsAvailable && this.dodgeAvailable)
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
            yield return new WaitForSeconds(dodgeTime + trail.time * 1.2f);
            trail.enabled = false;
            yield break;
        }
        #endregion
    }
}