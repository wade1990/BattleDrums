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

        public UnityEvent Nextbeat;
        public UnityEvent NextHalfbeat;

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
            while (true)
            {
                Beat.Invoke();
                InvokeAndClear(Nextbeat);
                yield return new WaitForSeconds(BeatTime / 2);

                HalfTimeBeat.Invoke();
                InvokeAndClear(NextHalfbeat);
                yield return new WaitForSeconds(BeatTime / 2);
            }
        }

        private static void InvokeAndClear(UnityEvent unityEvent)
        {
            unityEvent.Invoke();
            unityEvent.RemoveAllListeners();
        }
    }
}
