using System.Collections.Generic;
using UnityEngine;

public class Translator : MonoBehaviour
{
    public static Translator Instance
    {
        get
        {
            if (s_Instance != null)
                return s_Instance;

            s_Instance = FindObjectOfType<Translator>();

            if (s_Instance != null)
                return s_Instance;

            return CreateDefault();
        }
    }

    protected static Translator s_Instance;

    static Translator CreateDefault ()
    {
        Translator prefab = Resources.Load<Translator>("Translator");
        Translator defaultInstance = Instantiate(prefab);
        return defaultInstance;
    }

    public static string CurrentLanguage
    {
        get { return Instance.phrases[Instance.m_LanguageIndex].language; }
    }

    public static int CurrentLanguageIndex 
    {
        get { return Instance.m_LanguageIndex; }
    }

    public List<OriginalPhrases> phrases = new List<OriginalPhrases> ();

    [SerializeField]
    protected int m_LanguageIndex;

    public string this [string key]
    {
        get { return phrases[m_LanguageIndex][key]; }
    }

    public static bool SetLanguage (int index)
    {
        if (index >= Instance.phrases.Count || index < 0)
            return false;

        Instance.m_LanguageIndex = index;
        return true;
    }

    public static bool SetLanguage (string language)
    {
        for (int i = 0; i < Instance.phrases.Count; i++)
        {
            if (Instance.phrases[i].language == language)
            {
                Instance.m_LanguageIndex = i;
                return true;
            }
        }
        return false;
    }

    public static void SetLanguage (TranslatedPhrases phrases)
    {
        for (int i = 0; i < Instance.phrases.Count; i++)
        {
            if (Instance.phrases[i] == phrases)
            {
                Instance.m_LanguageIndex = i;
                return;
            }
        }
        Instance.phrases.Add (phrases);
        Instance.m_LanguageIndex = Instance.phrases.Count - 1;
    }

    public static void UpdateText()
    {
        TranslatedText[] translatedTexts = FindObjectsOfType<TranslatedText>();
        foreach (TranslatedText translatedText in translatedTexts) {
            translatedText.SetText();
        }
    } 

    //TO DO: not working singleton, need to rework\change it
    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            Debug.Log("same tranlator must be destroyed");
            return;
        }
        DontDestroyOnLoad(gameObject);

        LoadLanguage();
    }

    public static void LoadLanguage()
    {
        string userLanguage;

        if (PlayerPrefs.HasKey(nameof(CurrentLanguage)))
        {
            userLanguage = PlayerPrefs.GetString(nameof(CurrentLanguage));
            Translator.SetLanguage(userLanguage);
        }
    }
    public static void SaveLanguage()
    {
        PlayerPrefs.SetString(nameof(CurrentLanguage), CurrentLanguage);
    }
}