using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class TranslationWordData {
    public string KeyIdentifier = string.Empty;
    public string Eng = string.Empty;
    public string Rus = string.Empty;
    public string Tur = string.Empty;
};

[Serializable]
public class WordsContainer {
    public TranslationWordData [] ContentArray = null;
};

public class ControlTextLabels : MonoBehaviour
{
    private static string GetLang() {
        try {
            string language = PlayerPrefs.GetString("LANGUAGE_GAME", string.Empty);
            language = language.ToUpper().Trim();
            if (language.Contains("EN")) return "Eng";
            if (language.Contains("RU")) return "Rus";
            if (language.Contains("TR")) return "Tur";
            return "Eng";
        } catch {
            return "Eng";
        }
    }

    public string GetTextByLanguage(TranslationWordData wordData) {
        try {
            string language = GetLang();
            if ("Eng" == language) return wordData.Eng;
            if ("Rus" == language) return wordData.Rus;
            if ("Tur" == language) return wordData.Tur;
            return wordData.Eng;
        } catch {
            return string.Empty;
        }
    }

    private string GetJson() {
        const string fileName = "DICTIONARY_TRANSLATIONS";
        TextAsset asset = Resources.Load<TextAsset>(fileName);
        string jsonString = asset.text;
        return jsonString;
    }

    public void InitContainer() {
        string jsonString = GetJson();
        this._container = JsonUtility.FromJson<WordsContainer>(jsonString);
    }

    private WordsContainer _container = null;

    private void Awake() {
        InitContainer();
    }

    private void Start() {
        InitContainer();
        SetStartText();
        SetTextX2Gold();
        SetWatchVideoText();
        SetNotEnoughMoney();
        SetOkGoodBtn();
        SetChangeSkinText();
        SetUseThisSkinBtnText();
    }

    public TranslationWordData GetElementByIdentifier(string keyIdentifier) {
        InitContainer();
        foreach (TranslationWordData element in this._container.ContentArray) {
            if (keyIdentifier.Trim() == element.KeyIdentifier.Trim()) return element;
        }
        return null;
    }


    private void SetStartText() {
        try {
            if (_textStart) {
                var wordData = GetElementByIdentifier("KEY_START");
                _textStart.text = GetTextByLanguage(wordData);
            }
        } catch { }
    }

    private void SetTextX2Gold() {
        try {
            if (_getX2Gold) {
                var wordData = GetElementByIdentifier("KEY_X2_GOLD_GET");
                _getX2Gold.text = GetTextByLanguage(wordData);
            }
        } catch { }
    }

    private void SetWatchVideoText() {
        try {
            if (_wathVideo) {
                var wordData = GetElementByIdentifier("KEY_WATCH_VIDEO_TO_GOLD");
                _wathVideo.text = GetTextByLanguage(wordData);
            }
        } catch { }
    }

    private void SetNotEnoughMoney() {
        try {
            if (_notEnoughMoney) {
                var wordData = GetElementByIdentifier("KEY_NOT_ENOUGH_GOLD");
                _notEnoughMoney.text = GetTextByLanguage(wordData);
            }
        } catch { }
    }

    private void SetOkGoodBtn() {
        try {
            if (_okGood) {
                var wordData = GetElementByIdentifier("KEY_OK_GOOD");
                _okGood.text = GetTextByLanguage(wordData);
            }
        } catch { } 
    }

    private void SetChangeSkinText() {
        try {
            if (_changeSkin) {
                var wordData = GetElementByIdentifier("KEY_CHANGE_SKIN_LABEL");
                _changeSkin.text = GetTextByLanguage(wordData);
            }
        } catch { }
    }

    private void SetUseThisSkinBtnText() {
        try {
            if (_useThisSkinBtn) {
                var wordData = GetElementByIdentifier("KEY_USE_THIS_SKIN");
                _useThisSkinBtn.text = GetTextByLanguage(wordData);
            }
        } catch { }
    }

    [SerializeField] private Text _textStart = null;

    [SerializeField] private Text _getX2Gold = null;

    [SerializeField] private Text _wathVideo = null;

    [SerializeField] private Text _notEnoughMoney = null;

    [SerializeField] private Text _okGood = null;

    [SerializeField] private Text _changeSkin = null;

    [SerializeField] private Text _useThisSkinBtn = null;
}
