using UnityEngine;

[CreateAssetMenu(fileName = "TranslatedPhrases", menuName = "ScriptableObjects/Localization/TranslatedPhrases", order = 2)]
public class TranslatedPhrases : OriginalPhrases
{
    public OriginalPhrases originalPhrases;
}