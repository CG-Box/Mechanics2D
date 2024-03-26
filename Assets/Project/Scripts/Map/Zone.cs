using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ZoneID
{
    A,
    B,
    C,
    D
}

public class Zone : MapSpace
{
    [SerializeField] private Image image;

    public Color defaultColor = Color.white;
    public Color activeColor = Color.green;
    public Color disabledColor = Color.red;

    [SerializeField] private ZoneID zoneID;

    public ZoneID ZoneID { get { return zoneID; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void AddActive()
    {
        base.AddActive();
        ChangeColor(activeColor);
    }
    public override void RemoveActive()
    {
        base.RemoveActive();
        ChangeColor(defaultColor);
    }

    public override void Disable()
    {
        base.Disable();
        ChangeColor(disabledColor);
    }
    public override void Enable()
    {
        base.Enable();

        if(IsActive)
            ChangeColor(activeColor);
        else
            ChangeColor(defaultColor);
    }

    void ChangeColor(Color newColor)
    {
        image.color = newColor;
    }
}
