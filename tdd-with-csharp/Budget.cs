using System;

namespace tdd_with_csharp;

public class Budget
{
    public string YearMonth { get; set; }
    public int Amount { get; set; }

    public int GetDays()
    {
        var firstDay = GetFirstDay();
        return DateTime.DaysInMonth(firstDay.Year, firstDay.Month);
    }

    public DateTime GetLastDay()
    {
        return DateTime.ParseExact(YearMonth + GetDays(), "yyyyMMdd", null);
    }

    public DateTime GetFirstDay()
    {
        return DateTime.ParseExact(YearMonth, "yyyyMM", null);
    }

    public int GetOverlappingDays(DateTime start, DateTime end)
    {
        DateTime overlappingEnd;
        DateTime overlappingStart;
        if (YearMonth == start.ToString("yyyyMM"))
        {
            overlappingEnd = GetLastDay();
            overlappingStart = start;
        }
        else if (YearMonth == end.ToString("yyyyMM"))
        {
            overlappingEnd = end;
            overlappingStart = GetFirstDay();
        }
        else
        {
            overlappingEnd = GetLastDay();
            overlappingStart = GetFirstDay();
        }

        var overlappingDay = (overlappingEnd - overlappingStart).Days + 1;
        return overlappingDay;
    }
}