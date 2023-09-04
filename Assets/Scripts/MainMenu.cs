using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button newGame;

    public Button exit;
    void Start()
    {
        newGame.onClick.AddListener(()=>
            SceneManager.LoadScene(1));
        
        exit.onClick.AddListener(Application.Quit);
    }
}
