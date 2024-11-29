using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateScenes : MonoBehaviour
{
    [SerializeField] private ButtonTranslate[] _textsToTranslate = new ButtonTranslate[0];
    [SerializeField] private Languages _language = default;

    private void Start()
    {
        if (LocalizationManager.instance._currentLanguage == 0)
        {
            _language = Languages.Spanish;
        }

        if (LocalizationManager.instance._currentLanguage == 1)
        {
            _language = Languages.English;
        }

        Debug.Log($"Accedi a {_language}");

        foreach (var item in _textsToTranslate)
        {
            item.textUI.text = LocalizationManager.instance.GetTranslate(item.ID);
        }
    }
}
