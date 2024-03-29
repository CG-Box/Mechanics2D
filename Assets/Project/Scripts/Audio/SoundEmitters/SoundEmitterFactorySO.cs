using UnityEngine;

[CreateAssetMenu(fileName = "NewSoundEmitterFactory", menuName = "ScriptableObjects/Audio/SoundEmitter Factory", order = 5)]
public class SoundEmitterFactorySO : FactorySO<SoundEmitter>
{
	//public SoundEmitter prefab = default;
    public GameObject soundEmitterPrefab = default;

	public override SoundEmitter Create()
	{
        //return Instantiate(prefab);
        return Instantiate(soundEmitterPrefab).GetComponent<SoundEmitter>();
	}
}
