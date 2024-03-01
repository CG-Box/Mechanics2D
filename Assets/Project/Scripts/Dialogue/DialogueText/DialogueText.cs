using UnityEngine;


[CreateAssetMenu(fileName = "NewDialogueText", menuName = "ScriptableObjects/Dialogue/Text")]
public class DialogueText: DescriptionBaseSO
{
	[Header("JSON")]
    [SerializeField] private TextAsset[] jsonArray = default;

    public TextAsset InkJSON 
    {
        get 
        { 
            if(jsonArray == null)
            {
                Debug.LogError($"DialogueText can't be empty");
                return null;
            }
            else if(jsonArray.Length > Translator.CurrentLanguageIndex)
            {
                return jsonArray[Translator.CurrentLanguageIndex];
            }
            else
            {
                Debug.LogError($"DialogueText hasn't text for language {Translator.CurrentLanguageIndex} index, default value returned");
                return jsonArray[0];
            }
        }
    }
}
