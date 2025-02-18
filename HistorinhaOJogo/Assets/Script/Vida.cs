using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Vida : MonoBehaviour
{   
    public int vida = 100;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //slider.value = vida;
        if (vida <= 0)
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }
}
