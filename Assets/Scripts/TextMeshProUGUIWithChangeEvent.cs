using System;
using TMPro;
using UnityEngine;

public class TextMeshProUGUIWithChangeEvent : TextMeshProUGUI
{
    public event Action<string> TextValueChanged;

    private string previousText;

    public new string text
    {
        get { return base.text; }
        set
        {
            if (value != previousText)
            {
                previousText = value;
                base.text = value;
                OnTextValueChanged(value);
            }
        }
    }

    protected virtual void OnTextValueChanged(string newText)
    {
        TextValueChanged?.Invoke(newText);
    }
}