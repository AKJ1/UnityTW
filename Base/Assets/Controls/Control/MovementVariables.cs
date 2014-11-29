using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Controls
{
    static class MovementVariables
    {

        public delegate void VariableChangedDelegate();
        #region Events
        public static event VariableChangedDelegate DodgeParamsOnChanged;
        public static event VariableChangedDelegate ControlParamsOnChanged;
        public static event VariableChangedDelegate MovementParamsOnChanged;
        public static event VariableChangedDelegate BlockParamsOnChanged;

        public static void OnDodgeChanged()
        {
            if (DodgeParamsOnChanged != null)
            {
                DodgeParamsOnChanged();
            }
        }
        public static void OnControlsChanged()
        {
            if (DodgeParamsOnChanged != null)
            {
                DodgeParamsOnChanged();
            }
        }
        public static void OnMovementChanged()
        {
            if (DodgeParamsOnChanged != null)
            {
                DodgeParamsOnChanged();
            }
        }
        public static void OnBlockChanged()
        {
            if (DodgeParamsOnChanged != null)
            {
                DodgeParamsOnChanged();
            }
        }

        #endregion
        #region Public Variables
        #region Movement Values

        public static float MoveSpeed { get; set; }
        public static float MaxSpeed { get; set; }
        public static float TurnaroundTime { get; set; }
        public static float MovementDecay { get; set; }
        #endregion
        #region Controller
        public static ControlMethod Controller = ControlMethod.Keyboard;
        public static bool ControlsAvailable { get; set; }
        #endregion
        #region PlayerState
        public static PlayerState CurrentPlayerState { get; set; }
        #endregion
        #region Dodge Variables
        public static float DodgeCooldown { get; set; }
        public static float DodgeTime { get; set; }
        public static float DodgeSpeed { get; set; }
        #endregion
        #endregion
        #region Methods

        public static void Init()
        {
            #region Movement Values
            MoveSpeed = 3.5f;
            MaxSpeed = 4.5f;
            TurnaroundTime = 0.1f;
            MovementDecay = 3.0f;
            #endregion

            #region Controller 
            Controller = ControlMethod.Keyboard;
            ControlsAvailable = true;
            #endregion

            #region AnimationState
            CurrentPlayerState = PlayerState.Idle;
            #endregion

            #region Dodge Values

            DodgeCooldown = 0.6f;
            DodgeSpeed = 4f * MaxSpeed;
            DodgeTime = 0.15f;

            #endregion

        }
        #endregion
        public static void AlterMovement(float effect)
        {
            
        }
    }
}
