using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Camera
{
    class SmoothFollow : MonoBehaviour
    {
        // ReSharper disable once InconsistentNaming
        public GameObject target;
        private Vector3 targetPosition;
        private float initialY;
        void Start()
        {
            initialY = transform.position.y;
        }

        void FixedUpdate()
        {
            Follow();
        }
        void Follow()
        {
            targetPosition = target.transform.position - new Vector3(7,target.transform.position.y + 20,6);
            transform.position = Vector3.Slerp(
                transform.position,
                new Vector3(Mathf.Clamp(targetPosition.x, -100, 100), initialY, Mathf.Clamp(targetPosition.z, -100, 100)),
                0.075f
            );
        }
    }
}
