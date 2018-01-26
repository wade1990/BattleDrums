using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Rhythm
{
    /// <summary>
    /// List of Beats.
    /// </summary>
    private List<Beat> _sequence;   
  
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sequence">The sequence </param>
    public Rhythm(List<Beat> sequence)
    {
        _sequence = sequence;
    }

    /// <summary>
    /// List of intervals, alternating on and off.
    /// </summary>
    public List<Beat> Sequence
    {
        get { return _sequence; }
    }
    
    public void Append(Beat beat)
    {
        if (_sequence == null)
            _sequence = new List<Beat>();
        Sequence.Add(beat);
    }

    /// <summary>
    /// Compares two sequences
    /// </summary>
    /// <returns>The error</returns>
    public float Compare(Rhythm other)
    {
        float similarity = 0;


        return 0;
    }

    /* TODO
    public static Rhythm operator *(Rhythm rhythm, float speed)
    {

    }
    */
}
