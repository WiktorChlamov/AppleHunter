using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections;
using static Game.Data;
#if UNITY_EDITOR
using UnityEditor;
#endif

using Debug = UnityEngine.Debug;

[ExecuteInEditMode]
public class LocalizationManager : MonoBehaviour
{
    private static LocalizationManager Instance;

    private Languages defaultLanguage = Languages.English;
    public static Languages currentLanguage;

    public static Languages newLanguage = Languages.English;

    public static readonly string DirectoryPath = "/TekkTech/Resources/LanguageFiles/";
    public static readonly string TagEnumPath = "/TekkTech/Scripts/Language/";

    private static LanguageFile m_loadedLanguageFile;

    [HideInInspector] public static List<string> fileKeys = new List<string>();

    private static List<LocalizedString> registeredLanguageStrings = new List<LocalizedString>();

    private void Awake()
    {
        if (!Instance) Instance = this;
    }

  /*  private void Start()
    {
        if (Application.isPlaying)
        {
            LoadLanguageFromFile(defaultLanguage);
        }
    }*/
    public void ChangeLanguageButton(int to)
    {
        Lang_id = to;
        SettingsMenu.lang = to;
        LoadLanguageFromFile((Languages)to);
        Game.SelectKnife.SetText();
        GetComponent<Game.Loading>().UpdateStats();
    }
    public static void ChangeLanguage(Languages to)
    {
        LoadLanguageFromFile(to);
    }
    public static string Translate(LocalizedString localized)
    {
        localized.SetAction(null);
        return localized;
    }
    public static Color32 Color32(KnifeRare knifeRare)
    {
        switch (knifeRare)
        {
            case KnifeRare.Normal:
                return new Color32(197, 246, 177, 255);
            case KnifeRare.Rare:
                return new Color32(61, 236, 255, 255);
            case KnifeRare.Unique:
                return new Color32(255, 199, 61, 255);
            default:
                return new Color32(255, 255, 255, 255);
        }
    }
    public static void RegisterLanguageString(LocalizedString ls)
    {
        if (!registeredLanguageStrings.Contains(ls))
        {
            registeredLanguageStrings.Add(ls);
            ls.LanguageSwitch();
        }
    }
    public static void UnregisterLanguageString(LocalizedString ls)
    {
        if (!registeredLanguageStrings.Contains(ls))
        {
            registeredLanguageStrings.Remove(ls);
        }
    }

    private static void LoadLanguageFromFile(Languages LanguageToLoad)
    {
        currentLanguage = LanguageToLoad;

        string filePath = Application.dataPath + DirectoryPath + currentLanguage.ToString() + ".lang";

        m_loadedLanguageFile = GetLanguageDataFromFile(filePath);

        for (int i = 0; i < registeredLanguageStrings.Count; i++)
        {
            registeredLanguageStrings[i].LanguageSwitch();
        }
    }

    public static LanguageFile GetLanguageDataFromFile(Languages LanguageToLoad)
    {
        string filePath = Application.dataPath + DirectoryPath + LanguageToLoad.ToString() + ".lang";
        return GetLanguageDataFromFile(filePath);
    }

    public static LanguageFile GetLanguageDataFromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("TekkTech: Language File " + filePath + " does not exist.");
            return null;
        }

        LanguageFile tempLanguageFile = JsonUtility.FromJson<LanguageFile>(File.ReadAllText(filePath));
        return tempLanguageFile;
    }

    public static bool LanguageFileExists(Languages languages)
    {
        string filePath = Application.dataPath + DirectoryPath + languages.ToString() + ".lang";
        return File.Exists(filePath);
    }

    public static string GetTextForTag(LanguageTags tag)
    {
        if (m_loadedLanguageFile is null)
            return "";

        string returnText = m_loadedLanguageFile.GetEntryText(tag.ToString());
        if (string.IsNullOrEmpty(returnText))
            return "[" + tag.ToString() + "]";
        return returnText;
    }


#if UNITY_EDITOR
    public void GenerateLanguageFile()
    {
        LanguageFile languagesFile = new LanguageFile();

        fileKeys.ForEach(key => { languagesFile.SetEntry(key); });

        string filePath = Application.dataPath + DirectoryPath + "LanguageXX.lang";
        File.WriteAllText(filePath, languagesFile.GetJsonFormat());

        AssetDatabase.Refresh();
    }
#endif

    public static void GenerateKeysFromEnum()
    {
        fileKeys.Clear();
        Enum.GetValues(typeof(LanguageTags)).Cast<LanguageTags>().ToList().ForEach(tag =>
        {
            fileKeys.Add(tag.ToString());
        });
    }

    public static void WriteNewKeysToLanguageFile(Languages language, List<string> keys, List<string> content = null)
    {
        string filePath = Application.dataPath + DirectoryPath + language.ToString() + ".lang";
        LanguageFile languagesFile = new LanguageFile();

        if (File.Exists(filePath))
        {
            languagesFile = JsonUtility.FromJson<LanguageFile>(File.ReadAllText(filePath));
        }

        for (int i = 0; i < keys.Count; i++)
        {
            if (content != null)
                languagesFile.SetEntry(keys[i], new LanguageFileEntry(keys[i], content[i]));
            else
                languagesFile.SetEntry(keys[i]);
        }

        File.WriteAllText(filePath, languagesFile.GetJsonFormat());
    }

#if UNITY_EDITOR
    public static void RemoveTagsFromLanguageFile(Languages language, List<string> tagsToRemove)
    {
        if (tagsToRemove.Count == 0) return;

        string filePath = Application.dataPath + DirectoryPath + language.ToString() + ".lang";
        LanguageFile languagesFile = new LanguageFile();

        if (File.Exists(filePath))
        {
            languagesFile = JsonUtility.FromJson<LanguageFile>(File.ReadAllText(filePath));
        }

        tagsToRemove.ForEach(tag => { languagesFile.RemoveEntry(tag); });

        WriteNewLanguageFile(language, languagesFile);
    }

    public static LanguageFile WriteNewLanguageFile(Languages language, LanguageFile file)
    {
        string filePath = Application.dataPath + DirectoryPath + language.ToString() + ".lang";
        file.fileLanguage = language;

        Directory.CreateDirectory(Application.dataPath + DirectoryPath);
        File.WriteAllText(filePath, file.GetJsonFormat());
        AssetDatabase.Refresh();
        return file;
    }
#endif

    public static void RemoveLanguageFile(Languages language)
    {
        string filePath = Application.dataPath + DirectoryPath + language.ToString() + ".lang";
        string metaPath = filePath + ".meta";

        File.Delete(filePath);
        File.Delete(metaPath);
    }
}

