using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Camera
{
    class SmoothFollow : MonoBehaviour
    {
        public GameObject target;
        private Vector3 targetPosition;
        private float initialZ;
        void Start()
        {
            initialZ = transform.position.z;
        }

        void FixedUpdate()
        {
            Follow();
        }
        void Follow()
        {
            targetPosition = target.transform.position - new Vector3(7,6);
            transform.position = Vector3.Slerp(
                transform.position,
                new Vector3(Mathf.Clamp(targetPosition.x, -100, 100), Mathf.Clamp(targetPosition.y, -100, 100), initialZ),
                0.075f
            );
            //UnityEngine.Camera.main.transform.LookAt(target.transform);
            //transform.position = new Vector3(Mathf.Clamp(target.transform.position.x, -target.transform.position.x-3, target.transform.position.x+3)*Mathf.Sign(target.transform.position.x),
            //                                 Mathf.Clamp(target.transform.position.y, -150, 150),
            //                                 transform.position.z);
        }
    }
}
