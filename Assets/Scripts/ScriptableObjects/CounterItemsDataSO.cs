using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
public class CounterItemsDataSO : ScriptableObject
{
    public string itemName;
    public CountersManager.CounterType counterType;
    public Sprite sprite;
    public int cost;
    public int lastIndex;
    public int maxCount;

    public void UpdateLastIndex()
    {
        lastIndex = PlayerPrefs.GetInt("Last" + counterType.ToString());
    }
}
