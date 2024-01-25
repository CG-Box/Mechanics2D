using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpeakersLibrary", menuName = "SriptableObjects/SpeakersLibrary", order = 6)]
public class SpeakersLibrary : ScriptableObject
{
    [SerializeField]
    private List<Speaker> itemsViewList; //only for viewing in the inspector

    static Dictionary<Speaker.Person, Speaker> itemsDatabase;

    [ContextMenu("GenerateDatabase")]
    public void CollectItemsFromFolder()
    {
        itemsViewList = Resources.LoadAll<Speaker>(path: "Speakers").OrderBy(item => item.person).ToList();

        //For static
        UpdateStaticLibrary();
    }

    public void UpdateStaticLibrary()
    {
        UpdateStaticLibrary(itemsViewList);
    }
    public void UpdateStaticLibrary(List<Speaker> itemsViewList)
    {
        itemsDatabase = itemsViewList.ToDictionary(keySelector: item => item.person, elementSelector: item => item);
    }

    public Speaker GetItemData(Speaker.Person type)
    {
        return itemsViewList.Find(item => item.person == type);
    }
    public Speaker GetItem(Speaker.Person type)
    {
        return GetItemStatic(type);
    }

    public static Speaker GetItemStatic(Speaker.Person type)
    {
        Speaker speaker = null;
        if (itemsDatabase.TryGetValue(type, out speaker))
        {
            //all is fine speaker now has correct value
        }
        else
        {
            Debug.LogError($"Speaker type: {type} is not found in speakers library, default value was returned");
            speaker = itemsDatabase[Speaker.Person.Unknown];
        }

        return speaker;
    }
}
