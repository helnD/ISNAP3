using System.Collections.Immutable;
using Domain.Entities;

namespace WebApplication.services
{
    public class DataTransferResult
    {
        public DataTransferResult(ImmutableList<Point> source, ImmutableList<double> sourceWithNoise, ImmutableList<double> receivedMessage, double a, double b, double c, double e, double ea, double eb, double ec)
        {
            Source = source;
            SourceWithNoise = sourceWithNoise;
            ReceivedMessage = receivedMessage;
            A = a;
            B = b;
            C = c;
            E = e;
            Ea = ea;
            Eb = eb;
            Ec = ec;
        }

        public ImmutableList<Point> Source { get; }
        public ImmutableList<double> SourceWithNoise { get; }
        public ImmutableList<double> ReceivedMessage { get; }
        
        public double A { get; }
        public double B { get; }
        public double C { get; }
        
        public double E { get; }
        public double Ea { get; }
        public double Eb { get; }
        public double Ec { get; }
    }
}