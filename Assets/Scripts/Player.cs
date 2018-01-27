using System;
using UnityEngine;

[RequireComponent(typeof(IRhythmInput))]
public class Player : MonoBehaviour
{
    [SerializeField] private Unit _archers;
    [SerializeField] private Unit _horsemen;
    [SerializeField] private Unit _spearmen;
    
    private IRhythmInput _rhythmInput;

    private void Awake()
    {
        _rhythmInput = GetComponent<IRhythmInput>();
        _rhythmInput.ValidInputMade += PerformAction;
    }

    private void PerformAction(UnitType unitType, ActionType actionType)
    {
        Unit unit = GetUnit(unitType);

        switch (actionType)
        {
            case ActionType.MoveForwards:
                unit.MoveForward();
                break;
            case ActionType.MoveBackwards:
                unit.MoveBackWards();
                break;
            case ActionType.Stay:
                unit.StopMoving();
                break;
            case ActionType.Action:
                unit.Attack();
                break;
        }
    }

    private Unit GetUnit(UnitType unitType)
    {
        switch (unitType)
        {
            case UnitType.Archers:
                return _archers;
            case UnitType.Horsemen:
                return _horsemen;
            case UnitType.Spearman:
                return _spearmen;
        }

        throw new ArgumentOutOfRangeException();
    }
}