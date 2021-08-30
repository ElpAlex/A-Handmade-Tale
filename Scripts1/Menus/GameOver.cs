using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject gamOverMenuUI;
    public PlayerController player;
    public playerHealth playerHealth;
    public bossContainer boss;

    public void RestartLevel()
    {
        gamOverMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;



        if (player.level1)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        else if (player.level2)
        {
            player.transform.position = new Vector3(702, 121, -234.8f);
            playerHealth.currentHealth = 5;
            player.runSpeed = 2;
        }
        else if (player.level3)
        {
            playerHealth.currentHealth = 5;
            player.bossTouched = false;
            
        }
    }

    public void StopGame()
    {
        gamOverMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f; SceneManager.LoadScene("Menu");
    }

    public void quitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}

