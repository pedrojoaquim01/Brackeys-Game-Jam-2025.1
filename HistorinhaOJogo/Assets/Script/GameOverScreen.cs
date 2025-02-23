using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        // animator.Play("Idle");
        gameObject.SetActive(false);
    }

    public void GameOver() {
        gameObject.SetActive(true);
        animator.Play("GameOver");
    }


    public void ResetGame()
    {
        Debug.Log("Request reset stage");
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        Debug.Log("Request main menu");
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
