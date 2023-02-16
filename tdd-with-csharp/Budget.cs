using System;

namespace tdd_with_csharp;

public class Budget
{
    public string YearMonth { get; init; } = null!;
    public int Amount { get; init; }

    private int GetDays()
    {
        var firstDay = GetFirstDay();
        return DateTime.DaysInMonth(firstDay.Year, firstDay.Month);
    }

    private DateTime GetLastDay() => DateTime.ParseExact($"{YearMonth}{GetDays()}", "yyyyMMdd", null);

    private DateTime GetFirstDay() => DateTime.ParseExact(YearMonth, "yyyyMM", null);

    public int GetOverlappingAmount(Period period) => GetDailyAmount() * period.GetOverlappingDays(CreatePeriod());

    private Period CreatePeriod() => new Period(GetFirstDay(), GetLastDay());

    private int GetDailyAmount() => Amount / GetDays();
}