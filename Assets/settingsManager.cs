using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class settingsManager : MonoBehaviour
{
    [Header("Audio")]
    public AudioMixer audioMixer;
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public Toggle muteToggle;
    

    public Slider sensitivitySlider;
    void Start()
    {
        //Load saved values or defaults
        float master = PlayerPrefs.GetFloat("masterVolume", 1f);
        float music = PlayerPrefs.GetFloat("musicVolume", 1f);
        float sfx = PlayerPrefs.GetFloat("sfxVolume", 1f);
        bool muted = PlayerPrefs.GetInt("mute", 0) == 1;

        // First set sliders
        masterVolumeSlider.minValue = 0;
        masterVolumeSlider.maxValue = 1;
        musicVolumeSlider.minValue = 0;
        musicVolumeSlider.maxValue = 1;
        sfxVolumeSlider.minValue = 0;
        sfxVolumeSlider.maxValue = 1;

        masterVolumeSlider.wholeNumbers = false;
        musicVolumeSlider.wholeNumbers = false;
        sfxVolumeSlider.wholeNumbers = false;

        masterVolumeSlider.value = master;
        musicVolumeSlider.value = music;
        sfxVolumeSlider.value = sfx;
        muteToggle.isOn = muted;

        ApplyMasterVolume(master);
        ApplyMusicVolume(music);
        ApplySfxVolume(sfx);
        ApplyMute(muted);

        float sensitivity = PlayerPrefs.GetFloat("Sensitivity", 2f);
        sensitivitySlider.value = sensitivity;
        GameSettings.sensitivity = sensitivity;
    }

    // --Audio functions--
    public void ApplyMasterVolume(float value)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(Mathf.Max(value, 0.001f)) * 20);
        PlayerPrefs.SetFloat("masterVolume", value);
        Debug.Log("Master volume:" + value);
    }
    public void ApplyMusicVolume(float value)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(Mathf.Max(value, 0.001f)) * 20);
        PlayerPrefs.SetFloat("musicVolume", value);
        Debug.Log("Music volume:" + value);
    }
    public void ApplySfxVolume(float value)
    {
        audioMixer.SetFloat("sfxVolume", Mathf.Log10(Mathf.Max(value, 0.001f)) * 20);
        PlayerPrefs.SetFloat("sfxVolume", value);
        Debug.Log("Sfx volume:" + value);
    }
    public void ApplyMute(bool isMuted)
    {
        if (isMuted)
        {
            audioMixer.SetFloat("masterVolume", -80f);
            audioMixer.SetFloat("musicVolume", -80f);
            audioMixer.SetFloat("sfxVolume", -80f);
        }
        else
        {
            ApplyMasterVolume(masterVolumeSlider.value);
            ApplyMusicVolume(musicVolumeSlider.value);
            ApplySfxVolume(sfxVolumeSlider.value);
        }
        
        PlayerPrefs.SetInt("mute", isMuted ? 1 : 0);
    }
    public void SetSensitivity(float value)
    {
        GameSettings.sensitivity = value;
        PlayerPrefs.SetFloat("Sensitivity", value);
        Debug.Log("Sensitivity:" + value);
    }
}
