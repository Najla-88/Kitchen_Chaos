using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private TextMeshProUGUI coinsAmount;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    [SerializeField] private Image recipeTimeIcon;
    [SerializeField] private Sprite sadRecipeTimeIcon;
    [SerializeField] private Sprite angryRecipeTimeIcon;


    private int sadRecipeTime;
    private int angryRecipeTime;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    private void Start()
    {
        sadRecipeTime = 7;
        angryRecipeTime = 4;
    }

    public void SetRecipeSO(WaitingRecipe waitingRecipe)
    {

        recipeNameText.text = waitingRecipe.recipeSO.recipeName;
        coinsAmount.text = waitingRecipe.recipeSO.CalculateTotalCost().ToString();
        if (waitingRecipe.timer <= sadRecipeTime)
        {
            recipeTimeIcon.sprite = sadRecipeTimeIcon;
        }
        if (waitingRecipe.timer <= angryRecipeTime)
        {
            recipeTimeIcon.sprite = angryRecipeTimeIcon;
        }
        foreach (Transform child in iconContainer)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in waitingRecipe.recipeSO.kitchenObjectSOList)
        {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
    }
}