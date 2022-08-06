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

    public void FinalMessage() {
        const string one = "Your score";
        string two = _score + " / " + _length;
        const string three = "Tap to restart";
        finalResultTextComponent.text = one + '\n' + two + '\n' + three;
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

    private void RunAnimation() {
        Animator anim = gameObject.GetComponent<Animator>();
        const string stateName = "TextLabelAnimation";
        const int wait = 0;
        anim.CrossFade(stateName, wait);
    }
}
