using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	[Header("Listening on channels")]

	[Tooltip("The SoundManager listens to this event, fired by objects in any scene, to play Music")]
	[SerializeField] private AudioCueEventChannelSO _musicEventChannel = default;



    [SerializeField] private AudioSource audioSource;


	private void OnEnable()
	{
		_musicEventChannel.OnAudioCuePlayRequested += PlayMusicTrack;
	}

	private void OnDestroy()
	{
		_musicEventChannel.OnAudioCuePlayRequested -= PlayMusicTrack;
	}


	private AudioCueKey PlayMusicTrack(AudioCueSO audioCue, AudioConfigurationSO audioConfiguration, Vector3 positionInSpace)
	{
        audioSource.clip = audioCue.GetClips()[0];
        audioSource.Play();

		return AudioCueKey.Invalid; //No need to return a valid key for music
	}



}
