using Domain.Entities;
using Domain.Entities.DataStructureRecorder;

namespace Domain.UseCase
{
    public class Interactor
    {
        
        private RangeBuilder _rangeBuilder;
        private Sender _sender;
        private Receiver _receiver;

        public Interactor()
        {
            _rangeBuilder = new RangeBuilder(Recorder);
            
            _sender = new Sender(Recorder);
            _receiver = new Receiver(Recorder);
        }
        
        public IRecorder Recorder { get; private set; } = new StructureRecorder();
        
        public void DataTransferProcess(double min, double max, int count, 
            double a, double b, double c, string function, double significanceLevel)
        {
            var range = _rangeBuilder.Min(min)
                .Max(max)
                .A(a)
                .B(b)
                .C(c)
                .Count(count)
                .Function(function)
                .Build();
            
            _sender.MessageIsSent += _receiver.Receive;
            
            _sender.SendMessage(range, function, significanceLevel);

        }
    }
}