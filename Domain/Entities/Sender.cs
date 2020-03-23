using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Domain.Entities
{
    public class Sender
    {
        private readonly NoiseSource _noiseSource;
        private readonly IRecorder _recorder;

        public Sender(NoiseSource noiseSource, IRecorder recorder)
        {
            _noiseSource = noiseSource;
            _recorder = recorder;
        }
        
        public event EventHandler<SendMessageArgs> MessageIsSent; 

        public void SendMessage(IReadOnlyCollection<dynamic> spots, string function)
        {
            var spotsWithNoise = new List<dynamic>();

            foreach (var spot in spotsWithNoise)
            {
                spotsWithNoise.Add(new
                {
                    X = spot.X,
                    Y = _noiseSource.CreateNoise(spot.Y)
                });
            }
            
            var onlyNoise = spotsWithNoise.Select(spot => spot.X)
                .Cast<double>()
                .ToList();

            _recorder.RecordNoise(onlyNoise);

            MessageIsSent?.Invoke(this, new SendMessageArgs(spots, function));
        }

        public class SendMessageArgs : EventArgs
        {
            public SendMessageArgs(IReadOnlyCollection<dynamic> spots, string function)
            {
                Spots = spots;
                Function = function;
            }

            public IReadOnlyCollection<dynamic> Spots { get; }
            public string Function { get; }
        }
    }
}