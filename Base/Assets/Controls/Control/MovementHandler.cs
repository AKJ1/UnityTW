using System.Collections;
using Assets.Game.Characters;
using UnityEngine;

namespace Assets.Controls
{
    public class MovementHandler : ControlHandler
    {
        #region Delegates
        // Inappropriate Naming ?? 
        public delegate void SpeedDelegate(object sender, float speed);
        public delegate void DirectionDelegate(Object sender, Vector3 direction);
        #endregion
        #region Variables
        public Vector2 Movement { get; private set; }
        public float MoveSpeed { get; set; }
        public float MaxSpeed { get; set; }
        public float TurnaroundTime { get; set; }

        public float CurrentSpeed
        {
            get { return this.currentSpeed; }
            set
            {
                this.currentSpeed = value;
                SpeedChanged(this, value);
            }
        }

        private float currentSpeed;

        public event SpeedDelegate SpeedChanged;
        public event DirectionDelegate DirectionChanged;
        public Vector3 MovementDirection;
        #endregion
        #region Events

        private void OnSpeedChanged(float speed)
        {
            if (SpeedChanged != null)
            {
                SpeedChanged(this, speed);
            }
        }

        private void OnDirectionChanged(Vector3 direction)
        {
            if (DirectionChanged != null)
            {
                DirectionChanged(this, direction);
            }
        }

        #endregion
        #region UnityMethods

        void Update()
        {
            Move();
            UpdateState();
        }

        public override void Start ()
        {
            //base.Init(); What the fuck is wrong with this?!
            this.MoveSpeed = 4;
            this.MaxSpeed = this.MoveSpeed*1.10f;
            this.TurnaroundTime = 0.1f;
        }
        
        #endregion
        /*Animate Method needs to be moved, it's here for demo purposes.*/
        void UpdateState()
        {
            
            if (MovementVariables.CurrentPlayerState == PlayerState.Idle ||
                MovementVariables.CurrentPlayerState == PlayerState.Running ||
                MovementVariables.CurrentPlayerState == PlayerState.Walking)
            {
                float x = Input.GetAxis("Horizontal") + Input.GetAxis("Vertical");
                if (x > 0.1 && x < 1.2)
                {
                    MovementVariables.CurrentPlayerState = PlayerState.Walking;
                }else if (x > 1.2)
                {
                    MovementVariables.CurrentPlayerState = PlayerState.Running;
                }
                else if (x < 0.05)
                {
                    MovementVariables.CurrentPlayerState = PlayerState.Idle;
                }
            }
        }
        
        #region Control Logic
        private void Move()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 deltaX = x * (Quaternion.Euler(30, 45, 0) * Vector3.right);
            Vector3 deltaZ = z * (Quaternion.Euler(0, 45, 30) * Vector3.forward);
            this.MovementDirection = (deltaZ + deltaX).normalized * this.MoveSpeed;
            transform.rigidbody.velocity = this.MovementDirection;
        }
        #endregion
    }
}
