using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepUI : MonoBehaviour
{
    [SerializeField] private Button okButton;

    private void Awake()
    {
        okButton.onClick.AddListener(() => { 
            gameObject.SetActive(false);
            KitchenGameManager.Instance.TogglePauseGame();
        });
    }
}
