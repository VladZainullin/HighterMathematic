namespace Corelation;

public sealed class Analitics
{
    private readonly IEnumerable<Selection> _selections;

    private Analitics(IEnumerable<Selection> selections) => _selections = selections;

    internal static Analitics CreateInstance(IEnumerable<Selection> selections)
    {
        return new Analitics(selections);
    }

    public int Count => _selections.Count();

    public decimal RСriteria
    {
        get
        {
            var indicators = 0m;
            var researchers = 0m;
            var multiply = 0m;

            _selections
                .ToList()
                .ForEach(selection =>
                {
                    var indicator = selection.Indicator - _selections
                        .Select(s => s.Indicator)
                        .Average();

                    var researcher = selection.Researcher - _selections
                        .Select(s => s.Researcher)
                        .Average();

                    indicators += indicator * indicator;
                    researchers += researcher * researcher;

                    multiply += indicator * researcher;
                });

            var numerator = Math.Math.Sqrt(indicators * researchers);

            return multiply / numerator;
        }
    }

    public decimal TСriteria =>
        RСriteria * Math.Math.Sqrt(Count - 2m) /
        Math.Math.Sqrt(1m - Math.Math.Power(RСriteria, 2m));

    public string Connection()
    {
        var result = RСriteria;

        return result switch
        {
            >= 0.0m and < 0.3m => "weak",
            >= 0.3m and < 0.5m => "moderate",
            >= 0.5m and < 0.7m => "notable",
            >= 0.7m and < 0.9m => "high",
            >= 0.9m and < 1m => "very high",
            1m => "absolute",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}