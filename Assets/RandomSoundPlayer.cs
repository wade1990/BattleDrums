using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
internal class RandomSoundPlayer : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _possibleClips;

    private AudioSource _audioSource;

    private AudioClip _previousClip;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomClip()
    {
        AudioClip clip;

        if (_possibleClips.Count == 1)
            clip = _possibleClips.First();
        else
        {
            _possibleClips.Remove(_previousClip);
            clip = _possibleClips[Random.Range(0, _possibleClips.Count)];

            _possibleClips.Add(_previousClip);
        }

        _audioSource.clip = clip;
        _audioSource.Play();

        _previousClip = clip;
    }
}