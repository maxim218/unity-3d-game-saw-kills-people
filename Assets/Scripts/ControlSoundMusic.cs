using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSoundMusic : MonoBehaviour {
    [SerializeField] private GameObject targetCamera = null;

    public void SetTargetCamera(GameObject obj) {
        targetCamera = obj;
    }

    private void LateUpdate() {
        if (targetCamera) {
            transform.position = targetCamera.transform.position;
        }
    }

    [SerializeField] private AudioClip clipMy = null;

    [SerializeField] private AudioSource audioSource = null;

    [SerializeField] private float volumeFloat = 1;

    [SerializeField] private bool loopSound = false;

    private void Start() {
        audioSource.volume = volumeFloat;
        audioSource.clip = clipMy;
        audioSource.Stop();

        if (false == loopSound) {
            audioSource.PlayOneShot(clipMy); 
            StartCoroutine( KillMe() );
        } else {
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    private IEnumerator KillMe() {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}

