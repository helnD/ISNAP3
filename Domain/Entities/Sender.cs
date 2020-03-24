using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Dynamic;
using System.Linq;

namespace Domain.Entities
{
    public class Sender
    {
        private readonly IRecorder _recorder;

        public Sender(IRecorder recorder)
        {
            _recorder = recorder;
        }
        
        public event EventHandler<SendMessageArgs> MessageIsSent; 

        public void SendMessage(IReadOnlyCollection<Point> spots, string function, double significanceLevel)
        {
            var noiseSource = new NoiseSource(significanceLevel);
            var onlyNoise = noiseSource.CreateNoise(spots.Select(it => it.X).ToList())
                .ToList();
            var spotsWithNoise = new List<Point>();
            var spotsList = spots.ToList();

            for (var index = 0; index < onlyNoise.Count; index++)
            {
                spotsWithNoise.Add(new Point(spotsList[index].X, onlyNoise[index] + spotsList[index].Y));
            }

            _recorder.RecordNoise(spotsWithNoise.Select(it => it.Y).ToList());

            MessageIsSent?.Invoke(this, new SendMessageArgs(spotsWithNoise, function));
        }

        public class SendMessageArgs : EventArgs
        {
            public SendMessageArgs(IReadOnlyCollection<Point> spots, string function)
            {
                Spots = spots;
                Function = function;
            }

            public IReadOnlyCollection<Point> Spots { get; }
            public string Function { get; }
        }
    }
}