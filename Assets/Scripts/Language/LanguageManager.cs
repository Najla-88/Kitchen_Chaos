using UnityEngine;
using System.Collections.Generic;
using System.IO;
using TMPro;
using System.Collections;
#if UNITY_ANDROID
using UnityEngine.Networking;
#endif

public class LanguageManager : MonoBehaviour
{
    private const string CURRENT_LANGUAGE_PREFS = "CurrentLanguagePrefs";
    public static LanguageManager Instance;

    [System.Serializable]
    private class LanguageData
    {
        public List<LanguageItem> items;
    }

    [System.Serializable]
    private class LanguageItem
    {
        public string key;
        public string value;
    }

    private Dictionary<string, string> currentLanguageTexts = new Dictionary<string, string>();
    public string currentLanguage;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            currentLanguage = GetLanguagePrefs();
            LoadLanguage(currentLanguage);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadLanguage(string langCode)
    {
        currentLanguage = langCode;
        string filePath = Path.Combine(Application.streamingAssetsPath, $"language_{langCode}.json");
        SetLanguagePrefs(langCode);


#if UNITY_EDITOR || UNITY_STANDALONE
        LoadLanguageDesktop(filePath);
#elif UNITY_ANDROID || UNITY_IOS
        StartCoroutine(LoadLanguageMobile(filePath));
#endif
    }

    private void LoadLanguageDesktop(string filePath)
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            ProcessLanguageData(jsonData);
        }
        else
        {
            Debug.LogError($"File not found: {filePath}");
        }
    }

#if UNITY_ANDROID || UNITY_IOS
    private IEnumerator LoadLanguageMobile(string filePath)
    {
        UnityWebRequest www = UnityWebRequest.Get(filePath);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            ProcessLanguageData(www.downloadHandler.text);
        }
        else
        {
            Debug.LogError($"Failed to load: {www.error}");
        }
    }
#endif

    private void ProcessLanguageData(string jsonData)
    {
        var wrapper = JsonUtility.FromJson<LanguageData>(jsonData);
        currentLanguageTexts.Clear();

        foreach (var item in wrapper.items)
        {
            currentLanguageTexts[item.key] = item.value;
        }

        UpdateAllTexts();
    }

    public string GetText(string key)
    {
        if (currentLanguageTexts.TryGetValue(key, out string value))
        {
            return value;
        }
        return $"MISSING: {key}";
    }

    private void UpdateAllTexts()
    {
        TranslatableText[] allTexts = FindObjectsOfType<TranslatableText>(true);
        foreach (var text in allTexts)
        {
            text.UpdateText();
        }
    }

    private void SetLanguagePrefs(string currentLanguage)
    {
        PlayerPrefs.SetString(CURRENT_LANGUAGE_PREFS, currentLanguage);
        PlayerPrefs.Save();
    }

    private string GetLanguagePrefs()
    {
        string currentLanguage = PlayerPrefs.GetString(CURRENT_LANGUAGE_PREFS);
        
        if(currentLanguage == null)
            return "en";
        return currentLanguage;
    }

    public int GetLanguageIndexForDropdown()
    {
        if(GetLanguagePrefs()=="en")
            return 0;
        else 
            return 1;
    }
}