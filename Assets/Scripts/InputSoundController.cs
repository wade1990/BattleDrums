using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(InputHandler))]
internal class InputSoundController : MonoBehaviour
{
    [SerializeField] private InputSoundPlayer _lowBeatPlayer;
    [SerializeField] private InputSoundPlayer _midBeatPlayer;
    [SerializeField] private InputSoundPlayer _highBeatPlayer;

    private void Start()
    {
        InputHandler inputHandler = GetComponent<InputHandler>();
        inputHandler.BeatMade += OnTheBeat;
    }

    private void OnTheBeat(Beat beat)
    {
        InputSoundPlayer player;

        switch (beat)
        {
            case Beat.Low:
                player = _lowBeatPlayer;
                break;
            case Beat.Mid:
                player = _midBeatPlayer;
                break;
            case Beat.High:
                player = _highBeatPlayer;
                break;
            default:
                return;
        }

        player.PlayRandomClip();
    }


    [Serializable]
    private class InputSoundPlayer
    {
        [SerializeField] private List<AudioClip> _clips;
        [SerializeField] private AudioSource _source;

        public void PlayRandomClip()
        {
            AudioClip clip = _clips[Random.Range(0, _clips.Count)];
            _source.clip = clip;
            _source.Play();
        }
    }
}
