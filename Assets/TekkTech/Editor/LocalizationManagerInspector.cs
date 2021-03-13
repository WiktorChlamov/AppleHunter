using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;

[CustomEditor(typeof(LocalizationManager))]
public class
    LocalizationManagerInspector : Editor
{
    private void OnEnable()
    {
        LocalizationManager.GenerateKeysFromEnum();
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        DrawDefaultInspector();

        string path = LocalizationManager.DirectoryPath;

        if (EditorGUI.EndChangeCheck())
        {
            LocalizationManager.ChangeLanguage(LocalizationManager.currentLanguage);
        }

        if (path != "")
        {
            if (!Directory.Exists(Application.dataPath + path))
            {
                EditorGUILayout.LabelField("TekkTech: Pfad existiert nicht.");
            }
        }

        LocalizationManager.newLanguage = (Languages)EditorGUILayout.EnumPopup(LocalizationManager.newLanguage, GUILayout.Width(EditorGUIUtility.currentViewWidth / 2f));

        GUI.enabled = LocalizationManager.currentLanguage != LocalizationManager.newLanguage && LocalizationManager.LanguageFileExists(LocalizationManager.newLanguage);

        string buttonText = "Switch Language";
        if (!LocalizationManager.LanguageFileExists(LocalizationManager.newLanguage))
            buttonText = "Language File doesn't exist. Just load it.";

        if (GUILayout.Button(buttonText))
        {
            LocalizationManager.ChangeLanguage(LocalizationManager.newLanguage);
        }
        GUI.enabled = true;
        GUILayout.Space(30);
    }
}