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

        public int musicVolume  = -1;
        public int sfxVolume  = -1;
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
            musicVolume = 100;
            sfxVolume = 100;
            typingSpeed = 50;
        }

        void LoadSettings() 
        {
            musicVolume = PlayerPrefs.GetInt(nameof(musicVolume));
            sfxVolume = PlayerPrefs.GetInt(nameof(sfxVolume));
            typingSpeed = PlayerPrefs.GetInt(nameof(typingSpeed));
        }

        void SaveSettings()
        {
            PlayerPrefs.SetInt(nameof(musicVolume), musicVolume);
            PlayerPrefs.SetInt(nameof(sfxVolume), sfxVolume);
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
            musicVolume = (int)value;
            SliderSharedChange(value);
        }
        void SfxChange(float value)
        {
            sfxVolume = (int)value;
            SliderSharedChange(value);
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
