using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.UI
{
    class ButtonWrapper : MonoBehaviour
    {
        private Button button { get; set; }

        void Start()
        {
            
        }

        void OnGUI()
        {
            if (GUI.Button(button.position, button.content))
            {
                button.active = !button.active;
            }
        }

        public ButtonWrapper(Button button)
        {
            this.button = button;
        }
    }
}
