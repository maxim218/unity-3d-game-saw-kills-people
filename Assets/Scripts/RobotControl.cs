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
    
    private float CalculateDistanceFromCenter() {
        Vector3 centerPos = Vector3.zero;
        Vector3 heroPos = new Vector3(transform.position.x, 0, transform.position.z);
        float distance = Vector3.Distance(centerPos, heroPos);
        return distance;
    }

    private const float MaxAllowedDistance = 25.1f;

    private void Update() {
        if (_allowUpdate) {
            float distance = CalculateDistanceFromCenter();
            if(distance > MaxAllowedDistance) {
                WallControl script = FindObjectOfType<WallControl>();
                script.KillHero();
            }
        }

        if (_allowUpdate) {
            transform.Translate(0, 0, forwardSpeed * Time.deltaTime);
            int direction = inputControlComp.GetDirection();
            transform.Rotate(0, direction * speedRotating * Time.deltaTime, 0);
        }
    }
}
