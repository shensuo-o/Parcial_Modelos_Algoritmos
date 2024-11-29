using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject pauseMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
        }
    }

    public void Jugar()
    {
        SceneManager.LoadScene("Parcial1");
    }

    public void Salir()
    {
        Debug.Log("Sali del juego");
        Application.Quit();
    }

    public void Controls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void GoBack()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
    }
}
