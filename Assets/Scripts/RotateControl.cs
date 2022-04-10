using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateControl : MonoBehaviour {
    [SerializeField] private float speedRotating = 0;
    
    private void Update() {
        transform.Rotate(0, speedRotating * Time.deltaTime, 0, Space.World);
    }
}
