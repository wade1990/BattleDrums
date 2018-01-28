using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public float countDownTime;

    public AudioClip GameMusic;
    public AudioClip EndMusic;

    public GameObject EndPanel;

    private AudioSource _audioSource;
    private GameState _gameState;

    public enum GameState
    {
        StartCountDownState,
        CountDownState,
        StartState,
        PlayState,
        EndState
    };

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _gameState = GameState.StartCountDownState;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("Menu");

        switch (_gameState)
        {
            case GameState.StartCountDownState:
                StartCoroutine(StartCountDown());
                break;
            case GameState.CountDownState:
                break;
            case GameState.StartState:
                Time.timeScale = 1.0f;
                _audioSource.clip = GameMusic;
                _audioSource.Play();
                _gameState = GameState.PlayState;
                break;
            case GameState.PlayState:
                if (!_audioSource.isPlaying)
                    TimeUp();
                break;
            case GameState.EndState:
                break;
        }
    }

    IEnumerator StartCountDown()
    {
        _gameState = GameState.CountDownState;
        yield return new WaitForSeconds(countDownTime);
        _gameState = GameState.StartState;
    }

    private void TimeUp()
    {
        _gameState = GameState.EndState;
        Time.timeScale = 0f;
        EndPanel.SetActive(true);
    }

    /// <summary>
    /// Play this when an army has been defeated.
    /// </summary>
    private void EndGame()
    {
        _gameState = GameState.EndState;
        Time.timeScale = 0f;
        EndPanel.SetActive(true);
        _audioSource.clip = EndMusic;
        _audioSource.Play();
    }
}
