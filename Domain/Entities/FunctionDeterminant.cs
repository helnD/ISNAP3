using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class FunctionDeterminant
    {
        private readonly Dictionary<string, Func<double, double, double, double, double>> _permissibleFunctions 
            = new Dictionary<string, Func<double, double, double, double, double>>
        {
            ["exponential"] = (x, a, b, c) => a + b * Math.Pow(x, c),
            ["quadratic"] = (x, a, b, c) => a + b * x + c * x * x,
            ["trigonometric"] = (x, a, b, c) => a + b * Math.Sin(x) + c * Math.Cos(x)
        };
        
        private readonly Exception _functionNotPermissible = new Exception("Данный тип функции не поддерживается");

        public Func<double, double, double, double, double> GetFunction(string function)
        {
            if (!_permissibleFunctions.ContainsKey(function))
                throw _functionNotPermissible;

            return _permissibleFunctions[function];
        }
    }
}