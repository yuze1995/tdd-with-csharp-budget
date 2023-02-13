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

    public int GetOverlappingDays(Budget budget)
    {
        DateTime overlappingEnd;
        DateTime overlappingStart;
        if (budget.YearMonth == Start.ToString("yyyyMM"))
        {
            overlappingEnd = budget.GetLastDay();
            overlappingStart = Start;
        }
        else if (budget.YearMonth == End.ToString("yyyyMM"))
        {
            overlappingEnd = End;
            overlappingStart = budget.GetFirstDay();
        }
        else
        {
            overlappingEnd = budget.GetLastDay();
            overlappingStart = budget.GetFirstDay();
        }

        var overlappingDay = (overlappingEnd - overlappingStart).Days + 1;
        return overlappingDay;
    }
}