using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float timeLimit = 30f;
    public bool playerAlive = false;
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
        playerAlive = true;
        if (playerAlive)
        {
            StartCountdown();
        }
    }

    private void Update()
    {
        if (playerAlive && countdownEnumerator != null && !gameEnded)
        {
            if (!countdownEnumerator.MoveNext())
            {
                ChangeScene("Win");
                playerAlive = false;
            }
            else if (countdownEnumerator.Current <= 10f) // Solo muestra si está en los últimos 10 segundos
            {
                Debug.Log($"Tiempo restante (menos de 10 seg): {countdownEnumerator.Current:F2}");
            }
        }
        else if (!playerAlive && countdownEnumerator != null)
        {
            // Si el jugador murió, frenamos el generator
            countdownEnumerator = null;
        }
    }

    private void StartCountdown()
    {
        countdownEnumerator = CountdownGenerator(timeLimit)
                                .TakeWhile(t => t > 1f) // Ganamos 1 segundo antes para no morir de mala suerte
                                .GetEnumerator();
    }

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
        playerAlive = true;
        // Reinicia el generator solo si el jugador está vivo
        if (playerAlive)
        {
            StartCountdown();
        }
    }
}
