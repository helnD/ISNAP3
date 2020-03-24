using System.Collections.Immutable;
using Domain.Entities;

namespace WebApplication.services
{
    public class DataTransferResult
    {
        public DataTransferResult(ImmutableList<Point> source, ImmutableList<double> sourceWithNoise, ImmutableList<double> receivedMessage, double a, double b, double c)
        {
            Source = source;
            SourceWithNoise = sourceWithNoise;
            ReceivedMessage = receivedMessage;
            A = a;
            B = b;
            C = c;
        }

        public ImmutableList<Point> Source { get; }
        public ImmutableList<double> SourceWithNoise { get; }
        public ImmutableList<double> ReceivedMessage { get; }
        
        public double A { get; }
        public double B { get; }
        public double C { get; }
    }
}