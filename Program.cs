using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Program
    {
        static void Main(string[] args)
        {

            /*
            Console.WriteLine("Available Ports:");
            foreach (string s in SerialPort.GetPortNames()) // Смотрим, какие СОМ порты доступны
            {
                Console.WriteLine("   {0}", s); 
            }
            */
            string portName = "COM" + portNumSetup(); // Запрашиваем и устанавливаем номер порта СОМ

            Console.WriteLine("Available IO speeds for {0} : 1200, 2400, 4800, 9600, 19200, 38400, 57600, 115200", portName);

            int portIOSpeed = portSpeedSetup(); // Запрашиваем и устанавливаем скорость СОМ порта

            Task.Factory.StartNew(() => StartListen(portName, portIOSpeed));
            Console.ReadLine();
        }
        public static void StartListen(string portName, int portIOSpeed)
        {
            using (SerialPort port = new SerialPort(portName, portIOSpeed))
            {
                Console.Write("Start connecting to {0} with speed {1} ...", portName, portIOSpeed);
                try
                {
                    port.Open(); // пытаемся открыть СОМ порт с установленными параметрами
                }
                catch (Exception e) // ловим ошибки при открытии СОМ порта
                {
                    Console.WriteLine("\nSomething went wrong:\n {0}", e); // не смогли открыть СОМ порт 
                    return ;
                }
                Console.WriteLine("OK");
                Console.WriteLine("Listening ...");


                while (true)
                {
                    if (port.BytesToRead != 0)
                    {
                        string result = port.ReadExisting(); // читаем, что исходит их СОМ порта
                        
                            Console.WriteLine("Received: {0}", result); // выводим то, что услышали
                       
                    }
                    Thread.Sleep(500);
                }
            }
        }

        public static string portNumSetup() // устанавливаем номер СОМ порта
        {
            Console.WriteLine("Available Ports:");
            foreach (string s in SerialPort.GetPortNames()) // Смотрим, какие СОМ порты доступны
            {
                Console.WriteLine("   {0}", s);
            }
            Console.Write("Please enter COM port number :");
            string portNum = Console.ReadLine();
            return portNum;
        }
        public static int portSpeedSetup() // устанавливаем скорость СОМ порта
        {
            Console.Write("Please enter IO speed for COM port number :");
            int portSpeed = Convert.ToInt32(Console.ReadLine());
            return portSpeed;
        }
    }
}