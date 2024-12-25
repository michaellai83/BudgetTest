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
        var result = _budgetService.Query(new DateTime(2024, 12, 2), new DateTime(2024, 12, 1));
        Assert.That(result, Is.EqualTo(0m));
    }
}