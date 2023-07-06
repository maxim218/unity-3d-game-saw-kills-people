using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeControl : MonoBehaviour {
    [SerializeField] private Material [] _matBladeArray = null;

    private void InitMaterial() {
        try {
            int index = PlayerPrefs.GetInt("BLADE_COLOR_INDEX", 0);
            Material material = _matBladeArray[index];
            if (material) GetComponent<Renderer>().material = material;
        } catch {
            // empty
        }
    }

    void Start() {
        try {
            InitMaterial();
        } catch {
            // empty
        }
    }
}
