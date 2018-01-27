using System;

[Flags]
public enum Beat : int
{
    None = 0,
    High = 1,
    Mid = 2,
    Low = 4,
    All = int.MaxValue
};