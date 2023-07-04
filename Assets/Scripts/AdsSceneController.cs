using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdsSceneController : MonoBehaviour {
    [SerializeField] private Text _label = null;

    [SerializeField] private Text _labelTopRight = null;

    [SerializeField] private GameObject _blockA = null;

    [SerializeField] private GameObject _blockB = null;

    [SerializeField] private Text _labelContinuePlaying = null;

    [SerializeField] private GameObject _background = null;

    [SerializeField] private GameObject _blockC = null;

    [SerializeField] private SceneLoader _sceneLoader = null;

    private int _levelNumber = 0;
    private int _money = 0;
    private int _totalMoney = 0;
    private int _nextLevelPrice = 0;

    private void InitPriceAndGotMoney() {
        _levelNumber = LevelIndexManager.GetLevelIndex();
        _nextLevelPrice = CalculatePriceForRunningNextLevel(_levelNumber);
        _money = CalculateMoneyByLevel(_levelNumber);
    }

    private void PrintInitedValues() {
        Debug.Log("Level Number: " + _levelNumber);
        Debug.Log("Next Level Price: " + _nextLevelPrice);
        Debug.Log("Money: " + _money);
    }

    private void InitTotalMoney() {
        _totalMoney = GetTotalMoney() + _money;
        SetTotalMoney(_totalMoney);
        _totalMoney = GetTotalMoney();
        _labelTopRight.text = "" + _totalMoney + "";
    }

    private void CheckTotatMoneyIsPositive() {
        _totalMoney = GetTotalMoney();
        if (_totalMoney < 0) _totalMoney = 0;
        SetTotalMoney(_totalMoney);
        _totalMoney = GetTotalMoney();
        _labelTopRight.text = "" + _totalMoney + "";
    }

    private void RenderLabels() {
        _label.text = GetGoldText(_money);
        _labelTopRight.text = "" + _totalMoney + "";
        _labelContinuePlaying.text = GetContinuePlayingText(_nextLevelPrice);
    }

    void Start()  {
        CheckTotatMoneyIsPositive();
        InitPriceAndGotMoney();
        PrintInitedValues();
        InitTotalMoney();
        RenderLabels();
        HideAllBlocks();
        _blockA.SetActive(true);
        CheckTotatMoneyIsPositive();

        // appodeal
        if (_levelNumber > 3) 
            AppodealController.PublicRunIntersticial();
    }

    private void HideAllBlocks() {
        _blockA.SetActive(false);
        _blockB.SetActive(false);
        _blockC.SetActive(false);
        _background.SetActive(false);
        CheckTotatMoneyIsPositive();
    }

    private int CalculateMoneyByLevel(int levelNumber) {
        int money = (2 + levelNumber) * 5;
        return money;
    }
    
    private int CalculatePriceForRunningNextLevel(int levelNumber) {
        int price = (2 + levelNumber) * 5 + levelNumber * 4 + 2;
        return price;
    }

    private string GetGoldText(int value) {
        string a = "Congratulations!";
        string b = $"You earned {value} gold!";
        return a + "\n" + b;
    }

    private string GetContinuePlayingText(int value) {
        string x = "Continue playing";
        string y = $"Run this level price - {value} gold";
        return x + "\n" + y;
    }

    private int GetTotalMoney() {
        int totalMoney = PlayerPrefs.GetInt("TOTAL_MONEY_KEY", 0);
        return totalMoney;
    }

    private void SetTotalMoney(int totalMoney) {
        PlayerPrefs.SetInt("TOTAL_MONEY_KEY", totalMoney);
        PlayerPrefs.Save();
    }

    public void ButtonGetMoreGold() {
        // appodeal
        AppodealController.PublicRunRewarderVideo();
    }

    public void SuccessWatchVideoFully() {
        // get money for finishing watching video
        CheckTotatMoneyIsPositive();
        InitTotalMoney();
        InitTotalMoney();
        CheckTotatMoneyIsPositive();
        ButtonCloseDialogWindow();
        Debug.Log("Player got reward - OK");
    }

    public void ButtonCloseDialogWindow() {
        HideAllBlocks();
        _blockB.SetActive(true);
        CheckTotatMoneyIsPositive();
    }

    public void PlayButtonClick() {
        CheckTotatMoneyIsPositive();
        HideAllBlocks();
        if (_nextLevelPrice > GetTotalMoney()) {
            _blockC.SetActive(true);
        } else {
            _background.SetActive(true);
            PayForLevelOpening(); 
            _sceneLoader.MoveToScene();
        }
        CheckTotatMoneyIsPositive();
    }

    private void PayForLevelOpening() {
        CheckTotatMoneyIsPositive();
        _totalMoney = GetTotalMoney() - _nextLevelPrice;
        SetTotalMoney(_totalMoney);
        _totalMoney = GetTotalMoney();
        _labelTopRight.text = "" + _totalMoney + "";
        CheckTotatMoneyIsPositive();
    }

    public void OkBtnClick() {
        HideAllBlocks();
        _blockA.SetActive(true);
        CheckTotatMoneyIsPositive();
    }
}
