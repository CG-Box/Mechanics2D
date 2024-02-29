using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    [SerializeField] protected AudioCueEventChannelSO _sfxEventChannel = default;
	[SerializeField] protected AudioConfigurationSO _audioConfig = default;
	//[SerializeField] protected GameStateSO _gameState = default;

    [SerializeField] private AudioCueSO _caneSwing;
	[SerializeField] private AudioCueSO _liftoff;
	[SerializeField] private AudioCueSO _land;
	[SerializeField] private AudioCueSO _objectPickup;
	[SerializeField] private AudioCueSO _footsteps;
	[SerializeField] private AudioCueSO _getHit;
	[SerializeField] private AudioCueSO _die;
	[SerializeField] private AudioCueSO _talk;
	
	protected void PlayAudio(AudioCueSO audioCue, AudioConfigurationSO audioConfiguration, Vector3 position = default)
	{
		//if (_gameState.CurrentGameState != GameState.Cutscene)
			_sfxEventChannel.RaisePlayEvent(audioCue, audioConfiguration, position);
	}

	//[ContextMenu("PlayFootstep")]
    public void PlayFootstep() => PlayAudio(_footsteps, _audioConfig, transform.position);
	public void PlayJumpLiftoff() => PlayAudio(_liftoff, _audioConfig, transform.position);
	public void PlayJumpLand() => PlayAudio(_land, _audioConfig, transform.position);
	public void PlayCaneSwing() => PlayAudio(_caneSwing, _audioConfig, transform.position);
	public void PlayObjectPickup() => PlayAudio(_objectPickup, _audioConfig, transform.position);
	public void PlayGetHit() => PlayAudio(_getHit, _audioConfig, transform.position);
	public void PlayDie() => PlayAudio(_die, _audioConfig, transform.position);
	public void PlayTalk() => PlayAudio(_talk, _audioConfig, transform.position);


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayFootstep();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayDie();
        }
    }
}
