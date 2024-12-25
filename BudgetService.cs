namespace BudgetTest;

public class BudgetService
{
    private readonly IBudgetRepo _budgetRepo;

    public BudgetService(IBudgetRepo budgetRepo)
    {
        _budgetRepo = budgetRepo;
    }

    public decimal Query(DateTime start, DateTime end)
    {
        if (start < end)
        {
            var currentMonth = end.ToString("yyyyMM");
            var budgets = _budgetRepo.GetAll();
            var budget = budgets.SingleOrDefault(budget => budget.YearMonth.Equals(currentMonth));

            return budget.Amount;
        }

        return 0;
    }
}