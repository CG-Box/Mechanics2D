using UnityEngine;
using UnityEngine.Events;
public class CheatInput : MonoBehaviour
{
    private string inputString = "";
    public string secretCode = "12345";

    [Header("Unity Events")]
    public UnityEvent OnCheatActivated;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        inputString += Input.inputString;
        if (inputString.Length > secretCode.Length)
        {
            inputString = inputString.Substring(inputString.Length - secretCode.Length, secretCode.Length);
        }
        if (inputString == secretCode)
        {
            SecretCodeEntered();
            inputString = "";
        }
    }

    void SecretCodeEntered()
    {
        OnCheatActivated?.Invoke();
        Debug.Log($"Cheat {secretCode} entered!");
    }
}