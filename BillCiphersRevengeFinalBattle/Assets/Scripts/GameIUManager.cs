using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameIUManager : MonoBehaviour
{
    private const string ENEMY_BULLET_TAG = "EnemyBullet";
    private const string MY_BULLET_TAG    = "Bullet";
    public TMPro.TextMeshProUGUI bulletCounterText;
    public TMPro.TextMeshProUGUI timeCounterText;

    public TMPro.TextMeshProUGUI lifeCounterText;

    public TMPro.TextMeshProUGUI playerBulletCounterText;
    public TMPro.TextMeshProUGUI enemyBulletCounterText;

    public GameObject gameOverScreen;

    public GameObject victoryScreen;
    public float startTime = 0f;
    public float timeLimit = 90f; // Tiempo límite en segundos (1 minuto y 30 segundos)

    private bool newGame = true;
    private bool gameEnded = false; // Para evitar múltiples finales del juego

    //private Player player; // Referencia al componente Player

    public void Start()
    {
        // Encuentra el objeto Player en la escena
        //player = GameObject.Find("Player").GetComponent<Player>();
        InvokeRepeating("UpdateTimeCounter", 0f, 1f); // Llamamos a la función UpdateTimeCounter cada 1 segundo
    }
    
    public void ResetGlobalVariables() {
        gameEnded = false;
        newGame   = true;
        Time.timeScale = 1f;
        InvokeRepeating("UpdateTimeCounter", 0f, 1f);
    }

    public void UpdateBulletCount()
    {
        try 
        {
            int enemyBulletCount = GameObject.FindGameObjectsWithTag(ENEMY_BULLET_TAG).Length;
            int playerBulletCount = GameObject.FindGameObjectsWithTag(MY_BULLET_TAG).Length;
            int bulletCount = enemyBulletCount + playerBulletCount;

            Debug.Log("Balas en el escenario: " + bulletCount);
            Debug.Log("Balas del enemigo: " + enemyBulletCount);
            Debug.Log("Balas del jugador: " + playerBulletCount);

            bulletCounterText.text = "Balas en el escenario: " + bulletCount;
            enemyBulletCounterText.text = "Balas del enemigo: " + enemyBulletCount;
            playerBulletCounterText.text = "Balas del jugador: " + playerBulletCount;
        } 
        catch {}
    }

    public void UpdateTimeCounter()
    {
        if (newGame) {
            startTime = Time.time;
            newGame = false;
        }

        if (gameEnded) return;

        float elapsedTime = Time.time;
        
        float timeDifference = elapsedTime - startTime;

        if (timeDifference >= timeLimit)
        {
            gameEnded = true;
            ShowVictoryScreen();
            Time.timeScale = 0f; // Pausa el juego
        }

        int minutes = Mathf.FloorToInt(timeDifference / 60F);
        int seconds = Mathf.FloorToInt(timeDifference % 60F);
        timeCounterText.text = string.Format("Tiempo transcurrido: {0:00}:{1:00}", minutes, seconds);
    }

    public void UpdateLifeCounter(int lifePoints)
    {
        lifeCounterText.text = "Vida: " + lifePoints;
    }

    public void ShowGameOverScreen()
    {
        CancelInvoke("UpdateTimeCounter");
        gameOverScreen.SetActive(true);
    }
    public void ShowVictoryScreen()
    {
        CancelInvoke("UpdateTimeCounter");
        victoryScreen.SetActive(true); 
    }
    public void RestartGame()
    {
        ResetGlobalVariables();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}