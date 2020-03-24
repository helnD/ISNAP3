using System.Collections.Generic;

namespace Domain.Entities
{
    public interface IRecorder
    {
        void RecordSource(IReadOnlyCollection<Point> source);
        void RecordNoise(IReadOnlyCollection<double> sourceWithNoise);
        void RecordParameters(double a, double b, double c);
        void RecordReceivedMessage(IReadOnlyCollection<double> receivedMessage);
    }
}