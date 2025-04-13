using UnityEngine;

public static class SaveSystem
{
    private const string STORE_PREFS = "StorePrefs";

    public static void Save(SaveData saveData)
    {
        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(STORE_PREFS, json);
        PlayerPrefs.Save();
    }

    public static SaveData Load()
    {
        if (PlayerPrefs.HasKey(STORE_PREFS))
        {
            string json = PlayerPrefs.GetString(STORE_PREFS);
            return JsonUtility.FromJson<SaveData>(json);
        }
        return null;
    }

    public static void ClearSaveData()
    {
        PlayerPrefs.DeleteKey(STORE_PREFS);
    }
}