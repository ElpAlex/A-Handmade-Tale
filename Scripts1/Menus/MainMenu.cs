using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


    private void Update()
    {
        AudioListener.pause = false;
    }

    public void PlayGame()
    {
        AudioListener.pause = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Exit!");
        Application.Quit();
    }
}