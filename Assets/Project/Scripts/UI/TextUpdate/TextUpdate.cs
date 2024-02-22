using UnityEngine;
using TMPro;

public abstract class TextUpdate<T> : MonoBehaviour
{
    [SerializeField]protected TextMeshProUGUI textToUpdate;

    [Header("Optional parts")]
    [SerializeField]protected string prefix = "";
    [SerializeField]protected string suffix = "";

    public void UpdateText(T newValue)
    {
        textToUpdate.text = prefix + newValue.ToString() + suffix;
    } 
}