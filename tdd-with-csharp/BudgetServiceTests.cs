using System;
using System.Collections.Generic;
using System.Globalization;
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
    [Test]
    public void TwoDayBudgetCrossMonths()
    {
        var budgetRepo = Substitute.For<IBudgetRepo>();
        var budgetService = new BudgetService(budgetRepo);
        budgetRepo.GetAll().Returns(new List<Budget>()
        {
            new Budget()
            {
                YearMonth = "202302",
                Amount = 2800

            },
            new Budget()
            {
                YearMonth = "202303",
                Amount = 31000
            }
        });
        var result = budgetService.Query(new DateTime(2023,2,28),new DateTime(2023,3,2));
        Assert.AreEqual(2100, result);
    }
    
}