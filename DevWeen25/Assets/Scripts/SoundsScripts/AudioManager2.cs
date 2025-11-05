using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager2: MonoBehaviour
{
    public static AudioManager2 Instance;


    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioSource _musicSource;

    [SerializeField] private AudioSource _ambienceSource;


    [Header("--SoundSFX---")]
    public AudioClip schoolBell;
    public AudioClip kiss1;
    public AudioClip kiss2;
    public AudioClip gettingHitBall;


    [Header("--RandonSFX--")]
    public AudioClip[] steps;

    [Header("--Musics---")]
    public AudioClip gameMusic;
    

    [Header("--Ambience--")]
    public AudioClip schoolAmbience;
    public AudioClip quadraAmbience;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

    }

    void Start()
    {
            PlayMusic(gameMusic);
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume, float pitch = 1f)
    {
        AudioSource audioSource = Instantiate(_audioSource, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.pitch = pitch;

        audioSource.Play();

        float clipLenght = audioClip.length;

        //Destroi depois de tocar o clip;
        Destroy(audioSource.gameObject, clipLenght);


    }

    public void PlayRandomSoundFXClip(AudioClip[] audioClip, Transform spawnTransform, float volume, float pitch = 1f)
    {
        int rand = Random.Range(0, audioClip.Length);

        AudioSource audioSource = Instantiate(_audioSource, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip[rand];

        audioSource.volume = volume;

        audioSource.pitch = pitch;

        audioSource.Play();

        float clipLenght = audioSource.clip.length;

        //Destroi depois de tocar o clip;
        Destroy(audioSource.gameObject, clipLenght);


    }

    public void PlayMusic(AudioClip musicClip)
    {
        if (_musicSource.clip != musicClip)
        {
            _musicSource.clip = musicClip;
            _musicSource.loop = true;
            _musicSource.Play();
        }
    }

    public void PlayAmbience(AudioClip ambienceClip)
    {
        if (_ambienceSource.clip != ambienceClip)
        {
            _ambienceSource.clip = ambienceClip;
            _ambienceSource.loop = true;
            _ambienceSource.Play();
        }
    }

}
