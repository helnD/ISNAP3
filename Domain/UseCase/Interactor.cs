using Domain.Entities;
using Domain.Entities.DataStructureRecorder;

namespace Domain.UseCase
{
    public class Interactor
    {
        
        public IRecorder Recorder { get; private set; }
        
        public void DataTransferProcess(double min, double max, int count, 
            double a, double b, double c, string function, double significanceLevel)
        {
            Recorder = new StructureRecorder();
            
            var rangeBuilder = new RangeBuilder(Recorder);
            var range = rangeBuilder.Min(min)
                .Max(max)
                .A(a)
                .B(b)
                .C(c)
                .Count(count)
                .Function(function)
                .Build();
            
            var sender = new Sender(new NoiseSource(significanceLevel), Recorder);
            var receiver = new Receiver(Recorder);

            sender.MessageIsSent += receiver.Receive;
            
            sender.SendMessage(range, function);

        }
    }
}