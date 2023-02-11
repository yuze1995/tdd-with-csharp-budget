using System.Collections.Generic;

namespace tdd_with_csharp;

public interface IBudgetRepo
{
    List<Budget> GetAll();
}