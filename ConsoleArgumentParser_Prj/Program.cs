using System;
using ConsoleArgumentParser_Prj.src;

namespace ConsoleArgumentParser_Prj
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ArgumentParser _parser = new ArgumentParser(args);

            _parser.AddArgument("-v", () => { Console.WriteLine("Coucou"); });
            _parser.AddArgument("-d", () => { Console.WriteLine("dddddddd"); });

            _parser.Parse();
        }
    }
}
