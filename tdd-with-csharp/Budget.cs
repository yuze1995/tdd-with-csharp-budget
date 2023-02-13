using System;

namespace tdd_with_csharp;

public class Budget
{
    public string YearMonth { get; set; }
    public int Amount { get; set; }

    public int GetDays()
    {
        _ = DateOnly.TryParseExact(YearMonth, "yyyyMM", out var firstDay);
        return DateTime.DaysInMonth(firstDay.Year, firstDay.Month);
    }

    public DateTime GetLastDay()
    {
        return DateTime.ParseExact(YearMonth + GetDays(), "yyyyMMdd", null);
    }
}