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

    public Period CreatePeriod()
    {
        return new Period(GetFirstDay(), GetLastDay());
    }
}