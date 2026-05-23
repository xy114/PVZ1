using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }
    
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
    
    public void LoadScene(SceneName scene)
    {
        SceneManager.LoadSceneAsync((int)scene);
    }
    
    public void LoadSceneAdditive(SceneName scene)
    {
        SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Additive);
    }
    
    public void UnloadScene(SceneName scene)
    {
        SceneManager.UnloadSceneAsync((int)scene);
    }
    
    public void ReloadCurrentScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}

public enum SceneName
{
    MainMenu = 0,
    LevelSelect = 1,
    CharacterSelect = 2,
    Battle = 3
}
