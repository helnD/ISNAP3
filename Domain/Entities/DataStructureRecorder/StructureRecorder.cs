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

        public void RecordReceivedMessage(IReadOnlyCollection<double> receivedMessage)
        {
            ReceivedMessage = receivedMessage.ToImmutableList();
        }
    }
}