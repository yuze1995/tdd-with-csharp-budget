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
            var currentMonth = new DateTime(start.Year, start.Month, 1).AddMonths(1);
            var sum = 0;
            while (currentMonth < new DateTime(end.Year, end.Month, 1))
            {
                var budget = GetBudget(budgets, currentMonth.ToString("yyyyMM"));
                if (budget != null)
                {
                    sum += budget.Amount;
                }
                currentMonth = currentMonth.AddMonths(1);
            }


            var startMonthDays = DateTime.DaysInMonth(start.Year, start.Month);
            var startBudget = GetBudget(budgets, start.ToString("yyyyMM"));
            var startBudgetPerDay = startBudget?.Amount / startMonthDays ?? 0;
            var amountOfStart = startBudgetPerDay * (startMonthDays - start.Day + 1);

            var endMonthDays = DateTime.DaysInMonth(end.Year, end.Month);
            var endBudget = GetBudget(budgets, end.ToString("yyyyMM"));
            var endBudgetPerDay = endBudget?.Amount / endMonthDays ?? 0;
            var amountOfEnd = endBudgetPerDay * (end.Day);

            sum += amountOfStart + amountOfEnd;
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

    private static Budget? GetBudget(List<Budget> budgets, string yearMonth)
    {
        return budgets.FirstOrDefault(b => b.YearMonth == yearMonth);
    }
}