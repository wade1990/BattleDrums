using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Matcher : IRhythmInput
{
    public UnitType Unit;
    public ActionType Action;

    public Beat Filter = Beat.All;

    public Rhythm Pattern;

    private int _currentBeat;
    private bool _broken;

    public bool Match(Beat beat)
    {
        //Check if we started a new bar.
        _currentBeat++;
        if (_currentBeat == Pattern.sequence.Count())
            Reset();

        //Return if already broken.
        if (_broken)
            return false;

        //Match the current beat.
        if((Pattern.sequence[_currentBeat] & Filter) != (beat & Filter))
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
        ValidInputMade();
    }

    public bool Broken()
    {
        return _broken;
    }

    public bool Matched()
    {
        return !_broken && (_currentBeat == Pattern.sequence.Count() - 1);
    }

    public void Reset()
    {
        _broken = false;
        _currentBeat = 0;
    }
}