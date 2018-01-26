using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private float _beatsPerMinute;

    [SerializeField] private List<Transform> _linePoints;

    [SerializeField] private Player _leftPlayer;
    [SerializeField] private Player _rightPlayer;
    
    private int _activeLineIndex;

    /// <summary>
    /// The time it takes for a single meter of 4 beats to play.
    /// </summary>
    private float MeterTime
    {
        get { return 4 * BeatTime; }
    }

    /// <summary>
    /// Duration of a single beat.
    /// </summary>
    private float BeatTime
    {
        get { return 60f / _beatsPerMinute; }
    }

    /// <summary>
    /// Wether the game is finished or not.
    /// </summary>
    private bool Finished
    {
        get { return _activeLineIndex == 0 || _activeLineIndex == _linePoints.Count - 1; }
    }
    
    private void Awake()
    {
        _activeLineIndex = _linePoints.Count / 2;

        StartCoroutine(Loop());
    }

    private IEnumerator Loop()
    {
        yield return InitialCountDown();

        while (!Finished)
        {
            yield return InputPhase();
            yield return ResolveActions();
        }
    }

    private IEnumerator InitialCountDown()
    {
        //Todo show a countdown.
        yield return new WaitForSeconds(MeterTime);
    }

    /// <summary>
    /// The phase that the players are inputting their rythm.
    /// </summary>
    private IEnumerator InputPhase()
    {
        _leftPlayer.StartReadingInput();
        _rightPlayer.StartReadingInput();

        yield return new WaitForSeconds(MeterTime);

        _leftPlayer.StopReadingInput();
        _rightPlayer.StopReadingInput();
    }

    /// <summary>
    /// The fase that the selected actions are resolved.
    /// </summary>
    /// <returns></returns>
    private IEnumerator ResolveActions()
    {
        //Get the selected units.
        Unit leftUnit = _leftPlayer.SelectedUnit;
        Unit rigtUnit = _rightPlayer.SelectedUnit;

        //Get the battle range and resole combat.
        Transform battleStartPoint = _linePoints[_activeLineIndex];
        _activeLineIndex += ResolveCombat(leftUnit, rigtUnit);
        Transform battleEndPoint = _linePoints[_activeLineIndex];

        //Perform the combat.
        _leftPlayer.DoCombat(battleStartPoint, battleEndPoint, MeterTime);
        _rightPlayer.DoCombat(battleStartPoint, battleEndPoint, MeterTime);

        //Wait.
        yield return new WaitForSeconds(MeterTime);
    }

    /// <summary>
    /// Resolves the combat.
    /// </summary>
    /// <param name="leftUnit">The unit chosen by <see cref="_leftPlayer"/>.</param>
    /// <param name="rightUnit">The unit chosen by <see cref="_rightPlayer"/>.</param>
    /// <returns>The change of the <see cref="_activeLineIndex"/> following this combat.</returns>
    private static int ResolveCombat(Unit leftUnit, Unit rightUnit)
    {
        if (leftUnit == rightUnit)
            return 0;

        return leftUnit == Unit.Horsemen && rightUnit == Unit.Archers || leftUnit < rightUnit 
            ? 1 
            : -1;
    }
}
