using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager instance = default;
    public Languages language = default;

    [SerializeField] private DataLocalization[] _data = default;

    [SerializeField] private Dictionary<Languages, Dictionary<string, string>> _translate = new();

    [SerializeField] public int _currentLanguage = 0;

    private void Awake()
    {
        instance = this;
        _translate = LanguageU.LoadTranslate(_data);

        _currentLanguage = PlayerPrefs.GetInt("Language");

        
    }

    private void Start()
    {
        if (_currentLanguage == 0)
        {
            language = Languages.Spanish;
        }

        if (_currentLanguage == 1)
        {
            language = Languages.English;
        }
    }

    public string GetTranslate(string ID)
    {
        if (!_translate.ContainsKey(language))
            return "No lang";

        if (!_translate[language].ContainsKey(ID))
            return "No ID";

        return _translate[language][ID];
    }

    private void Update()
    {
        _currentLanguage = PlayerPrefs.GetInt("Language");
        
    }
}