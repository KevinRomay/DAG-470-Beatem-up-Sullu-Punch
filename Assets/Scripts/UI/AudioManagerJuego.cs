using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManagerJuego : MonoBehaviour
{
    [Header("Mixer")]
    public AudioMixer mixer;
    public AudioMixerGroup gameGroup;
    public AudioMixerGroup sfxGroup;

    [Header("Clips del juego")]
    public AudioClip gameplayMusic;

    [Header("AudioSources")]
    private AudioSource musicSource;
    private AudioSource sfxSource;

    private void Awake()
    {
        // Fuente de música
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.playOnAwake = false;
        musicSource.outputAudioMixerGroup = gameGroup;

        // Fuente SFX
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;
        sfxSource.playOnAwake = false;
        sfxSource.outputAudioMixerGroup = sfxGroup;
    }

    private void Start()
    {
        if (gameplayMusic != null)
        {
            musicSource.clip = gameplayMusic;
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, volume);
    }
}

