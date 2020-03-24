using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Domain.Entities.DataStructureRecorder
{
    public class StructureRecorder : IRecorder
    {
        public ImmutableList<Point> Source { get; private set; }
        public ImmutableList<double> SourceWithNoise { get; private set; }
        public ImmutableList<double> ReceivedMessage { get; private set; }
        
        public double A { get; private set; }
        public double B { get; private set; }
        public double C { get; private set; }
        public double E { get; private set; }
        public double Ea { get; private set; }
        public double Eb { get; private set; }
        public double Ec { get; private set; }

        public void RecordSource(IReadOnlyCollection<Point> source)
        {
            Source = source.ToImmutableList();
        }

        public void RecordNoise(IReadOnlyCollection<double> sourceWithNoise)
        {
            SourceWithNoise = sourceWithNoise.ToImmutableList();
        }

        public void RecordParameters(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
        }

        public void RecordErrors(double e, double ea, double eb, double ec)
        {
            E = e;
            Ea = ea;
            Eb = eb;
            Ec = ec;
        }

        public void RecordReceivedMessage(IReadOnlyCollection<double> receivedMessage)
        {
            ReceivedMessage = receivedMessage.ToImmutableList();
        }
    }
}