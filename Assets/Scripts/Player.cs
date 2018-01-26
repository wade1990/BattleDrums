using System;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _archers;
    [SerializeField] private GameObject _horsemen;
    [SerializeField] private GameObject _spearmen;

    [SerializeField] private GameObject _middle;

    [SerializeField] private Unit _chosenUnit;

    [SerializeField] private float speed;

    private void Update()
    {
        switch (_chosenUnit)
        {
            case Unit.Archers:
                MoveUnit(_archers);
                break;
            case Unit.Horsemen:
                MoveUnit(_horsemen);
                break;
            case Unit.Spearman:
                MoveUnit(_spearmen);
                break;
            case Unit.None:
                break;
        }
    }

    private void MoveUnit(GameObject unit)
    {
        unit.transform.position = Vector3.MoveTowards(unit.transform.position, _middle.transform.position, speed);
    }

    public void StartReadingInput()
    {

    }

    public void StopReadingInput()
    {

    }


}

