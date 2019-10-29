﻿using System;
using System.IO;

namespace Interface03
{
    interface ILogger
    {
        void WriteLog(string message);
    }

    class ConsoleLogger : ILogger
    {
        public void WriteLog(string message)
        {
            Console.WriteLine("{0}, {1}", DateTime.Now.ToLocalTime(), message);
        }
    }

    class FileLogger : ILogger
    {
        private StreamWriter writer;

        public FileLogger (string path)
        {
            writer = File.CreateText(path);
            writer.AutoFlush = true;
        }

        public void WriteLog(string message)
        {
            writer.WriteLine("{0}, {1}", DateTime.Now.ToLocalTime(), message);
        }
    }

    class ClimateMonitor
    {
        private ILogger logger;
        public ClimateMonitor(ILogger logger)
        {
            this.logger = logger;
        }

        public void start()
        {
            while(true)
            {
                Console.WriteLine("온도를 입력해주세요.");
                string temp = Console.ReadLine();
                if (temp == "")
                    break;

                logger.WriteLog("현재 온도 : " + temp);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // 콘솔에 로그 출력
            ClimateMonitor monitor = new ClimateMonitor(new ConsoleLogger());
            monitor.start();

            // 파일에 로그 출력
            ClimateMonitor fmonitor = new ClimateMonitor(new FileLogger("MyLog.txt"));
            fmonitor.start();
        }
    }
}
