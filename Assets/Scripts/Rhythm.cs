using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[System.Serializable]
public class Rhythm : IRhythmInput
{
    public UnitType Unit;
    public ActionType Action;
    public event Action<UnitType, ActionType> ValidInputMade;

    public Beat Filter;

    public Beat[] Pattern;

    private int _currentBeat = -1;
    private bool _broken;

    public Rhythm(UnitType unit, ActionType action, Beat[] pattern, Beat filter = Beat.All)
    {
        Unit = unit;
        Action = action;
        Pattern = pattern;
        Filter = filter;
    }

    public bool Match(Beat beat)
    {
        //Check if we started a new bar.
        _currentBeat++;
        if (_currentBeat == Pattern.Count())
            Reset();

        //Return if already broken.
        if (_broken)
            return false;

        //Match the current beat.
        if((Pattern[_currentBeat] & Filter) != (beat & Filter))
        {
            _broken = true;
            return false;
        }

        //Check if pattern is completed.
        if(Matched())
        {
            Complete();
        }

        return true;
    }

    private void Complete()
    {
        ValidInputMade.Invoke(Unit, Action);
    }

    public bool Broken()
    {
        return _broken;
    }

    public int GetCurrentBeat()
    {
        return _currentBeat;
    }

    public bool Matched()
    {
        return !_broken && (_currentBeat == Pattern.Count() - 1);
    }

    public void Reset()
    {
        _broken = false;
        _currentBeat = 0;
    }
}