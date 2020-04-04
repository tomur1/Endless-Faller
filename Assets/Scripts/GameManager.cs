using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary> Manages the state of the whole application </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager managerInstance;
    //used for showing score in main menu
    public int highscore;

    public enum GameState
    {
        HomeMenu,
        PauseMenu,
        Gameplay,
        GameOver
    }

    [SerializeField] private string gameScene;
    [SerializeField] private string menuScene;

    public GameState currentState;

    private void Awake()
    {
        if (managerInstance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            managerInstance = this;
        }

        SceneManager.sceneLoaded += OnSceneLoad;
        Object.DontDestroyOnLoad(gameObject);
        currentState = GameState.HomeMenu;
    }

    public void Play()
    {
        StartCoroutine(LoadScene(gameScene));
        currentState = GameState.Gameplay;
    }

    public void GoToHomeMenu()
    {
        StartCoroutine(LoadScene(menuScene));
        currentState = GameState.HomeMenu;
    }

    public void PauseGame()
    {
        //just changes the status. actual game stop is performed by level manager.
        currentState = GameState.PauseMenu;
    }

    public void ResumeGame()
    {
        currentState = GameState.Gameplay;
    }

    private void OnSceneLoad(Scene loadedScene, LoadSceneMode mode)
    {
        //if the home has been loaded set the highscore text
        if (loadedScene.name == "Home")
        {
            GameObject.Find("Play").GetComponent<Button>().onClick.AddListener(delegate { Play(); });
            GameObject.Find("Highscore").GetComponent<TMPro.TextMeshProUGUI>().SetText("Highscore: " + highscore);
        }
    }

    private IEnumerator LoadScene(string sceneName)
    {
        Debug.Log("Loading game!");
        yield return new WaitForSeconds(.4f);
        SceneManager.LoadScene(sceneName);
    }

}