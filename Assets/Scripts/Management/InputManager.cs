using System;
using UnityEngine;

namespace Management
{
    public class InputManager : MonoBehaviour
    {
        public Action<float, float, bool> MovementInputReceived;
        //public Action<bool> DashInputReceived; 

        public void FixedUpdate()
        {
            MovementInputReceived?.Invoke(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), Input.GetKeyDown(KeyCode.LeftShift));
            //DashInputReceived?.Invoke(Input.GetKeyDown(KeyCode.LeftShift));
        }


    }
}
