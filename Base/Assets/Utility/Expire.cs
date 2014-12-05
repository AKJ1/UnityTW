using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Utility
{
    class Expire : MonoBehaviour
    {
        public float Lifetime;
        void Start()
        {
            Destroy(gameObject,Lifetime);
        }
    }
}
