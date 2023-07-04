using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeSceneControl : MonoBehaviour {
    public void RateUsBtnClick() {
        try {
            const string Url = "https://play.google.com/store/apps/details?id=com.K.O.MaxGames.KillingSaw";
            Application.OpenURL(Url.Trim());
        } catch {
            Debug.Log("Error in - Rate Us Btn Click");
        }
    }

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
