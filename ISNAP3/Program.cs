using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ISNAP3
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = 2;
            var b = 3;
            var c = 1.5;
            
            var min = 1;
            var max = 8;
            var n = 10;
            var k = 0.1;

            var h = (double)(max - min) / (n - 1);

            var x = new List<double>();
            var y = new List<double>();

            for (var i = (double)min; i <= max; i += h)
            {
                x.Add(i);
                y.Add(Function(i, a, b, c));
            }
            
            var topNoise = new List<double>();
            var lowNoise = new List<double>();

            for (var i = (double)min; i <= max; i += h)
            {
                topNoise.Add(CreateNoise(k, y));
                lowNoise.Add(-CreateNoise(k, y));
            }

            var yWithNoise = new List<double>();

            for (var i = 0; i < y.Count; i++)
            {
                var noise = Math.Abs(topNoise[i]) < Math.Abs(lowNoise[i]) 
                    ? topNoise[i] 
                    : lowNoise[i];

                var newElement = y[i] + noise;
                yWithNoise.Add(newElement);
            }

            var abc = GetParams(x, y);
            
            Console.WriteLine($"{abc.Item1}, {abc.Item2}, {abc.Item3}");

            foreach (var cx in x)
            {
                Console.WriteLine(abc.Item1 + abc.Item2 * Math.Pow(cx, abc.Item3));
            }
        }

        static double Function(double x, double a, double b, double c) =>
            a + b * Math.Pow(x, c);
        
        static double CreateNoise(double k, List<double> list) =>
            new Random().NextDouble() * k * list.Average();

        static (double, double, double) GetParams(List<double> xs, List<double> ys)
        {
            var arguments = new StringBuilder();
            xs.ForEach(it => arguments.Append((float)it + " "));
            arguments.Remove(arguments.Length - 1, 1);
            arguments.Append('|');
            ys.ForEach(it => arguments.Append((float)it + " "));
            arguments.Remove(arguments.Length - 1, 1);
            arguments.Replace(',', '.');

            var info = new ProcessStartInfo();

            var a = 0.0;
            var b = 0.0;
            var c = 0.0;

            var filename = "C:\\Users\\1\\RiderProjects\\ISNAP3\\ISNAP3\\power_function.py";
            
            info.FileName = "C:\\Users\\1\\AppData\\Local\\Programs\\Python\\Python38-32\\python.exe";
            info.Arguments = filename + " \"" + arguments + "\"";

            Process.Start(info);

            var port = 5264;

            var endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                socket.Bind(endPoint);
                socket.Listen(10);

                var handler = socket.Accept();

                var buffer = new byte[256];

                handler.Receive(buffer);
                
                handler.Close();
                
                var strParameters = new UTF8Encoding().GetString(buffer).Split(' ');

                a = double.Parse(strParameters[0].Replace('.', ','));
                b = double.Parse(strParameters[1].Replace('.', ','));
                c = double.Parse(strParameters[2].Replace('.', ','));
            }
            catch (IOException e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

            return (a, b, c);
        }
    }
}