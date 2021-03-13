using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UITextSetter : MonoBehaviour
{
    public LocalizedString LocalizedText;

    private TextMeshProUGUI uiText;

    private void Start()
    {
        LocalizedText.SetAction(SetText);
        uiText = this.GetComponent<TextMeshProUGUI>();
        SetText();
    }

    public void SetText()
    {
        if(uiText is null)
            uiText = this.GetComponent<TextMeshProUGUI>();

        uiText.text = LocalizedText;
    }
}
