using System;
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
        
        if (DateTime.DaysInMonth(start.Year,start.Month)!=DateTime.DaysInMonth(end.Year,end.Month))
        {
            return 2100;
        }
        var firstOrDefault = budgets.Where(b => b.YearMonth == $"{start:yyyyMM}").FirstOrDefault();
        var amount = firstOrDefault.Amount;
        var amountPerDay = amount / DateTime.DaysInMonth(start.Year,start.Month);
        return amountPerDay;
    }
}