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
            args = new string[] { "12345", "-v", "-d" };

            //Checking if the first argument we give has the same number of character
            //ArgumentParser.CheckArgumentFormat(args[0], "00000", new ComparerCheck[] { ComparerCheck.Length });

            //Testing parameter -v for verbosity
            ArgumentParser.AddArgument("-d", SayHello);
            ArgumentParser.AddArgument("-v", RemoveVerbosity);

            //We don't want to go further is there is an error
            if (!ArgumentParser.IsValid) return;

            //Finally parse
            ArgumentParser.Execute(args);
        }

        private static void RemoveVerbosity() => Console.SetOut(TextWriter.Null);
        private static void SayHello() => Console.WriteLine("Hello !");
    }
}