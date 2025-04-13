using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenuSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelNumber;
    [SerializeField] private Image lockImage;
    [SerializeField] private GameObject levelStarsUI;

    private LevelInfoSO levelInfoSO;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => {
            if (levelInfoSO != null)
            {
                Loader.Load(levelInfoSO.scene);
            }
        });
    }
    public void SetItemData(LevelInfoSO levelInfoSO)
    {
        this.levelInfoSO = levelInfoSO;
        levelNumber.text = levelInfoSO.levelNumber.ToString();
        if(!levelInfoSO.isUnlocked)
        {
            lockImage.gameObject.SetActive(true);
        }
        else
        {
            levelStarsUI.GetComponent<LevelStarsUI>().DisplayStars(levelInfoSO.starsCount);
            lockImage.gameObject.SetActive(false);
        }
    }
}
