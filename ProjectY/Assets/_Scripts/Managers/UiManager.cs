using ScriptableObjectEvents;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseScreen;
    [SerializeField] private BoolEvent _gamePaused;

    [SerializeField] private InputActionProperty _pause;
    [SerializeField] private InputActionProperty _restart;

    private void OnEnable()
    {
        _pause.action.performed += _ => PauseInput();
        _restart.action.performed += _ => Restart();
    }

    private void OnDisable()
    {
        _pause.action.performed -= _ => PauseInput();
        _restart.action.performed -= _ => Restart();
    }

    private void Start()
    {
        HideCursor();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseInput();
        }
        if(Input.GetKeyDown(KeyCode.R)) 
        {
            Restart();
        }
    }

    private void Restart()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
        Unpause();
    }

    private void PauseInput()
    {
        if(Time.timeScale == 0)
        {
            Unpause();
        }
        else
        {
            Pause();
        }
    }

    private void Pause()
    {
        _pauseScreen.SetActive(true);
        Time.timeScale = 0;
        ShowCursor();
        //using oposite here because I want to deactivate or disable something when game is paused 
        _gamePaused.Raise(!true);
    }

    private void Unpause()
    {
        _pauseScreen.SetActive(false);
        Time.timeScale = 1;
        HideCursor();
        //using oposite here because I want to activate or enable something when game is unpaused 
        _gamePaused.Raise(!false);
    }

    public void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
