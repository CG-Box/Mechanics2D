using TMPro;
using UnityEngine;

public class TranslatedText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string phraseKey;
    public bool setTextOnStart = true;

    void Awake ()
    {
        if (text == null)
            text = GetComponent<TextMeshProUGUI> ();
    }

    public void SetText ()
    {
        text.text = Translator.Instance[phraseKey];
    }

    void OnEnable()
    {
        if(setTextOnStart)
            SetText();
    }
}