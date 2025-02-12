using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public static SettingsUI Instance { get; private set; }

    //[SerializeField] private TextMeshProUGUI soundEffictsText;
    [SerializeField] private Slider soundEffictsSlider;
    //[SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private Slider musicSlider; 
    [SerializeField] private Button closeButton;


    [SerializeField] private TMP_Dropdown languageDropdown;
    [SerializeField] private LocalSelector localSelector;

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
    }
    private void Start()
    {
        if(Loader.GetCurrentScene().ToString() != Loader.Scene.MainMenuScene.ToString())
        {
            KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;
            UpdateVisual();
        
        }
        Hide();
        LanguageDropdown();
    }

    private void LanguageDropdown()
    {

        languageDropdown.onValueChanged.AddListener((int value) => {
            localSelector.ChangeLocale(value);
        });

        // Clear existing options
        languageDropdown.ClearOptions();

        // Get available locales
        var locales = localSelector.GetAvailableLocales();

        // Create a list of locale names
        List<string> localeNames = new List<string>();
        foreach (var locale in locales)
        {
            localeNames.Add(locale.name);
        }

        // Add locale names to the dropdown options
        languageDropdown.AddOptions(localeNames);

        // Set the current locale in the dropdown
        languageDropdown.value = localSelector.GetCurrentLocaleIndex();
    }
    private void KitchenGameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        //soundEffictsText.text = "Sound Effict: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f).ToString();
        soundEffictsSlider.value = SoundManager.Instance.GetVolume();

        //musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f).ToString();
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
