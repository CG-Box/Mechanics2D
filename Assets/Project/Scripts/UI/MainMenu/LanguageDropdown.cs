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

        int selectedIndex = 0;
        for(int i = 0; i < Translator.Instance.phrases.Count; i++)
        {
            OriginalPhrases originalPhrases = Translator.Instance.phrases[i];
            dropdown.options.Add(new TMP_Dropdown.OptionData() {text = originalPhrases.language});
            if(originalPhrases.language == Translator.CurrentLanguage)
            {
                selectedIndex = i;
            }
        }
        dropdown.SetValueWithoutNotify(selectedIndex);
        dropdown.RefreshShownValue();
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
        Translator.UpdateText();
        Translator.SaveLanguage();
    }
}
