using DefaultNamespace;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private FirstPersonController _personController;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _exitButton;

    private bool _isPaused;

    private void Awake()
    {
        _continueButton.onClick.AddListener(() =>
        {
            _isPaused = false;
            Pause(false);
            LockCursor(true);
            _pauseMenu.SetActive(false);
        });

        _exitButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
            Pause(false);
        });
        
        _isPaused = false;
        _pauseMenu.SetActive(_isPaused);
        LockCursor(true);
    }

    private void Pause(bool pause)
    {
        Time.timeScale = pause ? 0 : GameConfig.DEFAULT_TIMESCALE;
        _personController.enabled = !pause;
    }
    
    private void LockCursor(bool lockCursor)
    {
        Cursor.visible = !lockCursor;
        Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
    }
    

    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        LockCursor(_isPaused);
        _isPaused = !_isPaused;
        Pause(_isPaused);
        _pauseMenu.SetActive(_isPaused);
    }
}