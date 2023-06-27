using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StoryGameManager : MonoBehaviour
{
    public static StoryGameManager Instance { get; private set; }

    private int m_currentLevel;
    private int m_maxLevels;
    private bool m_isPlayerAlive;
    private bool m_isGamePaused;
    private PlayerManager m_playerManager;
    public GameObject m_pauseCanvas;
    public GameObject m_lostCanvas;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        m_playerManager = FindObjectOfType<PlayerManager>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !m_pauseCanvas.activeSelf) 
        {
            PauseGame();
            m_pauseCanvas.SetActive(true);
        }
        else
        {
            ResumeGame();
            m_pauseCanvas.SetActive(false);
        }
        if(m_playerManager.GetPlayerHealth() <= 0)
        {
            m_lostCanvas.SetActive(true);
            StartCoroutine(LostTimer(10f));
        }
    }
    public void OnClickToGoBackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    private IEnumerator LostTimer(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("MainMenu");
        Destroy(gameObject);
    }
    //public void StartGame()
    //{
    //    m_currentLevel = 1;
    //    m_isPlayerAlive = true;
    //    m_isGamePaused = false;
    //    LoadLevel(m_currentLevel);
    //}

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene("Level" + levelIndex);
    }

    public void PlayerDied()
    {
        m_isPlayerAlive = false;
        PauseGame();
        //Activate dead-player menu
    }

    public void PlayerCompletedLevel()
    {
        m_currentLevel++;
        if (m_currentLevel > m_maxLevels)
        {
            //Game finished screen
        }
        else
        {
            LoadLevel(m_currentLevel);
        }
    }

    public void PauseGame()
    {
        m_isGamePaused = true;
        Time.timeScale = 0f;
        //Activate pause menu
    }
    public void ActivatePauseMenu()
    {

    }
    public void DeactivatePauseMenu() 
    {

    }
    public void ActivateDeathMenu()
    {

    }

    public void ResumeGame()
    {
        m_isGamePaused = false;
        Time.timeScale = 1f;
        //Deactivate pause menu
    }
}



