﻿using System;
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
        public static GameObject DaggerPrefab;
        public GameObject daggerPrefab;
        public static GameObject HammerPrefab;
        public GameObject hammerPrefab;
        public static GameObject BowProjectile;
        public GameObject bowProjectile;
        public static GameObject BowProjectileExact;
        public GameObject bowProjectileExact;
        public static GameObject ShotgunProjectile;
        public GameObject shotgunProjectile;


        void Start()
        {
            SwordPrefab = swordPrefab;
            DaggerPrefab = daggerPrefab;
            HammerPrefab = hammerPrefab;
            BowProjectile = bowProjectile;
            BowProjectileExact = bowProjectileExact;
            ShotgunProjectile = shotgunProjectile;
        }
    }
}
