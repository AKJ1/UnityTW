using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Controls
{
    class StateHandler
    {
        #region Variables
        public List<PlayerState> extraStates;
        private Dictionary<PlayerState, bool> activeStates;
        #endregion
        private void setActiveState()
        {
            MovementVariables.CurrentPlayerState = determineActiveState();
        }
        #region Unity Methods
        public void Update()
        {
            setActiveState();
        }
        public void Start()
        {

        }
        #endregion

        private PlayerState determineActiveState()
        {
            if (activeStates[PlayerState.Dodging])
            {
                return PlayerState.Dodging;
            }
            else if (activeStates[PlayerState.Attacking])
            {
                return PlayerState.Attacking;
            }
            else if (activeStates[PlayerState.Blocking])
            {
                return PlayerState.Blocking;
            }
            else if (activeStates[PlayerState.Running])
            {
                return PlayerState.Running;
            }
            else if (activeStates[PlayerState.Walking])
            {
                return PlayerState.Walking;
            }
            return PlayerState.Idle;
        }
        public void SetState(PlayerState state, bool value){
            this.activeStates[state] = value;
        }
    }
}
