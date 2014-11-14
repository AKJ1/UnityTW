using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Controls
{
    static class MovementVariables
    {
        #region Movement Values
        public static float MoveSpeed { get; set; }
        public static float MaxSpeed { get; set; }
        public static float TurnaroundTime { get; set; }
        public static float MovementDecay { get; set; }
        public static ControlMethod Controller = ControlMethod.Keyboard;
        public static PlayerState CurrentPlayerState { get; set; }
        #endregion
        
        public static void AlterMovement(float effect)
        {
            
        }
    }
}
