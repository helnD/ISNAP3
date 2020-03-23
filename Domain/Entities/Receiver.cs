using System.Collections.Generic;
using System.Linq;
using Domain.Entities.DataStructureRecorder;

namespace Domain.Entities
{
    public class Receiver
    {
        private readonly ISolver _solver = new PythonSolver();
        private readonly IRecorder _recorder;

        public Receiver(IRecorder recorder)
        {
            _recorder = recorder;
        }

        public void Receive(object sender, Sender.SendMessageArgs args)
        {
            var spots = args.Spots;
            var function = args.Function;
            
            var (a, b, c) = _solver.Solve(spots, function);
            
            var lambdaFunction = new FunctionDeterminant().GetFunction(function);
            
            var receivedMessage = spots.Select(spot => lambdaFunction(spot.X, a, b, c))
                .Cast<double>()
                .ToList();
            
            _recorder.RecordReceivedMessage(receivedMessage);
        }
    }
}