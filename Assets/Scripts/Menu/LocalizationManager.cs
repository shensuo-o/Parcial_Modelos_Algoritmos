using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager instance = default;
    public Languages language = default;

    [SerializeField] private DataLocalization[] _data = default;

    [SerializeField] private Dictionary<Languages, Dictionary<string, string>> _translate = new();

    private void Awake()
    {
        instance = this;
        _translate = LanguageU.LoadTranslate(_data);
    }

    public string GetTranslate(string ID)
    {
        if (!_translate.ContainsKey(language))
            return "No lang";

        if (!_translate[language].ContainsKey(ID))
            return "No ID";

        return _translate[language][ID];
    }
}