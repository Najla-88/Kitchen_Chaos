using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsMenuUI : MonoBehaviour
{

    [SerializeField] private Transform levelsContainer;
    [SerializeField] private Transform levelTemplate;
    [SerializeField] private LevelInfoListSO levelInfoListSO;

    [SerializeField] private Button backButton;
    [SerializeField] private Button gameSceneButton;

    private void Start()
    {
        backButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        MenuLevelsManager.OnLevelsInfoUpdated += MenuLevelsManager_OnLevelsInfoUpdated;
        UpdateVisual();
    }

    private void MenuLevelsManager_OnLevelsInfoUpdated(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {

        levelTemplate.gameObject.SetActive(false);

        foreach (Transform child in levelsContainer)
        {
            if (child == levelTemplate) continue;
            Destroy(child.gameObject);
        }

        for (int i = 0; i < levelInfoListSO.levelInfoSOArray.Length; i++)
        {
            Transform levelTransform = Instantiate(levelTemplate, levelsContainer);

            levelTransform.gameObject.SetActive(true);
            if(levelInfoListSO.levelInfoSOArray[i].levelUnlocked == false)
            {
                levelTransform.GetComponent<Button>().enabled = false;
            }

            levelTransform.GetComponent<LevelMenuSingleUI>().SetItemData(levelInfoListSO.levelInfoSOArray[i]);
        }
    }
}
