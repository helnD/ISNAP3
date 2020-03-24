using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public interface ISolver
    {
        (double, double, double) Solve(IReadOnlyCollection<Point> spots, 
            string function);
    }
}