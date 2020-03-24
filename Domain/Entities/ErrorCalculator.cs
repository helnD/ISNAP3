using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public class ErrorCalculator
    {
        public double E(List<double> source, List<double> @fixed)
        {
            var q = 0.0;

            for (var index = 0; index < source.Count; index++)
            {
                q += Math.Pow(source[index] - @fixed[index], 2);
            }

            var e = Math.Sqrt(q / (source.Count - 3)) / source.Max(Math.Abs);

            return e;
        }

        public double ECoefficient(double source, double @fixed) 
            => Math.Abs((source - @fixed) / source);
    }
}