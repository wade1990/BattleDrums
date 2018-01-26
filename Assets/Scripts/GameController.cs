using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private float _beatsPerMinute;

    [SerializeField] private List<Transform> _linePoints;

    [SerializeField] private Player _player1;
    [SerializeField] private Player _player2;

    private bool _finished;

    private void Awake()
    {
        StartCoroutine(Loop());
    }

    private IEnumerator Loop()
    {
        yield return InitialCountDown();

        while (!_finished)
        {
            yield return InputPhase();
            yield return ResolveActions();
        }
    }


    private object InitialCountDown()
    {
        throw new System.NotImplementedException();
    }


    private object InputPhase()
    {
        throw new System.NotImplementedException();
    }

    private object ResolveActions()
    {
        throw new System.NotImplementedException();
    }
}
