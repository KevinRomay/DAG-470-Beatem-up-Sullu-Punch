using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsAudio : MonoBehaviour
{
    public AudioMixer mixer;  // Mezclador asignado en el Inspector
    public Slider musicSlider;
    public Slider sfxSlider;

    private const string MUSIC_KEY = "musicVolume";
    private const string SFX_KEY = "sfxVolume";

    private void Start()
    {
        // Cargar valores guardados (0..1)
        float musicVal = PlayerPrefs.HasKey(MUSIC_KEY) ? PlayerPrefs.GetFloat(MUSIC_KEY) : 1f;
        float sfxVal = PlayerPrefs.HasKey(SFX_KEY) ? PlayerPrefs.GetFloat(SFX_KEY) : 1f;

        // Asignar sliders
        if (musicSlider != null) musicSlider.value = musicVal;
        if (sfxSlider != null) sfxSlider.value = sfxVal;

        // Aplicar al mixer
        SetMusicVolume(musicVal);
        SetSFXVolume(sfxVal);

        // Listeners para actualizar en tiempo real
        if (musicSlider != null) musicSlider.onValueChanged.AddListener(SetMusicVolume);
        if (sfxSlider != null) sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMusicVolume(float linear)
    {
        float dB = LinearToDecibel(linear);
        mixer.SetFloat("MusicVolume", dB);
        PlayerPrefs.SetFloat(MUSIC_KEY, linear);
    }

    public void SetSFXVolume(float linear)
    {
        float dB = LinearToDecibel(linear);
        mixer.SetFloat("SFXVolume", dB);
        PlayerPrefs.SetFloat(SFX_KEY, linear);
    }

    /// <summary>
    /// Convierte un valor lineal (0..1) a decibeles con curva suave.
    /// Max dB limitado a -10 para evitar que la música/SFX sea demasiado fuerte.
    /// </summary>
    private float LinearToDecibel(float linear)
    {
        linear = Mathf.Clamp(linear, 0.0001f, 1f);  // evitar log(0)

        float minDb = -80f;   // silencio
        float maxDb = -10f;   // límite de volumen máximo (antes 0 dB)

        // curva de sensibilidad más natural
        float curved = Mathf.Pow(linear, 1.5f);

        return Mathf.Lerp(minDb, maxDb, curved);
    }

    private void OnDisable()
    {
        PlayerPrefs.Save();
    }
}


