using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelInfoSO levelInfoSO;

    public static LevelManager Instance { private set; get; }

    private void Awake()
    {
        Instance = this;
    }


}
