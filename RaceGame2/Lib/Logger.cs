using System;
using System.IO;

namespace RaceGame2.Lib
{
    public static class Logger
    {
        public static void Info(String lines)
        {
            lines = "Info: \r\n" + lines;
            Log(lines);
        }
        public static void Error(String lines)
        {
            lines = "Error: \r\n" + lines;
            Log(lines);
        }
        public static void Debug(String lines)
        {
            lines = "Debug: \r\n" + lines;
            Log(lines);
        }

        private static void Log(String lines)
        {
            // Write the string to a file.append mode is enabled so that the log
            // lines get appended to  test.txt than wiping content and writing the log

            System.IO.StreamWriter file = new System.IO.StreamWriter(Path.Combine(Environment.CurrentDirectory,"logs.txt"),true);
            file.WriteLine(lines);

            file.Close();
        }
    }
}