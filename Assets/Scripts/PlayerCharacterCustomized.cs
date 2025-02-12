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


    //public void Load()
    //{
    //    int currentIndex = PlayerPrefs.GetInt(PLAYER_MATERIAL_INDEX);

        
    //    SetSavedMaterialIndex(currentIndex);

    //    if (currentIndex != -1)
    //    {
    //        foreach (MeshRenderer meshRenderer in materialData.meshRendererArray)
    //        {
    //            meshRenderer.material = materialData.playerMaterialDataListSO.playerMaterialDataSOArray[currentIndex].material;
    //        }
    //    }
    //}
}