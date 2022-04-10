using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour {
    private bool _a = false;
    private bool _d = false;
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.A))  _a = true;
        if (Input.GetKeyDown(KeyCode.D)) _d = true;
        
        if (Input.GetKeyUp(KeyCode.A))  _a = false;
        if (Input.GetKeyUp(KeyCode.D)) _d = false;
    }

    public int GetDirection() {
        if (_a) return -1;
        if (_d) return 1;
        return 0;
    }
}
