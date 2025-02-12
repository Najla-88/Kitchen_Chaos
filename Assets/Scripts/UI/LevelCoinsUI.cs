using TMPro;
using UnityEngine;

public class LevelCoinsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;


    private void Start()
    {
        CoinsManager.Instance.OnGameCoinsAmountChange += CoinsManager_OnLevelCoinsAmountChange;
        SetCoins(CoinsManager.Instance.GetCoinsInLevel());
    }

    private void CoinsManager_OnLevelCoinsAmountChange(object sender, System.EventArgs e)
    {
        SetCoins(CoinsManager.Instance.GetCoinsInLevel());
    }


    private void SetCoins(int coinsAmount)
    {
        coinsText.text = coinsAmount.ToString() + "  <sprite=0>";
    }
}
