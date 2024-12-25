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
            var endYearMonth = end.ToString("yyyyMM");
            var budgets = _budgetRepo.GetAll();
            var budget = budgets.SingleOrDefault(budget => budget.YearMonth.Equals(endYearMonth));

            if (budget == null)
            {
                return 0m;
            }

            if (start.ToString("yyyyMM") == endYearMonth)
            {
                var days = (end - start).Days + 1;

                return (decimal)budget.Amount * days / DateTime.DaysInMonth(end.Year, end.Month);
            }

            var startAmount = GetStartOverlapAmount(start, budgets);
            var endAmount = GetEndOverlapAmount(end, budgets);


            return startAmount + endAmount;
        }

        return 0;
    }

    private static decimal GetEndOverlapAmount(DateTime end, List<Budget> budgets)
    {
        var endBudget = budgets.SingleOrDefault(budget => budget.YearMonth.Equals(end.ToString("yyyyMM")));
        if (endBudget == null)
        {
            return 0m;
        }
        return (decimal)endBudget.Amount * end.Day / DateTime.DaysInMonth(end.Year, end.Month);
    }

    private static decimal GetStartOverlapAmount(DateTime start, List<Budget> budgets)
    {
        var startBudget = budgets.SingleOrDefault(budget => budget.YearMonth.Equals(start.ToString("yyyyMM")));
        if (startBudget == null)
        {
            return 0m;
        }
        var overlapOfStart = DateTime.DaysInMonth(start.Year, start.Month) - start.Day + 1;
        var startAmount = (decimal)startBudget.Amount * overlapOfStart / DateTime.DaysInMonth(start.Year, start.Month);
        return startAmount;
    }
}