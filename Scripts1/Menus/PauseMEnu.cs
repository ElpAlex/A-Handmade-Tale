using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMEnu : MonoBehaviour {

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public PlayerController player;

    //public Texture2D cursorTexture;
    //public CursorMode cursorMode = CursorMode.Auto;
    //public Vector2 hotSpot = Vector2.zero;

    // Update is called once per frame
    void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if (GameIsPaused && !player.loading)
        {
            AudioListener.pause = true;

        }
        else if (!GameIsPaused && player.loading)
        {
            AudioListener.pause = true;
        }
        else if (!GameIsPaused && !player.loading)
        {
            AudioListener.pause = false;
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive (false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        Resume();
        AudioListener.pause = true;
        SceneManager.LoadScene("Menu");
    }

    public void quitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
