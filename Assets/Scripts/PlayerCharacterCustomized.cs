using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterCustomized : MonoBehaviour
{
    [SerializeField] MaterialData materialData;
    
    [Serializable]
    public class MaterialData
    {
        public MeshRenderer[] meshRendererArray;
        public PlayerMaterialDataListSO playerMaterialDataListSO;
    }

    public void ChangeMaterial(int newIndex)
    {
        foreach (MeshRenderer meshRenderer in materialData.meshRendererArray)
        {
            meshRenderer.material = materialData.playerMaterialDataListSO.playerMaterialDataSOArray[newIndex].material;
        }
    }
}