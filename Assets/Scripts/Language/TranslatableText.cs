using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TranslatableText : MonoBehaviour
{
    public string textKey;
    private TextMeshProUGUI textComponent;

    void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        if (LanguageManager.Instance == null || textComponent == null) return;

        string text = LanguageManager.Instance.GetText(textKey);

        if (LanguageManager.Instance.currentLanguage == "ar")
        {
            text = ArabicSupport.ArabicFixer.Fix(text, false, false);
        }

        textComponent.text = text;
    }
}