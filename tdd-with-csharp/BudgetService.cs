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
                    if (currentMonth.ToString("yyyyMM") == start.ToString("yyyyMM"))
                    {
                        var startBudgetPerDay = budget?.Amount / startMonthDays ?? 0;
                        var startBudgetAmount = startBudgetPerDay * (startMonthDays - start.Day + 1);
                        sum += startBudgetAmount;
                    }
                    else if(currentMonth.ToString("yyyyMM") == end.ToString("yyyyMM"))
                    {
                        var endBudgetPerDay = budget?.Amount / endMonthDays ?? 0;
                        var endBudgetAmount = endBudgetPerDay * (end.Day);
                        sum += endBudgetAmount;
                    }
                    else
                    {
                        sum += budget.Amount;
                    }
                }

                currentMonth = currentMonth.AddMonths(1);
            }

            // var startBudget = GetBudget(budgets, start.ToString("yyyyMM"));
            // var startBudgetPerDay = startBudget?.Amount / startMonthDays ?? 0;
            // var startBudgetAmount = startBudgetPerDay * (startMonthDays - start.Day + 1);
            // sum += startBudgetAmount;

            // var endBudget = GetBudget(budgets, end.ToString("yyyyMM"));
            // var endBudgetPerDay = endBudget?.Amount / endMonthDays ?? 0;
            // var endBudgetAmount = endBudgetPerDay * (end.Day);
            // sum += endBudgetAmount;

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