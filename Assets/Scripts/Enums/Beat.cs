using System;

/// <summary>
/// Different types of beats.
/// </summary>
[Flags]
public enum Beat : int
{
    None = 0,
    Low = 1,
    Mid = 2,
    High = 4,
    All = int.MaxValue
};