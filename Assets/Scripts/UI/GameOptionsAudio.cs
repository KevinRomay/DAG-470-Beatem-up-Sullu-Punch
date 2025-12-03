using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameOptionsAudio : MonoBehaviour
{
    public AudioMixer mixer;

    public Slider gameSlider;
    public Slider sfxSlider;

    private const string GAME_KEY = "gameVolume";
    private const string SFX_KEY = "gameSfxVolume";

    private void Start()
    {
        float gameVal = PlayerPrefs.HasKey(GAME_KEY) ? PlayerPrefs.GetFloat(GAME_KEY) : 1f;
        float sfxVal = PlayerPrefs.HasKey(SFX_KEY) ? PlayerPrefs.GetFloat(SFX_KEY) : 1f;

        if (gameSlider != null) gameSlider.value = gameVal;
        if (sfxSlider != null) sfxSlider.value = sfxVal;

        SetGameVolume(gameVal);
        SetSFXVolume(sfxVal);

        if (gameSlider != null) gameSlider.onValueChanged.AddListener(SetGameVolume);
        if (sfxSlider != null) sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetGameVolume(float val)
    {
        mixer.SetFloat("GameVolume", LinearToDB(val));
        PlayerPrefs.SetFloat(GAME_KEY, val);
    }

    public void SetSFXVolume(float val)
    {
        mixer.SetFloat("SFXVolume", LinearToDB(val));
        PlayerPrefs.SetFloat(SFX_KEY, val);
    }

    private float LinearToDB(float linear)
    {
        linear = Mathf.Clamp(linear, 0.0001f, 1f);
        return Mathf.Lerp(-80f, -10f, Mathf.Pow(linear, 1.5f));
    }

    private void OnDisable()
    {
        PlayerPrefs.Save();
    }
}
