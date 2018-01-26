using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _linePoints;

    [SerializeField] private PlayerPrefs _player1;
    [SerializeField] private PlayerPrefs _player2;

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
