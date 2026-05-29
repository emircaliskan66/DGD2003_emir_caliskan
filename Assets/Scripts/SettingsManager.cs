using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider sensitivitySlider;

    [Header("Value Texts")]
    [SerializeField] private TMP_Text soundValueText;
    [SerializeField] private TMP_Text sensitivityValueText;

    public static float MouseSensitivity { get; private set; } = 2f;

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("SoundVolume", 0.75f);
        float savedSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 500f);

        soundSlider.value = savedVolume;
        sensitivitySlider.value = savedSensitivity;

        AudioListener.volume = savedVolume;
        MouseSensitivity = savedSensitivity;

        UpdateSoundText(savedVolume);
        UpdateSensitivityText(savedSensitivity);

        soundSlider.onValueChanged.AddListener(SetSoundVolume);
        sensitivitySlider.onValueChanged.AddListener(SetMouseSensitivity);
    }

    public void SetSoundVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("SoundVolume", value);
        PlayerPrefs.Save();

        UpdateSoundText(value);
    }

    public void SetMouseSensitivity(float value)
    {
        MouseSensitivity = value;
        PlayerPrefs.SetFloat("MouseSensitivity", value);
        PlayerPrefs.Save();

        UpdateSensitivityText(value);
    }

    private void UpdateSoundText(float value)
    {
        soundValueText.text = Mathf.RoundToInt(value * 100) + " ";
    }

    private void UpdateSensitivityText(float value)
    {
        sensitivityValueText.text = Mathf.RoundToInt(value).ToString();
    }
}