using Domain.Entities.DataStructureRecorder;
using Domain.UseCase;

namespace WebApplication.services
{
    public class LocalDataTransfer : IDataTransferProcess
    {
        private readonly Interactor _interactor = new Interactor();
        
        public DataTransferResult DataTransferProcess(double min, double max, int count, 
            double a, double b, double c, string function, double significanceLevel)
        {
            _interactor.DataTransferProcess(min, max, count, a, b, c, function, significanceLevel);

            var rec = _interactor.Recorder as StructureRecorder;
            
            var result = new DataTransferResult(rec.Source, rec.SourceWithNoise, rec.ReceivedMessage,
                rec.A, rec.B, rec.C, rec.E, rec.Ea, rec.Eb, rec.Ec);

            return result;
        }
    }
}