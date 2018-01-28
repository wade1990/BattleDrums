using Assets.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public struct KeyToBeat
{
    public KeyCode key;
    public Beat beat;

    public KeyToBeat(KeyCode key, Beat beat)
    {
        this.key = key;
        this.beat = beat;
    }
}

/// <summary>
/// Controller Controller
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class InputHandler : MonoBehaviour, IRhythmInput
{
    [SerializeField]
    bool _toneToUnit;
    
    public Rhythm[] PatternList;

    public KeyToBeat[] ControlScheme = { new KeyToBeat(KeyCode.Z, Beat.High), new KeyToBeat(KeyCode.X, Beat.Mid), new KeyToBeat(KeyCode.C, Beat.Low) };

    private Beat beats = Beat.None;

    public event Action<UnitType, ActionType> ValidInputMade;
    public event Action<Beat> BeatMade;

    private GameController _gameController;

    // Use this for initialization
    void Start()
    {
        _gameController = GameObject.FindObjectOfType<GameController>();

        BeatManager.Instance.QuarterTimeBeat.AddListener(RunBeat);


        // If we pick one unit per tone
        if (_toneToUnit)
        {
            List<Rhythm> rhythms = new List<Rhythm>();
            foreach (Rhythm baseRhythm in PatternList)
            {
                if ((baseRhythm.Unit & UnitType.Archers) == UnitType.Archers)
                    rhythms.Add(new Rhythm(UnitType.Archers, baseRhythm.Action, baseRhythm.Pattern, (Beat)UnitType.Archers));

                if ((baseRhythm.Unit & UnitType.Horsemen) == UnitType.Horsemen)
                    rhythms.Add(new Rhythm(UnitType.Horsemen, baseRhythm.Action, baseRhythm.Pattern, (Beat)UnitType.Horsemen));

                if ((baseRhythm.Unit & UnitType.Spearmen) == UnitType.Spearmen)
                    rhythms.Add(new Rhythm(UnitType.Spearmen, baseRhythm.Action, baseRhythm.Pattern, (Beat)UnitType.Spearmen));
            }

            PatternList = rhythms.ToArray();
        }

        //Attach listener to all patterns.
        foreach (Rhythm rhythm in PatternList)
            rhythm.ValidInputMade += InputComplete;
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameController.gameState != GameController.GameState.PlayState)
            return;
        foreach (KeyToBeat pair in ControlScheme)
        {
            if (Input.GetKeyDown(pair.key))
            {
                if (BeatMade != null)
                    BeatMade.Invoke(pair.beat);

                beats |= pair.beat;
            }
        }
    }


    private void InputComplete(UnitType unit, ActionType action)
    {
        if (ValidInputMade != null)
            ValidInputMade.Invoke(unit, action);
    }

    public void RunBeat()
    {
        if (_gameController.gameState != GameController.GameState.PlayState)
            return;

        foreach (Rhythm rhythm in PatternList)
        {
            rhythm.Match(beats);
        }
        beats = Beat.None;
    }
}
