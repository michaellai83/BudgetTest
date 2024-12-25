﻿namespace BudgetTest;

public interface IBudgetRepo
{
    public List<Budget> GetAll();
}

public class Budget
{
    public string YearMonth { get; set; }
    public int Amount { get; set; }
}