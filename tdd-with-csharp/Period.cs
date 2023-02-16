using System;

namespace tdd_with_csharp;

public class Period
{
    public Period(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }

    public DateTime Start { get; private set; }
    public DateTime End { get; private set; }

    public int GetOverlappingDays(Period another)
    {
        var overlappingEnd = End < another.End
            ? End
            : another.End;
        var overlappingStart = Start > another.Start
            ? Start
            : another.Start;
        
        return (overlappingEnd - overlappingStart).Days + 1;
    }
}