using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System;

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
public class InputHandler : MonoBehaviour, IRhythmInput
{
    [SerializeField]
    bool _toneToUnit;

    [SerializeField]
    Rhythm[] PatternList = { };

    public KeyToBeat[] ControlScheme = { new KeyToBeat(KeyCode.Z, Beat.High), new KeyToBeat(KeyCode.X, Beat.Mid), new KeyToBeat(KeyCode.C, Beat.Low) };

    private Beat beats = Beat.None;

    public event Action<UnitType, ActionType> ValidInputMade;

    // Use this for initialization
    void Start()
    {
        BeatManager.Instance.HalfTimeBeat.AddListener(RunBeat);


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
        foreach (KeyToBeat pair in ControlScheme)
        {
            if (Input.GetKeyDown(pair.key))
            {
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
        foreach (Rhythm rhythm in PatternList)
        {
            rhythm.Match(beats);
        }
        beats = Beat.None;
    }
}
