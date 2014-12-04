using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Controls
{
    class AimHandler : ControlHandler
    {
        public Vector3 AimDirection;
        private ControlDelegate activeScheme;

        private LayerMask mask = 8;
        #region Events
        private void SwitchMovement(object sender, ControlMethod ctrl)
        {
            activeScheme = null;
            switch (ctrl)
            {
                case ControlMethod.Joystick:
                    this.activeScheme += JoystickLookAt;
                    break;
                case ControlMethod.Keyboard:
                    this.activeScheme += KeyboardLookAt;
                    break;
            }
        }
        #endregion
        #region UnityMethods
        public override void Start()
        {
            base.Init();
            this.ControllerChanged += new ControllerChangedDelegate(SwitchMovement);
            this.Controller = MovementVariables.Controller;
        }

        void LateUpdate()
        {
            if (activeScheme != null)
            {
                activeScheme();
                transform.rigidbody.angularVelocity = Vector3.zero;
            }
            else
            {
                //this.Controller = ControlMethod.Keyboard;
            }
        }
        #endregion
        #region Control Logic
        private void JoystickLookAt()
        {
            Vector3 diff = new Vector3(Input.GetAxis("VerticalRight") * 100, 0f, Input.GetAxis("HorizontalRight") * 100);
            if (Mathf.Abs(diff.x) > 0.2 || Mathf.Abs(diff.z) > 0.2)
            {
                var angle = Mathf.Atan2(diff.x, diff.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(90, -angle - 45, 0), 0.4f);
            }
            else
            {
                LookForward();
            }
        }
        private void KeyboardLookAt()
        {
            if (Input.mousePosition.x < Screen.width ||
                Input.mousePosition.x > 0 ||
                Input.mousePosition.y < Screen.height ||
                Input.mousePosition.y > 0)
            {
                Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 200f, 1 << LayerMask.NameToLayer("AimMesh")))
                {
                    Vector3 targetPosition = hit.point - transform.position;
                    targetPosition.Normalize();
                    var angle = Mathf.Atan2(targetPosition.x, targetPosition.z) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(90, angle, 0), 0.3f);
                    this.AimDirection = Quaternion.Euler(90, angle, 0) * (Vector3.forward + Vector3.right);
                }
            }
            else
            {
                LookForward();
            }
        }
        private void LookForward()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            var angle = Mathf.Atan2(x, z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(90, angle + 45, 0), 0.4f);
        }
        #endregion
    }

}
