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
        var overlappingStart = Start > budget.GetFirstDay()
            ? Start
            : budget.GetFirstDay();
        var overlappingEnd = End < budget.GetLastDay() 
            ? End
            : budget.GetLastDay();

        return (overlappingEnd - overlappingStart).Days + 1;
    }
}