using System;
using UnityEngine;
using UnityEditor;

namespace EditorNamespace {
    public class StorageEditor : EditorWindow {
        private string _inputValue = string.Empty;

        [MenuItem("Window/Local Storage Manager")]
        public static void ShowWindow() {
            const string title = "Local Storage Manager";
            GetWindow<StorageEditor>(title);
        }

        private void ClearStorage() {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debug.Log("Clear storage - OK");
        }

        private void SetTotalMoney() {
            int totalMoney = int.Parse(_inputValue);
            PlayerPrefs.SetInt("TOTAL_MONEY_KEY", totalMoney);
            PlayerPrefs.Save();
            Debug.Log("Total money: " + totalMoney);
        }

        private void RenderTotalMoney() {
            int totalMoney = PlayerPrefs.GetInt("TOTAL_MONEY_KEY", 0);
            Debug.Log("Total money: " + totalMoney);
        }

        private void OnGUI() {
            GUILayout.Space(20);

            if (GUILayout.Button("Clear storage")) ClearStorage();

            GUILayout.Space(20);

            _inputValue = EditorGUILayout.TextField("Total money int", _inputValue);

            GUILayout.Space(5);

            if (GUILayout.Button("Set total money")) SetTotalMoney();

            GUILayout.Space(20);

            if (GUILayout.Button("Render total money value")) RenderTotalMoney();

            GUILayout.Space(20);
        }
    }
}
