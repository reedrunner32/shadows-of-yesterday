using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TMP namespace

public class SettingsUI : MonoBehaviour
{
    public Slider sensitivitySlider;
    public TextMeshProUGUI sensitivityValueText; // TMP version of Text

    void Start()
    {
        float current = SettingsManager.Instance.GetSensitivity();
        sensitivitySlider.value = current;
        sensitivityValueText.text = current.ToString("F2");

        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
    }

    void OnSensitivityChanged(float value)
    {
        SettingsManager.Instance.SetSensitivity(value);
        sensitivityValueText.text = value.ToString("F2");
    }
}
