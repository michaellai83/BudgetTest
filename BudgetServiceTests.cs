using NSubstitute;

namespace BudgetTest;

[TestFixture]
public class BudgetServiceTests
{
    private IBudgetRepo? _budgetRepo;
    private BudgetService _budgetService;

    [SetUp]
    public void Setup()
    {
        _budgetRepo = Substitute.For<IBudgetRepo>();
        _budgetService = new BudgetService(_budgetRepo);
    }

    [Test]
    public void InvalidQuery()
    {
        BudgetShouldBe(new DateTime(2024, 12, 2), new DateTime(2024, 12, 1), 0m);
    }

    [Test]
    public void Query_full_month()
    {
        _budgetRepo.GetAll().Returns(new List<Budget>()
        {
            new Budget
            {
                YearMonth = "202412",
                Amount = 31000
            }
        });

        BudgetShouldBe(new DateTime(2024, 12, 1), new DateTime(2024, 12, 31), 31000m);
    }
    
    [Test]  
    public void Query_partial_month()
    {
        _budgetRepo.GetAll().Returns(new List<Budget>()
        {
            new Budget
            {
                YearMonth = "202412",
                Amount = 31000
            }
        });

        BudgetShouldBe(new DateTime(2024, 12, 1), new DateTime(2024, 12, 2), 2000m);
    }

    private void BudgetShouldBe(DateTime start, DateTime end, decimal expected)
    {
        var result = _budgetService.Query(start, end);
        Assert.That(result, Is.EqualTo(expected));
    }
}