using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public AudioClip GameMusic;
    public AudioClip EndMusic;

    private AudioSource _audioSource;
    private float _musicLength;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

	// Use this for initialization
	void Start ()
    {
		
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("Menu");
    }
}
