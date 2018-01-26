using UnityEngine;

[RequireComponent(typeof(IRythmInput))] internal class Player
{
    public Unit SelectedUnit { get; private set; }

    public void StartReadingInput()
    {

    }

    public void StopReadingInput()
    {

    }

    public void DoCombat(Transform battleStartpoint, Transform battleEndPoint, float duration)
    {

    }
}
