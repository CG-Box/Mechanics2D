using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpace : MonoBehaviour
{
    bool isActive = false;
    public bool IsActive { get { return isActive; } }
    bool isDisabled = false;
    public bool IsDisabled { get { return isDisabled; } }

    public virtual void Disable()
    {
        isDisabled = true;
    }
    public virtual void Enable()
    {
        isDisabled = false;
    }

    public virtual void AddActive()
    {
        isActive = true;
    }
    public virtual void RemoveActive()
    {
        isActive = false;
    }
}
