using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotControl : MonoBehaviour {
    [SerializeField] private float forwardSpeed = 0;
    [SerializeField] private float speedRotating = 0;
    
    [SerializeField] private InputControl inputControlComp = null;

    private bool _allowUpdate = true;

    public void MovingStop() {
        _allowUpdate = false;
    }
    
    private void Update() {
        if (_allowUpdate) {
            transform.Translate(0, 0, forwardSpeed * Time.deltaTime);
            int direction = inputControlComp.GetDirection();
            transform.Rotate(0, direction * speedRotating * Time.deltaTime, 0);
        }
    }
}
