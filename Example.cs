using System;
using System.IO;
using ConsoleArgumentParser.src;

namespace ConsoleArgumentParser
{
    class Example
    {
        static void Main(string[] args)
        {
            //Just faking arguments for the sake of example.
            args = new string[] { "-v", "-d" };

            //Testing parameter -v for verbosity
            ArgumentParser.AddArgument("-d", SayHello);
            ArgumentParser.AddArgument("-v", RemoveVerbosity);

            //Finally parse
            ArgumentParser.Parse(args);
        }

        private static void RemoveVerbosity() => Console.SetOut(TextWriter.Null);
        private static void SayHello() => Console.WriteLine("Hello !");
    }
}