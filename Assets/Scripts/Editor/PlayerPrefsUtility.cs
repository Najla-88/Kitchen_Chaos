using UnityEngine;
using UnityEditor;

public class PlayerPrefsUtility
{
    [MenuItem("Tools/Delete All PlayerPrefs")]
    private static void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("All PlayerPrefs have been deleted.");
    }
}