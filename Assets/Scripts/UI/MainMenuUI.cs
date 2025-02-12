using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button storeButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Transform settingUI;

    private void Awake()
    {
        playButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.LevelsMenuScene);     
        });
        
        storeButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.StoreScene);     
        });

        settingButton.onClick.AddListener(() =>
        {
            settingUI.gameObject.SetActive(true);
            //Loader.Load(Loader.Scene.SettingScene);
        });

        quitButton.onClick.AddListener(() => {
             Application.Quit();
        });

        Time.timeScale = 1f;
    }
}
