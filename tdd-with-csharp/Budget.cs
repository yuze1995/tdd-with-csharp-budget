using System;

namespace tdd_with_csharp;

public class Budget
{
    public string YearMonth { get; set; }
    public int Amount { get; set; }

    public int GetDays()
    {
        var date = DateTime.ParseExact(YearMonth, "yyyyMM", null);
        return DateTime.DaysInMonth(date.Year, date.Month);
    }
}