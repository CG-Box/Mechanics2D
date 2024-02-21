using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Z-User")]
public class EffectUser : Effect
{ 
    [SerializeField]private Effect[] useEffects;
    
    public void UseAll()
    {
        foreach(Effect effect in useEffects)
        {
            effect.Apply();
        }
    }

    public override void Apply()
    {
        base.Apply();
        UseAll();
    }

}
