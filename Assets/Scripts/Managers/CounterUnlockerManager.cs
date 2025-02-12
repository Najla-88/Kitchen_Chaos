using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterUnlockerManager : MonoBehaviour
{
    private const string LAST = "Last";

    //[SerializeField] private CountersManager.CounterUnlockType counterType;
    //[SerializeField] private int index;

    ////public CountersManager counterManager;
    //void Awake()
    //{
    //    //if (PlayerPrefs.GetInt(counterType.ToString() + index.ToString()) == 1)
    //    //{
    //    //    counterManager.UnlockCounterInScene(counterType, index);
    //    //}

    //    UnlockCounterInPrefs(counterType, index);
    //}

    public void UnlockCounterInPrefs(CountersManager.CounterType counterType,int index)
    {
        PlayerPrefs.SetInt(LAST + counterType.ToString(), index);
        PlayerPrefs.Save();
        //PlayerPrefs.SetInt(counterType + index.ToString(), 1);
        //counterManager.UnlockCounterInScene(counterType, index);
    }
    public void LockCounterInPrefs(CountersManager.CounterType counterType,int index)
    {
        PlayerPrefs.SetInt(LAST + counterType.ToString(), index--);
        PlayerPrefs.Save();

        //PlayerPrefs.SetInt(counterType + index.ToString(), 0);
    }


    // use this in the store
    public int GetLastIndexOfCounterType(CountersManager.CounterType counterType)
    {
        return PlayerPrefs.GetInt(LAST + counterType.ToString());

    }
}