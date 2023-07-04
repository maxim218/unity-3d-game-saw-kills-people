using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreControllerScript : MonoBehaviour {
    [SerializeField] private Text textComponent = null;

    [SerializeField] private Text finalResultTextComponent = null;
    
    private int _length = 0;
    private int _score = 0;

    private void RenderScore() {
        string content = _score + " / " + _length;
        textComponent.text = content;
    }

    [SerializeField] private FabricSounds fabricSoundsScript = null;

    public void PlayKillMonstSound() {
        if (fabricSoundsScript) {
            const string soundType = FabricSounds.SOUND_MONSTR_KILL_PREFAB_CONST;
            fabricSoundsScript.CreateSound(soundType);
        }
    }
    private void PlaySound(bool isHeroLose) {
        if (fabricSoundsScript) {
            const string a = FabricSounds.SOUND_HIT_WALL_PREFAB_CONST;
            const string b = FabricSounds.SOUND_PLAYER_WIN_GAME_PREFAB_CONST;
            string soundType = isHeroLose ? a : b;
            fabricSoundsScript.CreateSound(soundType);
        } else {
            const string warnMsg = "Warning - fabricSoundsScript is null";
            Debug.LogWarning(warnMsg);
        }
    }

    public void FinalMessage(bool isHeroLose) {
        int numStatistics = LevelIndexManager.GetLevelIndex();
        int resultStatistics = isHeroLose ? 0 : 1;
        AppodealController.AppodealEvent_level_finished(numStatistics, resultStatistics);

        if (isHeroLose) {
            const string one = "Your score";
            string two = _score + " / " + _length;
            const string three = "Tap to restart";
            finalResultTextComponent.text = one + '\n' + two + '\n' + three;
        } else {
            const string lineA = "You are winner";
            const string lineB = "Nice game";
            const string lineC = "Tap to continue";
            finalResultTextComponent.text = lineA + '\n' + lineB + '\n' + lineC; 
            LevelIndexManager.MakeNextLevelIndex();
        }

        MyJoystickControl script = FindObjectOfType<MyJoystickControl>();
        if (script) script.JoystickDeactivateOnScreen();

        StartCoroutine(ShowHugeBoxAsync());

        PlaySound(isHeroLose);
    }

    private IEnumerator ShowHugeBoxAsync() {
        const float waitVal = 1.1f;
        yield return new WaitForSeconds(waitVal);
        if (hugeImageObj) hugeImageObj.SetActive(true);
    }

    [SerializeField] private GameObject hugeImageObj = null;

    public static void RunMenuAnimation() {
        GameObject obj = GameObject.Find("MenuFon");
        Animator anim = obj.GetComponent<Animator>();
        const string stateName = "MenuFonAnimation";
        const int wait = 0;
        anim.CrossFade(stateName, wait);
    }

    private void Start() {
        LevelBuilder levelBuilder = LevelBuilder.GetLevelBuilderObject(); 
        levelBuilder.LoadLevelBlock();
        _length = levelBuilder.GetEnemiesNumber();
        RenderScore();
    }

    public void ChangeScoreAndRender() {
        _score++;
        RenderScore();
        RunAnimation();
    }

    public bool IsWin() {
        return (_length == _score);
    }

    private void RunAnimation() {
        Animator anim = gameObject.GetComponent<Animator>();
        const string stateName = "TextLabelAnimation";
        const int wait = 0;
        anim.CrossFade(stateName, wait);
    }
}
