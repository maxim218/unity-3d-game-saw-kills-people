using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable] 
internal class ElementInLevel {
    public float x = 0;
    public float z = 0;
    public string type = string.Empty;
}

[Serializable]
internal class ContentLevel {
    public float xHero = 0;
    public float zHero = 0;
    public ElementInLevel[] arr = null;
}

public class LevelBuilder : MonoBehaviour {
    private const string WallType = "WALL_TYPE_ELEMENT";
    private const string RedPersonType = "RED_PERSON_TYPE_ELEMENT";

    [SerializeField] private GameObject PrefabWall = null;
    [SerializeField] private GameObject PrefabRedPerson = null;
    [SerializeField] private GameObject PrefabHakiWakiMonstr = null;

    public static LevelBuilder GetLevelBuilderObject() {
        LevelBuilder obj = FindObjectOfType<LevelBuilder>();
        return obj;
    }

    private static string GetStringFromAsset() {
        // get string from asset
        int levelNumber = LevelIndexManager.GetLevelIndex();
        string nameFile = "LEVEL_" + levelNumber;
        TextAsset asset = Resources.Load<TextAsset>(nameFile);
        string jsonString = asset.text;
        return jsonString;
    }

    [SerializeField] private Text _labelLevelText = null;

    private void SetLabelLevelText(int lv) {
        if (_labelLevelText) {
            _labelLevelText.text = "Lv " + lv;
        }
    }

    public void LoadLevelBlock() {
        // json
        string jsonString = string.Empty;

        try {
            jsonString = GetStringFromAsset();
        } catch {
            LevelIndexManager.ResetIndexToZero();
            jsonString = GetStringFromAsset();
        }

        string infoAboutLevelMsg = "-------- " + "Level index: " + LevelIndexManager.GetLevelIndex() + " --------";
        Debug.Log(infoAboutLevelMsg);
        SetLabelLevelText( 1 + LevelIndexManager.GetLevelIndex() );

        // make obj from json
        ContentLevel contentLevel = JsonUtility.FromJson<ContentLevel>(jsonString);

        // hero position set
        RobotControl robotControl = FindObjectOfType<RobotControl>();
        robotControl.SetRobotPosition(contentLevel.xHero, contentLevel.zHero);

        // build walls
        for(int i = 0; i < contentLevel.arr.Length; i++) {
            ElementInLevel element = contentLevel.arr[i];
            if(element.type.Trim() == WallType) {
                CreateWall(element.x, element.z);
            }
        }

        // build red persons
        for (int i = 0; i < contentLevel.arr.Length; i++) {
            ElementInLevel element = contentLevel.arr[i];
            if(element.type.Trim() == RedPersonType) {
                CreateRedPerson(element.x, element.z, i);
            }
        }

        // get all persons
        ManControl [] personsArray = FindObjectsOfType(typeof(ManControl)) as ManControl[];

        // rotate red persons
        foreach(ManControl manControl in personsArray) {
            if(manControl) {
                float angle = UnityEngine.Random.Range(-300f, 300f);
                manControl.transform.Rotate(0, angle, 0);
            }
        }
    }

    private void CreateWall(float x, float z) {
        GameObject wall = Instantiate(PrefabWall) as GameObject;
        wall.transform.position = new Vector3(x, 2.78f, z);
        // parent set
        GameObject wallsGroupObj = GameObject.Find("WallsGroup");
        wall.transform.SetParent(wallsGroupObj.transform);
    }

    private void CreateRedPerson(float x, float z, int index) {
        bool condition = (index % 2 == 0);
        GameObject redPerson = Instantiate(condition ? PrefabRedPerson : PrefabHakiWakiMonstr) as GameObject;
        redPerson.transform.position = new Vector3(x, 2f, z);
        // parent set
        GameObject peopleGroupObj = GameObject.Find("PeopleGroup");
        redPerson.transform.SetParent(peopleGroupObj.transform);
    }

    public int GetEnemiesNumber() {
        ManControl[] personsArray = FindObjectsOfType(typeof(ManControl)) as ManControl[];
        int length = personsArray.Length;
        return length;
    }
}
