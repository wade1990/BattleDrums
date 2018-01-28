using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    internal class BeatManager : MonoBehaviour
    {
        /// <summary>
        /// The amount of beats per minute.
        /// </summary>
        [SerializeField] private int _beatsPerMinute;

        [SerializeField] private float _startupDelay;

        /// <summary>
        /// Static instance <see cref="BeatManager"/>.
        /// </summary>
        public static BeatManager Instance { get; private set; }

        /// <summary>
        /// Time between beats.
        /// </summary>
        public float BeatTime { get { return 60f / _beatsPerMinute; } }


        public float BPM { get { return _beatsPerMinute; } }

        /// <summary>
        /// Event thrown every beat.
        /// </summary>
        public UnityEvent Beat;
        public UnityEvent HalfTimeBeat;
        public UnityEvent QuarterTimeBeat;

        public UnityEvent Nextbeat;
        public UnityEvent NextHalfbeat;

        public UnityEvent NextMeasure;



        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Invoke("DropDaBeat", _startupDelay);
        }

        private void DropDaBeat()
        {
            StartCoroutine(OpDaBeat());
        }

        /// <summary>
        /// Invokes <see cref="Beat"/> after every intervalof <see cref="BeatTime"/>.
        /// </summary>
        /// <returns></returns>
        private IEnumerator OpDaBeat()
        {
            float expected = Time.time;
            while (true)
            {
                expected += BeatTime / 4;
                PlayBeat();
                yield return new WaitForSeconds(expected-Time.time);
                expected += BeatTime / 4;
                PlayQuarterBeat();
                yield return new WaitForSeconds(expected - Time.time);
                expected += BeatTime / 4;
                PlayHalfBeat();
                yield return new WaitForSeconds(expected - Time.time);
                expected += BeatTime / 4;
                PlayQuarterBeat();
                yield return new WaitForSeconds(expected - Time.time);
                Debug.Log(Time.time + "/" + expected);
            }
        }

        private void PlayBeat()
        {
            Beat.Invoke();
            InvokeAndClear(NextMeasure);
            InvokeAndClear(Nextbeat);
        }

        private void PlayHalfBeat()
        {
            HalfTimeBeat.Invoke();
            InvokeAndClear(NextMeasure);
            InvokeAndClear(NextHalfbeat);
        }

        private void PlayQuarterBeat()
        {
            QuarterTimeBeat.Invoke();
            InvokeAndClear(NextMeasure);
        }

        private static void InvokeAndClear(UnityEvent unityEvent)
        {
            unityEvent.Invoke();
            unityEvent.RemoveAllListeners();
        }
    }
}
