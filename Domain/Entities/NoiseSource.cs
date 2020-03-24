using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public class NoiseSource
    {
        private readonly double _significanceLevel;

        public NoiseSource(double significanceLevel)
        {
            _significanceLevel = significanceLevel;
        }

        public IReadOnlyCollection<double> CreateNoise(IReadOnlyCollection<double> sourceData)
        {
            var average = sourceData.Average();
            var random = new Random();

            var topNoise = sourceData.Select(it => random.NextDouble() * _significanceLevel * average).ToList();
            var lowNoise = sourceData.Select(it => -random.NextDouble() * _significanceLevel * average).ToList();

            var result = topNoise.Select((value, index) =>
            {
                var noise = Math.Abs(value) < Math.Abs(lowNoise[index])
                    ? value
                    : lowNoise[index];

                var newElement = noise;
                return newElement;
            });
            
            return result.ToList();
        }
    }
}