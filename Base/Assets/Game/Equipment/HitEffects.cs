using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Game.Equipment
{
    
    public delegate void HitEffectsDelegate(Collider c, GameObject go);

    public class HitEffects : MonoBehaviour
    {
        /// <summary>
        /// Hit actions are applied to the foregin transform. 
        /// This is an adapter class that connects the logic 
        /// from within the weapon classes to the projectile's
        /// MonoBehaviour OnTriggerEnter Method.
        /// This is done so that the variables within the 
        /// weapon classes can be reused without being passsed
        /// on to the HitEffects Behaviour. 
        /// </summary>
        public HitEffectsDelegate HitActions;
        void OnTriggerEnter(Collider c)
        {
            HitActions(c, gameObject);
        }
    }
}
