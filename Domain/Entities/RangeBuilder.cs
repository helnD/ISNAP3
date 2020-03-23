using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    internal class RangeBuilder
    {
        private double _min = 0;
        private double _max = 10;
        private int _count = 10;

        private string _function = "power";
        private double _a = 0.5;
        private double _b = 0.5;
        private double _c = 0.5;

        private readonly IRecorder _recorder;

        public RangeBuilder(IRecorder recorder)
        {
            _recorder = recorder;
        }

        internal RangeBuilder Min(double min)
        {
            _min = min;
            return this;
        }
        
        internal RangeBuilder Max(double max)
        {
            _max = max;
            return this;
        }
        
        internal RangeBuilder Count(int count)
        {
            _count = count;
            return this;
        }
        
        internal RangeBuilder Function(string function)
        {
            _function = function;
            return this;
        }
        
        internal RangeBuilder A(double a)
        {
            _a = a;
            return this;
        }
        
        internal RangeBuilder B(double b)
        {
            _b = b;
            return this;
        }
        
        internal RangeBuilder C(double c)
        {
            _c = c;
            return this;
        }

        internal IReadOnlyCollection<dynamic> Build()
        {
            var function = new FunctionDeterminant().GetFunction(_function);
            
            var h = (_max - _min) / (_count - 1);

            var spots = new List<dynamic>();

            for (var x = _min; x <= _max; x += h)
            {
                spots.Add(new
                {
                    X = x,
                    Y = function(x, _a, _b, _c)
                });
            }
            
            _recorder.RecordSource(spots);

            return spots;
        }
    }
}