using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _archers;
    [SerializeField] private GameObject _horsemen;
    [SerializeField] private GameObject _spearmen;

    [SerializeField] private Unit _chosenUnit;

    [SerializeField] private float speed;

    public Unit SelectedUnit { get; private set; }

    public void DoCombat(Transform battleStartpoint, Transform battleEndPoint, float duration)
    {

    }

    public void StartReadingInput()
    {

    }

    public void StopReadingInput()
    {

    }
}