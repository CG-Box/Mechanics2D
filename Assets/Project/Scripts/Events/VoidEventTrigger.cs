using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A flexible trigger for void events in the form of a MonoBehaviour. Can be used from buttons
/// </summary>
public class VoidEventTrigger : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO _channel = default;

    // Start is called before the first frame update
    public void RaiseEvent()
    {
        _channel.RaiseEvent();
    }
}
