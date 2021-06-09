using System;
using System.Collections.Generic;

namespace ConsoleArgumentParser.src
{
    public class ArgumentParser
    {
        private Dictionary<string, Action> _actions = new Dictionary<string, Action>();

        public ArgumentParser() { }

        /// <summary>
        /// Adding an argument to invoke it later. You can register any string you need.
        /// </summary>
        public void AddArgument(string _initiator, Action _invocation)
        {
            //Checking if argument is not already set
            //If so set it an action
            if (IsValueExists(_initiator))
            {
                _actions[_initiator] = _invocation;
                Console.WriteLine("Argument successfully replaced.");
            }
            else
            {
                //Add an entry in the dictionnary
                _actions.Add(_initiator, _invocation);
                Console.WriteLine("Argument successfully added.");
            }
        }


        /// <summary>
        /// If you want to remove an argument for whatever reason.
        /// </summary>
        /// <param name="_initiatorKey"></param>
        public void RemoveArgument(string _initiatorKey)
        {
            if (IsValueExists(_initiatorKey))
            {
                _actions.Remove(_initiatorKey);
                Console.WriteLine("Argument successfully removed.");
            }
            else
            {
                Console.WriteLine("Key not recognized.");
            }
        }


        /// <summary>
        /// Simplify access to test a key in the dictionnary.
        /// </summary>
        /// <param name="_key"></param>
        /// <returns></returns>
        private bool IsValueExists(string _key) => _actions.TryGetValue(_key, out Action _invocation) ? true : false;


        /// <summary>
        /// Check if the key value exists before invoking its value.
        /// </summary>
        public void Parse(string[] _args)
        {
            foreach (string _s in _args)
            {
                if (IsValueExists(_s))
                {
                    //Execute existing action
                    _actions[_s]();
                }
            }
        }
    }
}