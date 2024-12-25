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
            var startYearMonth = start.ToString("yyyyMM");
            var endYearMonth = end.ToString("yyyyMM");
            var budgets = _budgetRepo.GetAll();
            var budget = budgets.SingleOrDefault(budget => budget.YearMonth.Equals(endYearMonth));
            
            if (budget == null)
            {
                return 0m;
            }

            if (startYearMonth == endYearMonth)
            {
                var days = (end - start).Days + 1;

                return  budget.Amount * days / DateTime.DaysInMonth(end.Year, end.Month);
            }
            
            
            

            return budget.Amount;
        }

        return 0;
    }
}