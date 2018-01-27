using System;

internal interface IRhythmInput
{
    event Action<UnitType, ActionType> ValidInputMade;
}
