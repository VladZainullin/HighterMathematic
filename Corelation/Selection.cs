namespace Corelation;

public struct Selection
{
    public Selection(decimal researcher, decimal indicator)
    {
        Researcher = researcher;
        Indicator = indicator;
    }

    public decimal Researcher { get; }
    
    public decimal Indicator { get; }
}