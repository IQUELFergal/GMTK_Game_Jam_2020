﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject endgameMenu;

    bool isPaused = false;

    EndGameInteractable endgameInteractable;

    public float respawnLag = 0.5f;
    public GameObject player;
    public GameObject start;
    public bool allowPressingRToResetPlayer = false;

    bool canReset = true;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        endgameMenu.SetActive(false);
        endgameInteractable = (EndGameInteractable)FindObjectOfType(typeof(EndGameInteractable));
        endgameInteractable.endGameEvent.AddListener(EndGame);
    }
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPlayer();
        }

    }

    public void FreezeGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void UnfreezeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        FreezeGame();
    }

    public void EndGame()
    {
        endgameMenu.SetActive(true);
        FreezeGame();
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        UnfreezeGame();
    }

    public void GoToMainMenu(SceneAsset scene)
    {
        SceneManager.LoadScene(scene.name);
    }

    public void Quit()
    {
        //Add a popup to ask if you are sure to quit
        Application.Quit();
    }

    // reset position
    public void ResetPlayer()
    {
        if (canReset)
        {
            player.SetActive(false);
            player.transform.position = start.transform.position;
            player.SetActive(true);
        }            
    }

    public void ContinuousResetPlayerPosition()
    {
        if (canReset)
        {
            ResetPlayer();
            StartCoroutine(CancelResetPlayer(3.0f));
        }
    }

    IEnumerator CancelResetPlayer(float duration)
    {
        canReset = false;
        yield return new WaitForSeconds(duration);
        canReset = true;
    }
}
