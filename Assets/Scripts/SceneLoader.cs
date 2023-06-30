using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    [SerializeField] private string sceneNameString = string.Empty;

    [SerializeField] private GameObject activateMe = null;
    [SerializeField] private GameObject deactivateMe = null;
    [SerializeField] private GameObject killingSawLabel = null;

    private void ControlActDeact() {
        if (deactivateMe) deactivateMe.SetActive(false);
        if (activateMe) activateMe.SetActive(true);
        if (killingSawLabel) killingSawLabel.SetActive(false);
    }

    private IEnumerator LoadSceneAsync() {
        yield return new WaitForSeconds(1);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneNameString);
        while (!operation.isDone) yield return new WaitForSeconds(1);
    }

    private bool _allowClc = true;

    public void MoveToScene() {
        if (_allowClc) {
            _allowClc = false; 
            ControlActDeact();
            StartCoroutine(LoadSceneAsync());
        }
    }
}
