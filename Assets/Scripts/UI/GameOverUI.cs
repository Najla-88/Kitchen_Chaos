using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI recipesDeleverdText;
    [SerializeField] private TextMeshProUGUI recipesLostText;
    [SerializeField] private TextMeshProUGUI collectedCoinsText;
    [SerializeField] private Button levelMenuButton;
    [SerializeField] private Button replayButton;
    [SerializeField] private Button nextLevelButton;


    private void Start()
    {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        Hide();

        levelMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.LevelsMenuScene);
        });
        replayButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.GetCurrentSceneName());
        });
        nextLevelButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.GetSecondLevelScene());
        });
    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.IsGameOver())
        {
            levelText.text = LevelNameMapping.GetCurrentLevelName();
            recipesDeleverdText.text = DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString();
            recipesLostText.text = DeliveryManager.Instance.GetLostRecipesAmount().ToString();
            collectedCoinsText.text = CoinsManager.Instance.GetCoinsInLevel().ToString();

            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }

}
