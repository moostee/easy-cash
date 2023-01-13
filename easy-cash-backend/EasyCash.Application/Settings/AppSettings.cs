namespace EasyCash.Application.Settings;

public class AppSettings
{
    public string Secret { get; set; } 

    public int MinimumLoanAmount { get; set; }
    public int LoanLimit { get; set; }
    public double LoanPercentage { get; set; }
}