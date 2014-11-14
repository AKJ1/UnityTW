using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Controls
{
    class AnimationHandler : MonoBehaviour
    {
        void Animate(PlayerState currentState)
        {
            Animator animator = transform.GetComponent<Animator>();
            
            switch (currentState)
            {
                case PlayerState.Attacking:
                    animator.Play("Attacking" + AnimationVariables)
                    
            }
        }
        #region Unity Methods
        void Update()
        {
            Animate(MovementVariables.CurrentPlayerState);
        }
        void Start()
        {

        }
        #endregion
    }
}
