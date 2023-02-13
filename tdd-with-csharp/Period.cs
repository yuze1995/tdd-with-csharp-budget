using System;

namespace tdd_with_csharp;

public class Period
{
    public Period(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }

    private DateTime Start { get; }
    private DateTime End { get; }

    public int GetOverlappingDays(Budget budget)
    {
        var another = new Period(budget.GetFirstDay(), budget.GetLastDay());

        var overlappingStart = Start > another.Start
            ? Start
            : another.Start;
        var overlappingEnd = End < another.End 
            ? End
            : another.End;

        return (overlappingEnd - overlappingStart).Days + 1;
    }
}