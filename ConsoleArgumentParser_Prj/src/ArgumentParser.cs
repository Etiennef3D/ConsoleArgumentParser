using System;
using System.Collections.Generic;

namespace ConsoleArgumentParser_Prj.src
{
    public class ArgumentParser
    {
        private string[] _arguments;
        private Dictionary<string, Action> _actions = new Dictionary<string, Action>();

        public ArgumentParser() { }

        public ArgumentParser(string[] _args)
        {
            this._arguments = _args;
        }

        public void AddArgument(string _initiator, Action _invocation)
        {
            Console.WriteLine($"Parsing value: {_initiator}, with invocation: {_invocation}.");

            //Checking if argument is not already set
            //If so set it an action
            if(_actions.TryGetValue(_initiator, out Action _myAction))
            {
                //Already set
                _actions[_initiator] = _invocation;
            }
            else
            {
                //Add an entry in the list
                _actions.Add(_initiator, _invocation);
            }
        }

        public void Parse(string[] _args)
        {

        }

    }
}