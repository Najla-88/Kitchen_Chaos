using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public static SettingsUI Instance { get; private set; }

    [SerializeField] private Slider soundEffictsSlider;
    [SerializeField] private Slider musicSlider; 
    [SerializeField] private Button closeButton;
    [SerializeField] private TMP_Dropdown languageDropdown;

    private Action onCloseButtonAction;

    private void Awake()
    {
        Instance = this;
        soundEffictsSlider.onValueChanged.AddListener((v) => {
            SoundManager.Instance.ChangeVolume(v);
            UpdateVisual();
        });

        musicSlider.onValueChanged.AddListener((v) => {
            MusicManager.Instance.ChangeVolume(v);
            UpdateVisual();
        });

        closeButton.onClick.AddListener(() => {
            Hide();

            if (Loader.GetCurrentScene().ToString() != Loader.Scene.MainMenuScene.ToString())
            {
                onCloseButtonAction();
            }
        });

        languageDropdown.value = LanguageManager.Instance.GetLanguageIndexForDropdown();
    }
    private void Start()
    {
        if(Loader.GetCurrentScene().ToString() != Loader.Scene.MainMenuScene.ToString())
        {
            KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;
            UpdateVisual();
        }
        Hide();
        languageDropdown.onValueChanged.AddListener((int value) => {
            if (value == 0)
                LanguageManager.Instance.LoadLanguage("en");
            else
                LanguageManager.Instance.LoadLanguage("ar");
        });
    }
    private void KitchenGameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffictsSlider.value = SoundManager.Instance.GetVolume();
        musicSlider.value = MusicManager.Instance.GetVolume();
    }

    public void Show(Action onCloseButtonAction)
    {
        this.onCloseButtonAction = onCloseButtonAction;
        gameObject.SetActive(true);

        soundEffictsSlider.Select();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
