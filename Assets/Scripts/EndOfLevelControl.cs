using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevelControl : MonoBehaviour {
    [SerializeField] private SceneLoader sceneLoaderScript = null;

    private bool _allowClickFlag = true;

    public void EndOfLevel() {
        if (_allowClickFlag) {
            _allowClickFlag = false;
            const string message = "End Of Level - user click";
            Debug.Log(message);
            GameObject menuFon = GameObject.Find("MenuFon");
            if (menuFon) menuFon.SetActive(false);
            sceneLoaderScript.MoveToScene();
        }
    }
}
