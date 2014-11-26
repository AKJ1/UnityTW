using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Game.Equipment
{
    class WeaponGlobals: MonoBehaviour
    {
        public static GameObject SwordPrefab;
        public GameObject swordPrefab;

        void Start()
        {
            SwordPrefab = swordPrefab;
        }
    }
}
