using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelStarsUI : MonoBehaviour
{
    [SerializeField] private Image[] stars;
    [SerializeField] private Sprite starSprite;
    [SerializeField] private Sprite starOutlierSprite;

    public void DisplayStars(int numberOfStars)
    {
        if(numberOfStars>=0)
        {
            for(int i = 0; i < stars.Length; i++)
            {
                stars[i].gameObject.SetActive(true);
                stars[i].sprite = numberOfStars >= i+1 ? starSprite : starOutlierSprite;
            }
            //Debug.Log(numberOfStars);
            //star1.sprite = numberOfStars >= 1 ? starSprite : starOutlierSprite;
            //star2.sprite = numberOfStars >= 2 ? starSprite : starOutlierSprite;
            //star3.sprite = numberOfStars == 3 ? starSprite : starOutlierSprite;
        }
        else
        {
            for(int i = 0;i < stars.Length; i++)
            {
                stars[i].gameObject.SetActive(false);
            }
        }
    }

}
