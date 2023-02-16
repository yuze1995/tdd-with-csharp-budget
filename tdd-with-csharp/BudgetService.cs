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

        var startMonthDays = DateTime.DaysInMonth(start.Year, start.Month);
        var endMonthDays = DateTime.DaysInMonth(end.Year, end.Month);

        if (start.ToString("yyyyMM") != end.ToString("yyyyMM"))
        {
            var currentMonth = new DateTime(start.Year, start.Month, 1);
            var sum = 0;
            while (currentMonth < new DateTime(end.Year, end.Month, 1).AddMonths(1))
            {
                var budget = GetBudget(budgets, currentMonth.ToString("yyyyMM"));
                if (budget != null)
                {
                    DateTime overlappingEnd;
                    if (currentMonth.ToString("yyyyMM") == start.ToString("yyyyMM"))
                    {
                        overlappingEnd = budget.GetLastDay();
                        var overlappingStart = start;
                        var overlappingDays = (overlappingEnd - overlappingStart).Days + 1;
                        var dailyAmount = budget.Amount / budget.GetDays();
                        sum += dailyAmount * overlappingDays;
                    }
                    else if (currentMonth.ToString("yyyyMM") == end.ToString("yyyyMM"))
                    {
                        overlappingEnd = end;
                        var overlappingStart = budget.GetFirstDay();
                        var overlappingDays = (overlappingEnd - overlappingStart).Days + 1;
                        var dailyAmount = budget.Amount / budget.GetDays();
                        sum += dailyAmount * overlappingDays;
                    }
                    else
                    {
                        overlappingEnd = budget.GetLastDay();
                        var overlappingStart = budget.GetFirstDay();
                        var overlappingDays = (overlappingEnd - overlappingStart).Days + 1;
                        var dailyAmount = budget.Amount / budget.GetDays();
                        sum += dailyAmount * overlappingDays;
                    }
                }

                currentMonth = currentMonth.AddMonths(1);
            }

            return sum;
        }

        var oneMonthBudget = GetBudget(budgets, start.ToString("yyyyMM"));
        if (oneMonthBudget == null) return 0;

        var amount = oneMonthBudget.Amount;
        var amountPerDay = amount / startMonthDays;
        return amountPerDay * ((end - start).Days + 1);
    }

    private static Budget? GetBudget(List<Budget> budgets, string yearMonth)
    {
        return budgets.FirstOrDefault(b => b.YearMonth == yearMonth);
    }
}