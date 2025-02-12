using TMPro;
using UnityEngine;

public class PlayerCoinsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;


    private void Start()
    {
        CoinsManager.Instance.OnPlayerCoinsAmountChange += CoinsManager_OnPlayerCoinsAmountChange;

        SetCoins(CoinsManager.Instance.GetPlayerCoins());
    }

    private void CoinsManager_OnPlayerCoinsAmountChange(object sender, System.EventArgs e)
    {
        SetCoins(CoinsManager.Instance.GetPlayerCoins());
    }


    private void SetCoins(int coinsAmount)
    {
        coinsText.text = coinsAmount.ToString() + "  <sprite=0>";
    }
}
