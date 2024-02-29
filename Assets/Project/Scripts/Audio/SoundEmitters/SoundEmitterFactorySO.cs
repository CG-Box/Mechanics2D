using UnityEngine;

[CreateAssetMenu(fileName = "NewSoundEmitterFactory", menuName = "Audio/SoundEmitter Factory")]
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
