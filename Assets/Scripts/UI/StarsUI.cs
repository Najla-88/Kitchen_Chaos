using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsUI : MonoBehaviour
{
    [SerializeField] private Image[] starsArray;

    [SerializeField] private Sprite starSprite;
    [SerializeField] private Sprite starOutlierSprite;

    private void Start()
    {
        StarsManager.Instance.OnGameStarsNumberChange += StarsManager_OnGameStarsNumberChange;
        UpdateVisual();
    }

    private void StarsManager_OnGameStarsNumberChange(object sender, System.EventArgs e)
    {
       UpdateVisual();
    }

    private void UpdateVisual()
    {
        int starsNumber = StarsManager.Instance.GetGameStars();
        //int outlierStarsNumber = System.Math.Max(0, 3 - starsNumber);

        for (int i = 0; i < 3; i++)
        {
            if (i < starsNumber)
            {
                starsArray[i].sprite = starSprite; // Set star sprite for collected stars
            }
            else
            {
                starsArray[i].sprite = starOutlierSprite; // Set outlier star sprite for uncollected stars
            }
        }
    }
}
