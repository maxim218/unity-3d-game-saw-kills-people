using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;

public class AppodealController : MonoBehaviour {
    private const string appKey = "3f0446ae75ddd5563e3c0f402fb1fdf305ab42652c4df57f";

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        try {
            int adTypes = AppodealAdType.Interstitial | AppodealAdType.RewardedVideo;
            AppodealCallbacks.Sdk.OnInitialized += OnInitializationFinished;
            AppodealCallbacks.RewardedVideo.OnFinished += OnRewardedVideoFinished;
            Appodeal.Initialize(appKey, adTypes);
        } catch {
            const string errorMsg = "Error in - Appodeal Start Method";
            Debug.Log(errorMsg);
        }
    }

    public void OnInitializationFinished(object sender, SdkInitializedEventArgs e) {
        try {
            Appodeal.Cache(AppodealAdType.Interstitial);
            Appodeal.Cache(AppodealAdType.RewardedVideo);
            AppodealEvent_app_start();
        } catch {
            const string errorMsg = "Error in - Running Appodeal Cache Methods";
            Debug.Log(errorMsg);
        }
    }

    public void OnRewardedVideoFinished(object sender, RewardedVideoFinishedEventArgs e) {
        try {
            AdsSceneController script = FindObjectOfType<AdsSceneController>();
            if (script) script.SuccessWatchVideoFully();
            AppodealEvent_rewarded();
        } catch {
            Debug.Log("Error in - On Rewarded Video Finished");
        }
    }

    public void ShowInterstisial() {
        try {
            if (Appodeal.IsLoaded(AppodealAdType.Interstitial))
                Appodeal.Show(AppodealShowStyle.Interstitial);
        } catch {
            const string errorMsg = "Error in - Show Interstisial";
            Debug.Log(errorMsg);
        }
    }

    public void ShowRewarderVideo() {
        try {
            if (Appodeal.IsLoaded(AppodealAdType.RewardedVideo))
                Appodeal.Show(AppodealShowStyle.RewardedVideo);
        } catch {
            const string errorMsg = "Error in - Show Rewarder Video";
            Debug.Log(errorMsg);
        }
    }

    public static void PublicRunIntersticial() {
        AppodealController controller = FindObjectOfType<AppodealController>();
        if (controller) controller.ShowInterstisial();
    }

    public static void PublicRunRewarderVideo() {
        AppodealController controller = FindObjectOfType<AppodealController>();
        if (controller) controller.ShowRewarderVideo();
    }

    public static void AppodealEvent_app_start() {
        try {
            Appodeal.LogEvent("app_start");
        } catch {
            Debug.Log("APPODEAL EVENT ERROR");
        }
    }

    public static void AppodealEvent_rewarded() {
        try {
            Appodeal.LogEvent("rewarded");
        } catch {
            Debug.Log("APPODEAL EVENT ERROR");
        }
    }

    public static void AppodealEvent_level_start(int num) {
        try {
            Appodeal.LogEvent("level_start", new Dictionary<string, object> {
                { "num", num }
            });
        } catch {
            Debug.Log("APPODEAL EVENT ERROR");
        }
    }

    public static void AppodealEvent_level_finished(int num, int result) {
        try {
            Appodeal.LogEvent("level_finished", new Dictionary<string, object> {
                { "num", num },
                { "result", result }
            });
        } catch {
            Debug.Log("APPODEAL EVENT ERROR");
        }
    }
}
