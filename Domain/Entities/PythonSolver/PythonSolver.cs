using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PythonSolver : ISolver
    {
        private readonly ProcessStartInfo _processStartInfo = new ProcessStartInfo
        {
            FileName = "C:\\Users\\1\\AppData\\Local\\Programs\\Python\\Python38-32\\python.exe",
            Arguments = "C:\\Users\\1\\RiderProjects\\ISNAP3\\Domain\\Entities\\PythonSolver\\estimate_function.py"
        };
        
        private Socket _socket;
        
        public PythonSolver()
        {
            Process.Start(_processStartInfo);
            SocketSetupAsync();
        }
        
        public (double, double, double) Solve(IReadOnlyCollection<Point> spots, string function)
        {
            var argumentsBuilder = new StringBuilder(function + '|');
            argumentsBuilder.AppendJoin(' ', spots.Select(it => it.X));
            argumentsBuilder.Remove(argumentsBuilder.Length - 1, 1);
            argumentsBuilder.Append('|');
            argumentsBuilder.AppendJoin(' ', spots.Select(it => it.Y));
            argumentsBuilder.Remove(argumentsBuilder.Length - 1, 1);

            var arguments = argumentsBuilder.ToString();
            
            while (!_socket.Connected) Thread.Sleep(100);

            var encoding = new UTF8Encoding();
            
            var buffer = encoding.GetBytes(arguments);
            _socket.Send(buffer);
            
            buffer = new byte[256];
            _socket.Receive(buffer);
            
            var strParameters = encoding.GetString(buffer)
                .Replace('.', ',')
                .Split(' ');

            var a = double.Parse(strParameters[0]);
            var b = double.Parse(strParameters[1]);
            var c = double.Parse(strParameters[2]);

            return (a, b, c);
        }

        private void SocketSetupAsync()
        {
            var endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5264);
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var task = new Task(() =>
            {
                while (!_socket.Connected)
                {
                    try
                    {
                        _socket.Connect(endPoint);
                    }
                    catch
                    {
                        // ignored
                    }
                }
            });
            task.Start();
        }
    }
}