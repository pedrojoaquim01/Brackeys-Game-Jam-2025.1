using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [field:SerializeField] public string StartGameScene { get; private set; } = string.Empty;
    [field:SerializeField] private Options options;
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(StartGameScene);
    }

    public void OpenOptions()
    {
        options.Open();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
