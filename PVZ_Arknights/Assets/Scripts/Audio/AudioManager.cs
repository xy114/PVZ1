using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    public AudioSource backgroundMusicSource;
    public AudioSource soundEffectSource;
    
    public AudioClip mainMenuMusic;
    public AudioClip battleMusic;
    public AudioClip victoryMusic;
    public AudioClip gameOverMusic;
    
    public AudioClip plantPlaceSound;
    public AudioClip plantAttackSound;
    public AudioClip zombieGroanSound;
    public AudioClip zombieDeathSound;
    public AudioClip sunlightCollectSound;
    public AudioClip buttonClickSound;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        GameManager.Instance.OnGameStateChanged += OnGameStateChanged;
        EventManager.Instance.Subscribe(GameEvents.PLANT_PLACED, OnPlantPlaced);
        EventManager.Instance.Subscribe(GameEvents.ZOMBIE_SPAWNED, OnZombieSpawned);
        EventManager.Instance.Subscribe(GameEvents.ZOMBIE_KILLED, OnZombieKilled);
        EventManager.Instance.Subscribe(GameEvents.SUNLIGHT_COLLECTED, OnSunlightCollected);
        EventManager.Instance.Subscribe(GameEvents.LEVEL_COMPLETE, OnLevelComplete);
        EventManager.Instance.Subscribe(GameEvents.GAME_OVER, OnGameOver);
    }
    
    void OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Menu:
                PlayBackgroundMusic(mainMenuMusic);
                break;
            case GameState.Battle:
                PlayBackgroundMusic(battleMusic);
                break;
            case GameState.Victory:
                PlayBackgroundMusic(victoryMusic);
                break;
            case GameState.GameOver:
                PlayBackgroundMusic(gameOverMusic);
                break;
        }
    }
    
    void OnPlantPlaced(object[] args)
    {
        PlaySoundEffect(plantPlaceSound);
    }
    
    void OnZombieSpawned(object[] args)
    {
        PlaySoundEffect(zombieGroanSound);
    }
    
    void OnZombieKilled(object[] args)
    {
        PlaySoundEffect(zombieDeathSound);
    }
    
    void OnSunlightCollected(object[] args)
    {
        PlaySoundEffect(sunlightCollectSound);
    }
    
    void OnLevelComplete()
    {
        PlayBackgroundMusic(victoryMusic);
    }
    
    void OnGameOver()
    {
        PlayBackgroundMusic(gameOverMusic);
    }
    
    public void PlayBackgroundMusic(AudioClip clip)
    {
        if (backgroundMusicSource.clip != clip)
        {
            backgroundMusicSource.clip = clip;
            backgroundMusicSource.loop = true;
            backgroundMusicSource.Play();
        }
    }
    
    public void PlaySoundEffect(AudioClip clip)
    {
        soundEffectSource.PlayOneShot(clip);
    }
    
    public void PlayButtonClick()
    {
        PlaySoundEffect(buttonClickSound);
    }
    
    public void SetMusicVolume(float volume)
    {
        backgroundMusicSource.volume = volume;
    }
    
    public void SetSoundEffectVolume(float volume)
    {
        soundEffectSource.volume = volume;
    }
}
