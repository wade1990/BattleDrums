using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public float countDownTime;

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
    };

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
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
                StartCoroutine(StartCountDown());
                break;
            case GameState.CountDownState:
                break;
            case GameState.StartState:
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

    IEnumerator StartCountDown()
    {
        gameState = GameState.CountDownState;
        yield return new WaitForSeconds(countDownTime);
        Destroy(Metronome.gameObject);
        gameState = GameState.StartState;
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
