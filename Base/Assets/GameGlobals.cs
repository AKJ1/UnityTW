using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    class GameGlobals : MonoBehaviour
    {
        public static Font GameFont;
        public Font gameFont;
        public static Font DamageFont;
        public Font damageFont;

        void Start()
        {
            GameFont = gameFont;
            DamageFont = damageFont;
        }
    }
}
