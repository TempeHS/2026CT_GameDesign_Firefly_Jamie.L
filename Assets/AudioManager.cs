using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource footstepSource;

    [Header("Music Clips (by quest progress)")]
    [Tooltip("Played before the diary has been read for the first time.")]
    public AudioClip introMusic;
    [Tooltip("Played after DiaryReturned is true.")]
    public AudioClip afterDiaryMusic;
    [Tooltip("Played after SecondQuestCompleted is true.")]
    public AudioClip afterSecondQuestMusic;
    [Tooltip("Played after ThirdQuestCompleted is true.")]
    public AudioClip afterThirdQuestMusic;
    [Tooltip("Played after DiaryReadAgain is true (final stage).")]
    public AudioClip finalMusic;

    [Header("Shared Sound Effects")]
    public AudioClip doorOpenSound;
    public AudioClip footstepSound;

    [Header("Dialogue Blip Sounds")]
    [Tooltip("A random clip from this array is played per typed character.")]
    public AudioClip[] dialogueBlipSounds;

    [Range(0f, 1f)]
    public float musicVolume = 0.6f;
    [Range(0f, 1f)]
    public float sfxVolume = 1f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;
        footstepSource.volume = sfxVolume;

        SceneManager.sceneLoaded += (scene, mode) => RefreshMusicForQuestProgress();
        RefreshMusicForQuestProgress();
    }

    /// <summary>
    /// Picks the appropriate music track based on the highest quest milestone reached.
    /// Call this whenever a GameState flag changes, or a new scene loads.
    /// </summary>
    public void RefreshMusicForQuestProgress()
    {
        AudioClip targetClip;

        if (GameState.DiaryReadAgain)
        {
            targetClip = finalMusic;
        }
        else if (GameState.ThirdQuestCompleted)
        {
            targetClip = afterThirdQuestMusic;
        }
        else if (GameState.SecondQuestCompleted)
        {
            targetClip = afterSecondQuestMusic;
        }
        else if (GameState.DiaryReturned)
        {
            targetClip = afterDiaryMusic;
        }
        else
        {
            targetClip = introMusic;
        }

        PlayMusic(targetClip);
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (clip == null || musicSource.clip == clip) return;

        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    public void PlayDoorOpen() => PlaySFX(doorOpenSound);

    public void PlayFootstep()
    {
        if (footstepSound == null) return;

        footstepSource.Stop();
        footstepSource.clip = footstepSound;
        footstepSource.Play();
    }
    public void StopFootstep()
    {
        footstepSource.Stop();
    }

    public void PlayRandomDialogueBlip()
    {
        if (dialogueBlipSounds == null || dialogueBlipSounds.Length == 0) return;

        int index = Random.Range(0, dialogueBlipSounds.Length);
        PlaySFX(dialogueBlipSounds[index]);
    }
}