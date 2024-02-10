using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Z-User")]
public class EffectUser : DescriptionBaseSO
{ 
    [SerializeField]private Effect[] useEffects;

    [ContextMenu("Use All Effects")]
    public void UseAll()
    {
        foreach(Effect effect in useEffects)
        {
            effect.Apply();
        }
    }
}
