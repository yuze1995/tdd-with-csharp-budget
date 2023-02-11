using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NSubstitute;
using NUnit.Framework;

namespace tdd_with_csharp;

[TestFixture]
public class BudgetServiceTests
{
    [Test]
    public void OneDayBudget()
    {
        var budgetRepo = Substitute.For<IBudgetRepo>();
        var budgetService = new BudgetService(budgetRepo);
        budgetRepo.GetAll().Returns(new List<Budget>()
        {
            new Budget()
            {
                YearMonth = "202302",
                Amount = 2800

            }
        });
        var result = budgetService.Query(new DateTime(2023,2,2),new DateTime(2023,2,2));
        Assert.AreEqual(100, result);
    }
    
}

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
        var firstOrDefault = budgets.Where(b => b.YearMonth == $"{start:yyyyMM}").FirstOrDefault();
        var amount = firstOrDefault.Amount;
        var amountPerDay = amount / DateTime.DaysInMonth(start.Year,start.Month);
        return amountPerDay;
    }
}

public interface IBudgetRepo
{
    List<Budget> GetAll();
}

public class Budget
{
    public string YearMonth { get; set; }
    public int Amount { get; set; }
}