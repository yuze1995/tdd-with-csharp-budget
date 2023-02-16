#region

using System;

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
        foreach (var budget in budgets)
        {
            var period = new Period(start, end);
            sum += budget.GetOverlappingAmount(period);
        }

        return sum;
    }
}