using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LanguageDropdown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;

    List<string> languages = default;

    void Start()
    {
        UnbindListener();
        FillDropdown();
        DropdownItemSelected(dropdown);
        BindListener();
    }

    void OnEnable()
    {
        BindListener();
    }

    void OnDisable()
    {
        UnbindListener();
    }

    void FillDropdown()
    {
        dropdown.options.Clear();
        languages = new List<string>();
        foreach(OriginalPhrases originalPhrases in Translator.Instance.phrases)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData() {text = originalPhrases.language});
        }
    }
    void BindListener()
    {
        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }
    void UnbindListener()
    {
        dropdown.onValueChanged.RemoveAllListeners();
    }

    void DropdownItemSelected(TMP_Dropdown dropdown)
    {
        Translator.SetLanguage(dropdown.value);
    }
}
