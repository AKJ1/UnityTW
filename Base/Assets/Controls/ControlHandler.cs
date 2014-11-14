using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Controls
{
    public class ControlHandler : MonoBehaviour
    {
        #region Delegates
        public delegate void ControllerChangedDelegate(object sender, ControlMethod method);
        public delegate void ControlDelegate();
        public delegate IEnumerator ControlCheckDelegate();
        #endregion
        #region Variables
        public event ControllerChangedDelegate ControllerChanged;
        public static ControlCheckDelegate UpdateControls;
        private static ControlMethod controller;
        private static bool init;
        public ControlMethod Controller
        {
            get { return controller; }
            set
            {
                controller = value;
                this.OnControllerChanged(value);
                if (this.ControllerChanged == null)
                {
                    Debug.DebugBreak();
                };

            }
        }
        #endregion
        #region Events

        public virtual void OnControllerChanged(ControlMethod ctrl)
        {
            if (ControllerChanged != null)
            {
                ControllerChanged(this, ctrl);
            }
        }
        #endregion
        #region Unity Methods

        public void Init()
        {
            //if (!init)
            //{
            //   
            //    this.Controller = MovementVariables.Controller;
            
            if (UpdateControls == null & !init)
            {
                UpdateControls += CheckControllerMovement;
                StartCoroutine(UpdateControls());
                init = true;
            }    
            //    
            //}
        }
        public virtual void Start()
        {
        }
        #endregion

        #region Controler Check Logic
        IEnumerator CheckControllerMovement()
        {;
            while (true)
            {
                if (this.Controller == ControlMethod.Joystick)
                {
                    bool check = Input.GetKey(KeyCode.W) ||
                                Input.GetKey(KeyCode.A) ||
                                Input.GetKey(KeyCode.S) ||
                                Input.GetKey(KeyCode.D);
                    if (check)
                    {
                        this.Controller = ControlMethod.Keyboard;
                    }
                }
                else
                {
                    bool check = Mathf.Abs(Input.GetAxis("VerticalRight")) +
                    Mathf.Abs(Input.GetAxis("HorizontalRight")) > 0.5;
                    if (check)
                    {
                        this.Controller = ControlMethod.Joystick;
                    }
                }
                yield return new WaitForSeconds(0.2f);
            }
        // ReSharper disable once FunctionNeverReturns
        }
        #endregion
        
    }
}
