using Assets.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int CountDownBeats;
    private int _count;

    public Player Player1;
    public Player Player2;

    public AudioClip GameMusic;
    public AudioClip TimeUpMusic;
    public AudioClip EndMusic;

    public Text EndText;
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

        Player1.AllUnitsDied += EndGame;
        Player2.AllUnitsDied += EndGame;
    }

    private void Start()
    {
        gameState = GameState.StartCountDownState;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("Menu");

        switch (gameState)
        {
            case GameState.StartCountDownState:
                gameState = GameState.CountDownState;
                BeatManager.Instance.Beat.AddListener(CountDown);
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

    private void CountDown()
    {
        _count++;
    }

    /// <summary>
    /// Play this when the time is up.
    /// </summary>
    private void TimeUp()
    {
        gameState = GameState.EndState;
        EndText.transform.parent.gameObject.SetActive(true);
        _audioSource.clip = TimeUpMusic;
        _audioSource.Play();
    }

    /// <summary>
    /// Play this when an army has been defeated.
    /// </summary>
    private void EndGame(Player player)
    {
        gameState = GameState.EndState;
        EndText.fontSize = 100;
        EndText.text = player.name + " lost!";
        EndText.transform.parent.gameObject.SetActive(true);
        _audioSource.loop = false;
        _audioSource.clip = EndMusic;
        _audioSource.Play();
    }
}
