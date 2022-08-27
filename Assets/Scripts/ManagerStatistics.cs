using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class ManagerStatistics : MonoBehaviour {
    private static string GetDomainAndPort() {
        return "http://2.59.156.129:5015";
    }

    [ContextMenu("Clear Local Storage Method")]
    public void ClearLocalStorageMethod() {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        const string message = "Storage deleted OK";
        Debug.Log(message);
    }

    private static string GenerateUniqueId() {
        string s = DateTime.Now.ToString();
        s = s.Replace(" ", "_");
        s = s.Replace(".", "_");
        s = s.Replace(",", "_");
        s = s.Replace(":", "_");
        s = s.Replace("/", "_");
        s = s + "_" + UnityEngine.Random.Range(2000, 8000);
        s = s + "_" + UnityEngine.Random.Range(2000, 8000);
        s = s + "_" + UnityEngine.Random.Range(2000, 8000);
        s = s + "_" + UnityEngine.Random.Range(2000, 8000);
        s = s + "_" + UnityEngine.Random.Range(2000, 8000);
        s = s + "_" + UnityEngine.Random.Range(2000, 8000);
        s = s + "_" + UnityEngine.Random.Range(2000, 8000);
        s = s + "_" + UnityEngine.Random.Range(2000, 8000);
        s = s.Replace(" ", "_");
        return "user_" + s;
    }

    private void Start() {
        // id control
        string id = PlayerPrefs.GetString("USER_UNIQUE_ID", string.Empty);
        if(string.IsNullOrEmpty(id)) {
            string newIdUnique = GenerateUniqueId();
            PlayerPrefs.SetString("USER_UNIQUE_ID", newIdUnique);
            PlayerPrefs.Save();
            Debug.Log("Created new id - " + newIdUnique);
        } else {
            Debug.Log("Id already exists - " + id);
        }

        // get current id
        string currentId = PlayerPrefs.GetString("USER_UNIQUE_ID", string.Empty);
        // query
        RunInternetQuery(currentId);
    }

    private string _url = string.Empty;

    private void RunInternetQuery(string currentId) {
        string sceneName = SceneManager.GetActiveScene().name;
        int levelNumber = LevelIndexManager.GetLevelIndex();
        string message = sceneName + "___" + levelNumber;
        // set url
        _url = GetDomainAndPort() + "/game/api/save/story?userIdParam=" + currentId + "&storyMessageParam=" + message;
        Debug.Log(_url);
        // send query
        StartCoroutine(QueryGetIEnumerator());
    }

    private IEnumerator QueryGetIEnumerator() {
        WWW www = new WWW(_url);
        yield return www;
        Debug.Log("Answer from server: " + www.text);
    }
}
