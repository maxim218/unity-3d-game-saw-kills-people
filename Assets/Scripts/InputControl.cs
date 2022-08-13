using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour {
    [SerializeField] private MyJoystickControl myJoystickControl = null;

    public int GetDirection() {
        int direction = myJoystickControl ? myJoystickControl.Direction : 0;
        return direction;
    }
}
