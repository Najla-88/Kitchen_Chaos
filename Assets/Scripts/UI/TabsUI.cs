using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabsUI : MonoBehaviour
{
    [SerializeField] private Image[] tabImges;
    [SerializeField] private GameObject[] pages;

    private void Start()
    {
        ActivateTab(0);
    }

    public void ActivateTab(int index)
    {
        for(int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
            //tabImges[i].color = Color.grey;
            tabImges[i].GetComponentInChildren<TextMeshProUGUI>().color = new Color32(100, 100, 100, 255);

        }

        pages[index].SetActive(true);
        //tabImges[index].color = Color.black;
        tabImges[index].GetComponentInChildren<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);

    }
}
