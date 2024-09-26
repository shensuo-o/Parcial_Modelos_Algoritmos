using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float timer;
    public float timeLimit;
    public bool playerAlive = true;
    public bool gameEnded = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        timer = timeLimit;
    }
    private void Update()
    {
        if(!gameEnded)
        {
            if (playerAlive)
            {
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    gameEnded = true;
                    ChangeScene("Win");
                }
            }
        }       
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
