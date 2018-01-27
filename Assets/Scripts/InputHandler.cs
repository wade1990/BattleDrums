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
public class InputHandler : MonoBehaviour, IRhythmInput {

    [SerializeField] Rhythm[] PatternList = { };
    public KeyToBeat[] ControlScheme = { new KeyToBeat(KeyCode.Z, Beat.High), new KeyToBeat(KeyCode.X, Beat.Mid), new KeyToBeat(KeyCode.C, Beat.Low) } ;

    private Beat beats = Beat.None;

    public event Action<UnitType, ActionType> ValidInputMade;

    // Use this for initialization
    void Start ()
    {
        BeatManager.Instance.HalfTimeBeat.AddListener(RunBeat);
        foreach (Rhythm rhythm in PatternList)
            rhythm.ValidInputMade += InputComplete;
	}
	
	// Update is called once per frame
	void Update ()
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
        ValidInputMade.Invoke(unit, action);
    }

    public void RunBeat()
    {
        foreach(Rhythm rhythm in PatternList)
        {
            rhythm.Match(beats);
        }
        beats = Beat.None;
    }
}
