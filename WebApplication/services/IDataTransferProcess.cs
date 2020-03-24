namespace WebApplication.services
{
    public interface IDataTransferProcess
    {
        DataTransferResult DataTransferProcess(double min, double max, int count, 
            double a, double b, double c, string function, double significanceLevel);
    }
}