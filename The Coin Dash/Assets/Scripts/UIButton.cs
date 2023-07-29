using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButton : MonoBehaviour
{

    public void Mainmenu()
    {
        SceneManager.LoadScene(0);
    }

    public void StartLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }
}
