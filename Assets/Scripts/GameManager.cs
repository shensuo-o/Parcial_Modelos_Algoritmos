using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float timeLimit = 10f;
    public bool playerAlive = true;
    public bool gameEnded = false;

    private IEnumerator<float> countdownEnumerator;

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
    }

    private void Start()
    {
        StartCountdown();
    }

    private void Update()
    {
        if (playerAlive && countdownEnumerator != null && !gameEnded)
        {
            if (!countdownEnumerator.MoveNext())
            {
                ChangeScene("Win");
            }
            else
            {
                Debug.Log($"Tiempo restante: {countdownEnumerator.Current:F2} segundos");
            }
        }
    }

    private void StartCountdown()
    {
        countdownEnumerator = CountdownGenerator(timeLimit).GetEnumerator();
    }
    //Generator que te muestra cuantos segundos quedan antes del cambio de escena
    private IEnumerable<float> CountdownGenerator(float startTime)
    {
        float currentTime = startTime;

        while (currentTime > 0f)
        {
            yield return currentTime;
            currentTime -= Time.deltaTime;
        }

        yield return 0f;
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        StartCountdown();
    }
}
