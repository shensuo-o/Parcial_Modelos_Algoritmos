using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateButtons : MonoBehaviour
{
    [SerializeField] private ButtonTranslate[] _textsToTranslate = new ButtonTranslate[0];
    [SerializeField] private Languages _language = default;

    public void TransalteBTN()
    {
        Debug.Log($"Accedi a {_language}");
        LocalizationManager.instance.language = _language;
        foreach (var item in _textsToTranslate)
        {
            item.textUI.text = LocalizationManager.instance.GetTranslate(item.ID);
        }
    }
}
