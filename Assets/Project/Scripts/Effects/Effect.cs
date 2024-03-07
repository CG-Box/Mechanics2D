using UnityEngine;

public class Effect : DescriptionBaseSO
{
    public virtual void Apply()
    {
        //Debug.Log("default apply effect");
    }

    public virtual void SetData(EffectData data)
    {
        Debug.Log("default set effect");
    }
}
