using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterCustomized : MonoBehaviour
{
    [SerializeField] private PlayerMaterialDataListSO playerMaterialDataList;
    [SerializeField] private MaterialData materialData;
    

    [Serializable]
    public class MaterialData
    {
        public MeshRenderer[] meshRendererArray;
        public PlayerMaterialDataListSO playerMaterialDataListSO;
    }

    private void Start()
    {
        for(int i = 0; i < playerMaterialDataList.playerMaterialDataSOArray.Length; i++)
        {
            if(playerMaterialDataList.playerMaterialDataSOArray[i].isUsed)
            {
                ChangeMaterial(i);
                break;
            }
        }
    }

    public void ChangeMaterial(int newIndex)
    {
        foreach (MeshRenderer meshRenderer in materialData.meshRendererArray)
        {
            meshRenderer.material = materialData.playerMaterialDataListSO.playerMaterialDataSOArray[newIndex].material;
        }
    }
}