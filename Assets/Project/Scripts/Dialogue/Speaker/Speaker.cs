using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public struct TypedImage 
{
    public Speaker.Emotion type;
    public Sprite image;
}

[CreateAssetMenu(fileName = "Speaker", menuName = "SriptableObjects/Speaker", order = 5)]
public class Speaker : DescriptionBaseSO
{
    public enum Person 
    {
        Unknown,
        GlassDude,
        Kappa,
        Random,
    }
    public enum Emotion
    {
        Normal,
        Angry,
        Sad
    }

    public Person person;
    public string displayName;
    public Color color;

    public Dictionary<Emotion, Sprite> emotions = new Dictionary<Emotion, Sprite>();
    public TypedImage[] emotionsList;

    [System.NonSerialized]
    bool dictionaryWasGenerated = false;

    /*public string DisplayName
    {
        get { return displayName; }
    }*/

    public void FillDictionary()
    {
        foreach(TypedImage currentEmotion in emotionsList)
        {
            emotions[currentEmotion.type] = currentEmotion.image;
        }
        dictionaryWasGenerated = true;
    }

    public Sprite GetEmotion(Speaker.Emotion emotionType)
    {
        if(!dictionaryWasGenerated)
        {
            FillDictionary();
        }

        Sprite emotionSprite = null;
        if (emotions.TryGetValue(emotionType, out emotionSprite))
        {
            //all is fine dicVal now has correct value
        }
        else
        {
            Debug.LogError($"Emotion: {emotionType} is not found in emotion list from {person}, default value was returned");
            emotionSprite = emotions[Emotion.Normal];
        }

        return emotionSprite;
    }
}
