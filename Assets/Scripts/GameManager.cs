using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Aquí se guardan algunos datos importantes del juego.
    public int world { get; private set; }
    public int stage { get; private set; }
    public int lives { get; private set; }
    public int coins { get; private set; }

    /**
     * Esta función se ejecuta el instanciarse el objeto en el que se encuentra el script.
     */
    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    /**
     * Esta función se ejecuta al destruirse el objeto que contiene este script.
     */
    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    /**
     * Esta función se ejecuta al iniciar el juego.
     */
    private void Start()
    {
        // Limito los frames por segundo a 60.
        Application.targetFrameRate = 60;

        // Inicio una nueva partida.
        NewGame();
    }

    /**
     * Esta función configura el juego para empezar de 0.
     */
    public void NewGame()
    {
        lives = 3;
        coins = 0;

        LoadLevel(1, 1);
    }

    /**
     * Esta función hace empezar de nuevo el juego.
     */
    public void GameOver()
    {
        NewGame();
    }

    /**
     * Esta función carga el nivel indicado.
     */
    public void LoadLevel(int world, int stage)
    {
        this.world = world;
        this.stage = stage;

        SceneManager.LoadScene($"{world}-{stage}");
    }

    /**
     * Esta función hace pasar al siguiente nivel.
     */
    public void NextLevel()
    {
        LoadLevel(world, stage + 1);
    }

    /**
     * Esta función reinicia el nivel actual a los segundos indicados.
     */
    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), delay);
    }

    /**
     * Esta función reinicial el nivel actual, restandote una vida en el proceso.
     */
    public void ResetLevel()
    {
        lives--;

        if (lives > 0) {
            LoadLevel(world, stage);
        } else {
            GameOver();
        }
    }

    /**
     * Esta función te agrega una moneda.
     */
    public void AddCoin()
    {
        coins++;

        if (coins == 100)
        {
            coins = 0;
            AddLife();
        }
    }


    /**
     * Esta función te agrega una vida.
     */
    public void AddLife()
    {
        lives++;
    }

}
