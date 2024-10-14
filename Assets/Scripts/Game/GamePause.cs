using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject resumeButton;
    public void PauseGame()
    {
        Time.timeScale = 0f; 

        pauseButton.SetActive(false);
        resumeButton.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f; 
        pauseButton.SetActive(true);
        resumeButton.SetActive(false);
    }
}