using UnityEngine;

[RequireComponent(typeof(IRythmInput))] internal class Player
public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _archers;
    [SerializeField] private GameObject _horsemen;
    [SerializeField] private GameObject _spearmen;

    [SerializeField] private GameObject _middle;

    [SerializeField] private Unit _chosenUnit;

    [SerializeField] private float speed;

    public Unit SelectedUnit { get; private set; }

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

    public void DoCombat(Transform battleStartpoint, Transform battleEndPoint, float duration)
    {

    }

    public void StartReadingInput()
    {

    }

    public void StopReadingInput()
    {

}

