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

    public event EventHandler OnIncrease;
    public event EventHandler OnDecrease;

    public void IncreaseLastIndex()
    {
        lastIndex +=1 ;
        OnIncrease?.Invoke(this, EventArgs.Empty);
    }
    public void DecreaseLastIndex()
    {
        if (lastIndex >0)
        {
            lastIndex -=1 ;
        }
        OnDecrease?.Invoke(this, EventArgs.Empty);
    }


}
