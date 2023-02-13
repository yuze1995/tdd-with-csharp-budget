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

        var sum = 0;
        var period = new Period(start, end);

        foreach (var budget in budgets)
        {
            var overlappingAmount = budget.GetOverlappingAmount(period);
            sum += overlappingAmount;
        }
        
        return sum;
    }

    private static Budget? GetBudget(List<Budget> budgets, string yearMonth) => budgets.FirstOrDefault(b => b.YearMonth == yearMonth);
}