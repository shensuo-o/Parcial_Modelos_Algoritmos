using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateButtons : MonoBehaviour
{
    [SerializeField] private ButtonTranslate[] _textsToTranslate = new ButtonTranslate[0];
    [SerializeField] private Languages _language = default;

    [SerializeField] private int _currentLanguage = 0;
    public void TransalteBTN()
    {
        Debug.Log($"Accedi a {_language}");
        LocalizationManager.instance.language = _language;

        if (LocalizationManager.instance.language == Languages.English)
        {
            _currentLanguage = 1;
        }

        if (LocalizationManager.instance.language == Languages.Spanish)
        {
            _currentLanguage = 0;
        }

        PlayerPrefs.SetInt("Language", _currentLanguage);
        PlayerPrefs.Save();

        foreach (var item in _textsToTranslate)
        {
            item.textUI.text = LocalizationManager.instance.GetTranslate(item.ID);
        }
    }
}
