using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [field:SerializeField] public string StartGameScene { get; private set; } = string.Empty;
    [field:SerializeField] private Options options;
    private Animator animator;
    [field:SerializeField] private Transform[] buttons;

    private void Awake() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
    private void Start() {
        animator = gameObject.GetComponent<Animator>();
        animator.Play("Default");
        Options.OnOptionClose += OptionsClosed;
    }

    private void OptionsClosed()
    {
        animator.Play("Default");
    }


    public void StartGame()
    {
        animator.Play("StartGame");
    }
    public void _StartGame()
    {
        SceneManager.LoadSceneAsync(StartGameScene);
    }

    public void OpenOptions()
    {
        animator.Play("OpenOptions");
    }

    public void _OpenOptions()
    {
        options.Open();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ShowButtons()
    {
        foreach (var btn in buttons)
            btn.gameObject.SetActive(true);
    }
    public void HideButtons()
    {
        foreach (var btn in buttons)
            btn.gameObject.SetActive(false);
    }
}
