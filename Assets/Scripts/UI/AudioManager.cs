using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Mixer")]
    public AudioMixer mixer;
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup sfxGroup;

    [Header("Music Clips")]
    public AudioClip menusClip; // Música del menú

    [Header("SFX Clips")]
    public AudioClip buttonClickClip;

    [Header("AudioSources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Asegurarse de que sea un GameObject raíz
        if (transform.parent != null)
            transform.parent = null;

        DontDestroyOnLoad(gameObject);

        // Crear AudioSources si no existen
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
            musicSource.playOnAwake = false;
            if (musicGroup != null) musicSource.outputAudioMixerGroup = musicGroup;
        }

        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.loop = false;
            sfxSource.playOnAwake = false;
            if (sfxGroup != null) sfxSource.outputAudioMixerGroup = sfxGroup;
        }
    }

    private void Start()
    {
        // Reproducir música del menú automáticamente si no está sonando
        if (!musicSource.isPlaying)
        {
            musicSource.clip = menusClip;
            musicSource.Play();
        }
    }

    // Reproducir SFX
    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, volume);
    }

    // Sonido de botón
    public void PlayButtonClick() => PlaySFX(buttonClickClip);
}
