using Assets.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int CountDownBeats;
    private int _count;

    public Player Player1;
    public Player Player2;

    public AudioClip GameMusic;
    public AudioClip EndMusic;

    public GameObject EndPanel;
    public GameObject Metronome;

    private AudioSource _audioSource;

    public GameState gameState;
    public enum GameState
    {
        StartCountDownState,
        CountDownState,
        StartState,
        PlayState,
        EndState
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        Player1.AllUnitsDied += x => EndGame();
        Player2.AllUnitsDied += x => EndGame();
    }

    private void Start()
    {
        gameState = GameState.StartCountDownState;
        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("Menu");

        switch (gameState)
        {
            case GameState.StartCountDownState:
                gameState = GameState.CountDownState;
                BeatManager.Instance.Beat.AddListener(count);
                break;
            case GameState.CountDownState:
                if (_count >= CountDownBeats)
                    gameState = GameState.StartState;
                break;
            case GameState.StartState:
                Destroy(Metronome.gameObject);
                _audioSource.clip = GameMusic;
                _audioSource.Play();
                gameState = GameState.PlayState;
                break;
            case GameState.PlayState:
                if (!_audioSource.isPlaying)
                    TimeUp();
                break;
            case GameState.EndState:
                break;
        }
    }

    private void count()
    {
        _count++;
    }

    private void TimeUp()
    {
        gameState = GameState.EndState;
        Time.timeScale = 0f;
        EndPanel.SetActive(true);
    }

    /// <summary>
    /// Play this when an army has been defeated.
    /// </summary>
    private void EndGame()
    {
        gameState = GameState.EndState;
        Time.timeScale = 0f;
        EndPanel.SetActive(true);
        _audioSource.clip = EndMusic;
        _audioSource.Play();
    }
}
