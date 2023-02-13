#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace tdd_with_csharp;

public class BudgetService
{
    private readonly IBudgetRepo _budgetRepo;

    public BudgetService(IBudgetRepo budgetRepo)
    {
        _budgetRepo = budgetRepo;
    }

    public double Query(DateTime start, DateTime end)
    {
        var budgets = _budgetRepo.GetAll();


        if (start.ToString("yyyyMM") != end.ToString("yyyyMM"))
        {
            var currentMonth = start;
            var sum = 0;
            while (currentMonth < new DateTime(end.Year, end.Month, 1).AddMonths(1))
            {
                var budget = GetBudget(budgets, currentMonth.ToString("yyyyMM"));

                if (budget != null)
                {
                    var overlappingDay = GetOverlappingDays(new Period(start, end), budget);
                    var dailyAmount = budget.Amount / budget.GetDays();
                    sum += dailyAmount * overlappingDay;
                }

                currentMonth = currentMonth.AddMonths(1);
            }

            return sum;
        }
        else
        {
            var oneMonthBudget = GetBudget(budgets, start.ToString("yyyyMM"));
            if (oneMonthBudget == null) return 0;

            var startMonthDays = DateTime.DaysInMonth(start.Year, start.Month);
            var amount = oneMonthBudget.Amount;
            var amountPerDay = amount / startMonthDays;

            return amountPerDay * ((end - start).Days + 1);
        }
    }

    private static int GetOverlappingDays(Period period, Budget budget)
    {
        DateTime overlappingEnd;
        DateTime overlappingStart;
        if (budget.YearMonth == period.Start.ToString("yyyyMM"))
        {
            overlappingEnd = budget.GetLastDay();
            overlappingStart = period.Start;
        }
        else if (budget.YearMonth == period.End.ToString("yyyyMM"))
        {
            overlappingEnd = period.End;
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

    private static Budget? GetBudget(List<Budget> budgets, string yearMonth)
    {
        return budgets.FirstOrDefault(b => b.YearMonth == yearMonth);
    }
}