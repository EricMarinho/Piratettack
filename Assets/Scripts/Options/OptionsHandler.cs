using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsHandler : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown sessionTimeDropdown;
    [SerializeField] private TMP_InputField spawnTimeInputField;
    [SerializeField] private AudioMixer audioMixer;
    private float[] sessionTime = { 60, 90, 120, 150, 180 };
    [SerializeField] private Slider volumeSlider;
    private float volume;
    private static readonly string volumePref = "volumePref";

    private void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volumePref", 1f);
    }

    private void OnEnable()
    {
        sessionTimeDropdown.value = PlayerPrefs.GetInt("SessionTimeOption");
        spawnTimeInputField.text = PlayerPrefs.GetFloat("SpawnTime").ToString();
        volumeSlider.value = PlayerPrefs.GetFloat(volumePref, 0.75f);
    }

    public void ApplyOptions()
    {
        PlayerPrefs.SetFloat("SessionTime", sessionTime[sessionTimeDropdown.value]);
        PlayerPrefs.SetInt("SessionTimeOption", sessionTimeDropdown.value);
        PlayerPrefs.SetFloat("SpawnTime", (float.Parse(((spawnTimeInputField.text == "" || spawnTimeInputField.text == "0") ? "1" : spawnTimeInputField.text))));
        SetVolume();
    }

    public void CancelOption()
    {
        volumeSlider.value = PlayerPrefs.GetFloat(volumePref, 0.75f);
    }

    public void SetVolume()
    {
        PlayerPrefs.SetFloat(volumePref, volumeSlider.value);
        audioMixer.SetFloat("volume", Mathf.Log10(PlayerPrefs.GetFloat("volumePref")) * 20);
    }

    public void TempChangeVolume()
    {
        audioMixer.SetFloat("Volume", Mathf.Log10(volumeSlider.value) * 20);
    }

}
