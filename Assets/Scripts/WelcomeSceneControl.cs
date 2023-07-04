using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeSceneControl : MonoBehaviour {
    private void MinimumMoneySet() {
        int totalMoney = PlayerPrefs.GetInt("TOTAL_MONEY_KEY", 0);
        if (totalMoney < 85) PlayerPrefs.SetInt("TOTAL_MONEY_KEY", 85);
        PlayerPrefs.Save();
    }

    private void RenderMoneyValue() {
        int totalMoney = PlayerPrefs.GetInt("TOTAL_MONEY_KEY", 0);
        Debug.Log("Total money: " + totalMoney);
    }

    void Start() {
        MinimumMoneySet();
        RenderMoneyValue();
    }
}
