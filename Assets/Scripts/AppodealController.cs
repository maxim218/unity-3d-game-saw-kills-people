using UnityEngine;

public class AppodealController : MonoBehaviour {
    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    public void AllowSound(string content) {
        if ("YES" == content) {
            AudioListener.volume = 1;
        } else {
            AudioListener.volume = 0;
        }
    }

    public void SetLanguage(string language) {
        PlayerPrefs.SetString("LANGUAGE_GAME", language);
    }

    public void OnRewardedVideoFinished(string message) {
        try {
            AdsSceneController script = FindObjectOfType<AdsSceneController>();
            if (script) script.SuccessWatchVideoFully();
            Debug.Log(message);
        } catch {
            Debug.Log("Error in - On Rewarded Video Finished");
        }
    }

    public void ShowInterstisial() {
        try {
            Application.ExternalCall("MyShowIntersticial", "");
        } catch { }
    }

    public void ShowRewarderVideo() {
        try {
            Application.ExternalCall("MyShowRewarderForMoney", "");
        } catch { }
    }

    public static void PublicRunIntersticial() {
        AppodealController controller = FindObjectOfType<AppodealController>();
        if (controller) controller.ShowInterstisial();
    }

    public static void PublicRunRewarderVideo() {
        AppodealController controller = FindObjectOfType<AppodealController>();
        if (controller) controller.ShowRewarderVideo();
    }
}
