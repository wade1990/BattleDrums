using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(AudioSource))]
    internal class Metronome : MonoBehaviour
    {
        [SerializeField] private AudioClip _MeterSound;
        [SerializeField] private AudioClip _BeatSound;

        private AudioSource _audioSource;

        private int _beeps;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            BeatManager.Instance.Beat.AddListener(Beep);
        }

        private void Beep()
        {
            _audioSource.clip = _beeps % 4 == 0 
                ? _MeterSound 
                : _BeatSound;

            _audioSource.Play();
            _beeps++;
        }
    }
}
