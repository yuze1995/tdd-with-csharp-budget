using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace tdd_with_csharp;

public class BudgetService
{
    private IBudgetRepo _budgetRepo;

    public BudgetService(IBudgetRepo budgetRepo)
    {
        _budgetRepo = budgetRepo;
    }

    public double Query(DateTime start, DateTime end)
    {
        var budgets = _budgetRepo.GetAll();

        var startYearMonth = start.ToString("yyyyMM");
        var endYearMonth = end.ToString("yyyyMM");
        var startMonthDays = DateTime.DaysInMonth(start.Year, start.Month);
        var endMonthDays = DateTime.DaysInMonth(end.Year, end.Month);
        
        if (startYearMonth != endYearMonth)
        {
            var nextMonthFirst = new DateTime(start.Year, start.Month + 1, 1);
            var sum = 0;
            while (nextMonthFirst.Year <= end.Year && nextMonthFirst.Month < end.Month)
            {
                var budget = GetBudget(budgets, $"{nextMonthFirst:yyyyMM}");
                sum += budget.Amount;
                nextMonthFirst = nextMonthFirst.AddMonths(1);
            }
            
            var startBudget = GetBudget(budgets, startYearMonth);
            var endBudget = GetBudget(budgets, endYearMonth);

            var startBudgetPerDay = startBudget.Amount / startMonthDays;
            var endBudgetPerDay = endBudget.Amount / endMonthDays;

            sum += startBudgetPerDay * (startMonthDays - start.Day + 1) + endBudgetPerDay * (end.Day);
            return sum;
        }
        
        var firstOrDefault = GetBudget(budgets, startYearMonth);
        var amount = firstOrDefault.Amount;
        var amountPerDay = amount / startMonthDays;
        return amountPerDay;
    }

    private static Budget? GetBudget(List<Budget> budgets, string yearMonth)
    {
        return budgets.FirstOrDefault(b => b.YearMonth == yearMonth);
    }
}