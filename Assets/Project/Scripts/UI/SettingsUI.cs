using UnityEngine;
using UnityEngine.UI;

namespace Mechanics2D
{
    public class SettingstUI : MenuPanel 
    {
        [Header("Sliders")]
        [SerializeField] private Slider musisSlider;
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private Slider textSlider;

        [Header("Volume Events")]
        [Tooltip("The SoundManager listens to this event, fired by objects in any scene, to change SFXs volume")]
        [SerializeField] private FloatEventChannelSO _SFXVolumeEventChannel = default;
        [Tooltip("The SoundManager listens to this event, fired by objects in any scene, to change Music volume")]
        [SerializeField] private FloatEventChannelSO _musicVolumeEventChannel = default;
        [Tooltip("The SoundManager listens to this event, fired by objects in any scene, to change Master volume")]
        [SerializeField] private FloatEventChannelSO _masterVolumeEventChannel = default;

        [Header("Volume values")]
        public float musicVolume  = -1;
        public float sfxVolume  = -1;
        public int typingSpeed  = -1;

        bool isDirty = false;


	    #region EventsBinding
        void OnEnable()
        {
            AddBindings();

            ShowAnimation();
            SelectDefaultButton();
        }

        void OnDisable()
        {
            RemoveBindings();

            if(isDirty) SaveSettings();
        }

        void AddBindings()
        {
            musisSlider.onValueChanged.AddListener(MusicChange);
            sfxSlider.onValueChanged.AddListener(SfxChange);
            textSlider.onValueChanged.AddListener(TextSpeedChange);     
        }
        void RemoveBindings()
        {
            musisSlider.onValueChanged.RemoveListener(MusicChange);
            sfxSlider.onValueChanged.RemoveListener(SfxChange);
            textSlider.onValueChanged.RemoveListener(TextSpeedChange);
        }
        #endregion

        void Start() 
        {
            Init();
        }

        void Init()
        {
            if(IsSettingsInPrefs())
            {
                LoadSettings();
            }
            else
            {
                ResetToDefault();
            }
            UpdateUI();
        }
        public void ResetToDefault() 
        {
            musicVolume = 1;
            sfxVolume = 1;
            typingSpeed = 50;
        }

        void LoadSettings() 
        {
            musicVolume = PlayerPrefs.GetFloat(nameof(musicVolume));
            sfxVolume = PlayerPrefs.GetFloat(nameof(sfxVolume));
            typingSpeed = PlayerPrefs.GetInt(nameof(typingSpeed));
        }

        void SaveSettings()
        {
            PlayerPrefs.SetFloat(nameof(musicVolume), musicVolume);
            PlayerPrefs.SetFloat(nameof(sfxVolume), sfxVolume);
            PlayerPrefs.SetInt(nameof(typingSpeed), typingSpeed);

            isDirty = false;
        }

        bool IsSettingsInPrefs()
        {
            return PlayerPrefs.HasKey(nameof(musicVolume));
        }

        public void UpdateUI(bool makeDirty = false)
        {
            musisSlider.value = musicVolume;
            sfxSlider.value = sfxVolume;
            textSlider.value = typingSpeed;

            isDirty = makeDirty;
        }

        void MusicChange(float value)
        {
            musicVolume = value;
            SliderSharedChange(value);

            _musicVolumeEventChannel.RaiseEvent(musicVolume);
        }
        void SfxChange(float value)
        {
            sfxVolume = value;
            SliderSharedChange(value);

            _SFXVolumeEventChannel.RaiseEvent(sfxVolume);
        }
        void TextSpeedChange(float value)
        {
            typingSpeed = (int)value;
            SliderSharedChange(value);
        }
        void SliderSharedChange(float value)
        {
            isDirty = true;
            //Debug.Log($"slider value: {value}");
        }
    }
}
