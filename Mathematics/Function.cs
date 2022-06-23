namespace Mathematics
{
    public delegate double TransmittedFunction(double x);

    public sealed class Function
    {
        public Function(TransmittedFunction function)
        {
            _function = new TransmittedFunction(function);
        }

        // Method Trapezoid
        public double DefiniteIntegral(in double a, in double b, in double accuracy)
        {
            var summa = 0.0;
            for (var i = a; i < b - accuracy; i += accuracy)
            {
                summa += (_function(i) + _function(i + accuracy));
            }
            return summa / 2 * accuracy;
        }
        
        // Method Rectangle
        public double DefiniteIntegral(in double a, in double b, in uint countSplitting)
        {
            var summa = 0.0;
            var h = Math.Abs(b - a) / countSplitting;
            for (var i = a; i <= countSplitting; i++)
            {
                summa += _function(a + i * h - h / 2);
            }
            return summa * h;
        }

        // Method Simpson
        public double DefiniteIntegralMethodSimpson(in double a, in double b, in uint countSplitting)
        {
            var width = Math.Abs(b - a) / countSplitting;
            var summa = 0.0;
            for (var i = 0; i < countSplitting; i++)
            {
                var buffer = a + i * width;
                summa += width * (_function(buffer) + 4.0 * _function(buffer + width / 2) + _function(buffer + width));
            }
            return summa / 6.0;
        }

        private readonly TransmittedFunction _function;
    }
}