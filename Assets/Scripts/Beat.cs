using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum BeatType
{
    High,
    Mid,
    Low
};

public struct Beat
{
    public int Time;
    BeatType Type;
        
    public Beat(int time, BeatType type)
    {
            
        Time = time;
        Type = type;
    }

        
}
