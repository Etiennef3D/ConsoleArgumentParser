using System;
using System.IO;
using ConsoleArgumentParser.src;

namespace ConsoleArgumentParser
{
    class Example
    {
        static void Main(string[] args)
        {
            //Just faking arguments for the sake of example
            //of course args are send as parameter when the console app
            //is launched
            args = new string[] { "-v", "-d" };

            //Just construct it the old way
            ArgumentParser _parser = new ArgumentParser();

            //Testing parameter -v for verbosity
            _parser.AddArgument("-v", () => { Console.SetOut(TextWriter.Null); });

            //If my previous arg was successfully registered,
            //nothing should print on the console
            _parser.AddArgument("-d", () => { Console.WriteLine("dddddddd"); });

            //Finally parse
            _parser.Parse(args);
        }
    }
}
