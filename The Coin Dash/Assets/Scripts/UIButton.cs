using System.Collections;
using System.Collections.Generic;
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
}
