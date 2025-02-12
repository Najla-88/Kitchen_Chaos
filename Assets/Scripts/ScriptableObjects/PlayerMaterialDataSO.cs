using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
public class PlayerMaterialDataSO : ScriptableObject
{
    public Material material;
    public int cost;
    public bool isSold;
    public bool isUsed;

    public event EventHandler OnUsed;
    public event EventHandler OnSold;

    public void UsePlayerMaterialDataSO()
    {
        isUsed = true;
        OnUsed?.Invoke(this, EventArgs.Empty);
    }
    public void UnUsePlayerMaterialDataSO()
    {
        isUsed = false;
    }
    public void SoldPlayerMaterialDataSO()
    {
        isSold = true;
        OnSold?.Invoke(this, EventArgs.Empty);
    }

}
