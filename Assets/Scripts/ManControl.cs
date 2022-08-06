using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManControl : MonoBehaviour {
    [SerializeField] private GameObject deadPrefab = null;
    
    private void OnTriggerEnter(Collider other) {
        if (GameObject.Find("Robot") == other.gameObject) {
            GameObject dead = Instantiate(deadPrefab) as GameObject;
            dead.transform.position = transform.position;
            ChangeScore();
            RunSkullAnimation();
            Destroy(gameObject);
        }
    }

    private static void RunSkullAnimation() {
        GameObject obj = GameObject.Find("SkullWithAnimation");
        Animator animator = obj.GetComponent<Animator>();
        const string stateName = "SkullAnimationState";
        const int wait = 0;
        animator.CrossFade(stateName, wait);
    }

    private static void ChangeScore() {
        ScoreControllerScript script = FindObjectOfType<ScoreControllerScript>();
        script.ChangeScoreAndRender();
        if (script.IsWin() == true) PlayerWinTheGame();
    }

    private static void PlayerWinTheGame() {
        const string playerWinMsg = "Player win the game";
        Debug.Log(playerWinMsg);

        RobotControl script = FindObjectOfType<RobotControl>();
        script.MovingStop();

        CapsuleCollider collider = script.gameObject.GetComponent<CapsuleCollider>();
        Destroy(collider);

        ScoreControllerScript.RunMenuAnimation();
        ScoreControllerScript scoreController = FindObjectOfType<ScoreControllerScript>();
        scoreController.FinalMessage(false);
    }
}
