using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallControl : MonoBehaviour {
    [SerializeField] private GameObject deadPrefab = null;
    
    public void KillHero() {
        RobotControl script = FindObjectOfType<RobotControl>();
        script.MovingStop();

        ScoreControllerScript.RunMenuAnimation();
        ScoreControllerScript scoreController = FindObjectOfType<ScoreControllerScript>();
        scoreController.FinalMessage(true);

        GameObject dead = Instantiate(deadPrefab) as GameObject;
        dead.transform.position = GameObject.Find("Robot").transform.position;
        HideRobotParts();
    }

    private void OnTriggerEnter(Collider other) {
        if (GameObject.Find("Robot") == other.gameObject) {
            KillHero();
        }
    }

    private static void HideRobotParts() {
        // hide fire from back
        GameObject.Find("Robot_D_part").SetActive(false);
        // set saw rotation
        GameObject.Find("Robot_B_part").transform.localRotation = Quaternion.Euler(-70, 70, 70);
        // stop rotating saw
        RotateControl rotateControlScript = GameObject.Find("Robot_B_part").GetComponent<RotateControl>();
        Destroy(rotateControlScript);
    }
}
