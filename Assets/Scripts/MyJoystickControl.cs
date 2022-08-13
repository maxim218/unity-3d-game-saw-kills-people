using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyJoystickControl : MonoBehaviour {
    [SerializeField] private Joystick joystickScript = null;

    private int _direction = 0;

    public int Direction => _direction;

    private static int GetIntDirection(float horizontal) {
        if (0.25f < horizontal) 
            return 1;
        if (horizontal < -0.25f) 
            return -1;
        return 0;
    }

    private void Update() {
        if(joystickScript) {
            float horizontal = joystickScript.Horizontal;
            _direction = GetIntDirection(horizontal);
        }
    }

    public void JoystickDeactivateOnScreen() {
        if (joystickScript) 
            joystickScript.gameObject.SetActive(false);
    }
}
