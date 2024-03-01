using UnityEngine;

[CreateAssetMenu(fileName = "NewSoundEmitterPool", menuName = "ScriptableObjects/Audio/SoundEmitter Pool", order = 4)]
public class SoundEmitterPoolSO : ComponentPoolSO<SoundEmitter>
{
	[SerializeField] private SoundEmitterFactorySO _factory;

	public override IFactory<SoundEmitter> Factory
	{
		get
		{
			return _factory;
		}
		set
		{
			_factory = value as SoundEmitterFactorySO;
		}
	}
}
