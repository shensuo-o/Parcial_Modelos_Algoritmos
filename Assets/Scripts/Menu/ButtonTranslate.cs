using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonTranslate : MonoBehaviour
{
    public string ID = default;
    public TextMeshProUGUI textUI = default;

    void Start()
    {
        textUI.text = LocalizationManager.instance.GetTranslate(ID);
    }
}