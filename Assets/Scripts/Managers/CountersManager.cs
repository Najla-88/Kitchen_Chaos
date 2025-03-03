using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountersManager : MonoBehaviour
{
    public enum CounterType
    {
        Clear,
        Stove,
        Cutting,
        MeatContainer,
        CabbageContainer,
        BreadContainer,
        TomatoContainer,
        CheeseBlockContainer,
    }

    [SerializeField] private GameObject[] clearCounters;
    [SerializeField] private GameObject[] stoveCounters;
    [SerializeField] private GameObject[] cuttingCounters;
    [SerializeField] private GameObject[] meatContainerCounters;
    [SerializeField] private GameObject[] breadContainerCounters;
    [SerializeField] private GameObject[] cabbageContainerCounters;
    [SerializeField] private GameObject[] tomatoContainerCounters;
    [SerializeField] private GameObject[] cheeseBlockContainerCounters;

    //[SerializeField] private CounterUnlockerManager counterUnlockerManager;


    private int clearCounterLastIndex;
    private int stoveCounterLastIndex;
    private int cuttingCounterLastIndex;
    private int meatContainerCounterLastIndex;
    private int breadContainerCounterLastIndex;
    private int cabbageContainerCounterLastIndex;
    private int tomatoContainerCounterLastIndex;
    private int cheeseBlockContainerCounterLastIndex;

    private void Start()
    {
        clearCounterLastIndex = SaveManager.Instance.GetLastIndexOfCounterType(CounterType.Clear);
        stoveCounterLastIndex = SaveManager.Instance.GetLastIndexOfCounterType(CounterType.Stove);
        cuttingCounterLastIndex = SaveManager.Instance.GetLastIndexOfCounterType(CounterType.Cutting);
        meatContainerCounterLastIndex = SaveManager.Instance.GetLastIndexOfCounterType(CounterType.MeatContainer);
        breadContainerCounterLastIndex = SaveManager.Instance.GetLastIndexOfCounterType(CounterType.BreadContainer);
        cabbageContainerCounterLastIndex = SaveManager.Instance.GetLastIndexOfCounterType(CounterType.CabbageContainer);
        tomatoContainerCounterLastIndex = SaveManager.Instance.GetLastIndexOfCounterType(CounterType.TomatoContainer);
        cheeseBlockContainerCounterLastIndex = SaveManager.Instance.GetLastIndexOfCounterType(CounterType.CheeseBlockContainer);

        for(int i = 0; i < clearCounterLastIndex; i++)
        {
            //if (PlayerPrefs.GetInt(CounterUnlockType.Clear.ToString() + i.ToString()) == 1)
            //{
                UnlockCounterInScene(CounterType.Clear, i);
            //}
        }
        for(int i = 0; i < stoveCounterLastIndex; i++)
        {
            //if (PlayerPrefs.GetInt(CounterUnlockType.Stove.ToString() + i.ToString()) == 1)
            //{
                UnlockCounterInScene(CounterType.Stove, i);
            //}
        }
        for(int i = 0; i < cuttingCounterLastIndex; i++)
        {
            //if (PlayerPrefs.GetInt(CounterUnlockType.Cutting.ToString() + i.ToString()) == 1)
            //{
                UnlockCounterInScene(CounterType.Cutting, i);
            //}
        }
        for(int i = 0; i < meatContainerCounterLastIndex; i++)
        {
            //if (PlayerPrefs.GetInt(CounterUnlockType.Container.ToString() + i.ToString()) == 1)
            //{
                UnlockCounterInScene(CounterType.MeatContainer, i);
            //}
        }
        for(int i = 0; i < breadContainerCounterLastIndex; i++)
        {
            //if (PlayerPrefs.GetInt(CounterUnlockType.Container.ToString() + i.ToString()) == 1)
            //{
                UnlockCounterInScene(CounterType.BreadContainer, i);
            //}
        }
        for(int i = 0; i < cabbageContainerCounterLastIndex; i++)
        {
            //if (PlayerPrefs.GetInt(CounterUnlockType.Container.ToString() + i.ToString()) == 1)
            //{
                UnlockCounterInScene(CounterType.CabbageContainer, i);
            //}
        }
        for(int i = 0; i < tomatoContainerCounterLastIndex; i++)
        {
            //if (PlayerPrefs.GetInt(CounterUnlockType.Container.ToString() + i.ToString()) == 1)
            //{
                UnlockCounterInScene(CounterType.TomatoContainer, i);
            //}
        }
        for(int i = 0; i < cheeseBlockContainerCounterLastIndex; i++)
        {
            //if (PlayerPrefs.GetInt(CounterUnlockType.Container.ToString() + i.ToString()) == 1)
            //{
                UnlockCounterInScene(CounterType.CheeseBlockContainer, i);
            //}
        }
    }

    public void UnlockCounterInScene(CounterType counterType, int index)
    {
        switch (counterType)
        {
            case CounterType.Clear:
                clearCounters[index].transform.GetChild(0).gameObject.SetActive(false);
                break;
            case CounterType.Stove:
                stoveCounters[index].transform.GetChild(0).gameObject.SetActive(false);
                break;
            case CounterType.Cutting:
                cuttingCounters[index].transform.GetChild(0).gameObject.SetActive(false);
                break;
            case CounterType.MeatContainer:
                meatContainerCounters[index].transform.GetChild(0).gameObject.SetActive(false);
                break;
            case CounterType.BreadContainer:
                breadContainerCounters[index].transform.GetChild(0).gameObject.SetActive(false);
                break;
            case CounterType.CabbageContainer:
                cabbageContainerCounters[index].transform.GetChild(0).gameObject.SetActive(false);
                break;
            case CounterType.TomatoContainer:
                tomatoContainerCounters[index].transform.GetChild(0).gameObject.SetActive(false);
                break;
            case CounterType.CheeseBlockContainer:
                cheeseBlockContainerCounters[index].transform.GetChild(0).gameObject.SetActive(false);
                break;
        }
    }
}