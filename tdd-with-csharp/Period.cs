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
        var firstDay = budget.GetFirstDay();
        var lastDay = budget.GetLastDay();
        
        var overlappingStart = Start > firstDay
            ? Start
            : firstDay;
        var overlappingEnd = End < lastDay 
            ? End
            : lastDay;

        return (overlappingEnd - overlappingStart).Days + 1;
    }
}