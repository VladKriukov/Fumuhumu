using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    [SerializeField] GameObject nextLevelButton;
    int currentLevel;

    Rigidbody[] rbs;

    void OnEnable()
    {
        GameManager.OnStartLevel += StartOfGame;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        GameManager.OnStartLevel -= StartOfGame;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        SetRigidbodiesKinematic();
    }

    void StartOfGame()
    {
        if (SceneManager.sceneCountInBuildSettings < currentLevel)
        {
            nextLevelButton.SetActive(false);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        currentLevel++;
        if (SceneManager.sceneCountInBuildSettings > currentLevel)
        {
            SceneManager.LoadScene(currentLevel);
        }
    }

    void SetRigidbodiesKinematic()
    {
        rbs = FindObjectsOfType<Rigidbody>();
        foreach (var item in rbs)
        {
            item.isKinematic = true;
        }
    }

}
