using System;
using UnityEngine;

public class MockInput : MonoBehaviour, IRhythmInput
{
    [SerializeField] private UnitType _unit;
    [SerializeField] private ActionType _action;

    [SerializeField] private int _mouseButton;

    public event Action<UnitType, ActionType> ValidInputMade;

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButtonDown(_mouseButton) && ValidInputMade != null)
            ValidInputMade.Invoke(_unit, _action);
    }
}
