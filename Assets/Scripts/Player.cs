using System;
using System.Collections.Generic;
using Assets.Scripts.Units;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(IRhythmInput))]
public class Player : MonoBehaviour
{
    [SerializeField] private Archers _archers;
    [SerializeField] private Horsemen _horsemen;
    [SerializeField] private Spearmen _spearmen;
    
    private IRhythmInput _rhythmInput;

    private void Awake()
    {
        _rhythmInput = GetComponent<IRhythmInput>();
        _rhythmInput.ValidInputMade += PerformAction;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("Menu");
    }

    private void PerformAction(UnitType unitType, ActionType actionType)
    {
        List<Unit> units = GetUnits(unitType);

        switch (actionType)
        {
            case ActionType.MoveForwards:
                foreach(Unit unit in units)
                    unit.MoveForward();
                break;
            case ActionType.MoveBackwards:
                foreach (Unit unit in units)
                    unit.MoveBackWards();
                break;
            case ActionType.Stay:
                foreach (Unit unit in units)
                    unit.StopMoving();
                break;
            case ActionType.Action:
                foreach (Unit unit in units)
                    unit.Attack();
                break;
        }
    }

    private List<Unit> GetUnits(UnitType unitType)
    {
        List<Unit> units = new List<Unit>();
        if((unitType & UnitType.Archers) == UnitType.Archers)
                units.Add(_archers);
        if ((unitType & UnitType.Horsemen) == UnitType.Horsemen)
                units.Add( _horsemen);
        if ((unitType & UnitType.Spearmen) == UnitType.Spearmen)
            units.Add(_spearmen);

        return units;
    }
}