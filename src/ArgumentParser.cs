using System;
using System.Collections.Generic;

namespace ConsoleArgumentParser.src
{
    /// <summary>
    /// Main class of this little program.
    /// It makes sure that arguments are easy to add,
    /// and also check if they exists before they are invoked.
    /// Mostly works with Action/Anonymous method for simplicity.
    /// <para>Just add some arguments, parse,
    /// then it's done !</para>
    /// </summary>
    public static class ArgumentParser
    {
        private static Dictionary<string, Action> _actions = new Dictionary<string, Action>();

        public static bool IsValid { get; set; }

        /// <summary>
        /// Adding an argument to invoke it later. You can register any string you need.
        /// </summary>
        public static void AddArgument(string _initiator, Action _invocation)
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
        /// Check if the incoming string has the right format we want.
        /// Check according to CompareCheck enum options if the value correspond to the right format.
        /// </summary>
        /// <param name="_value">The value we want to be tested</param>
        /// <param name="_comparer">The comparer in the good format</param>
        public static void CheckArgumentFormat(string _value, string _comparer, ComparerCheck[] _checks = null)
        {
            if(_checks != null)
            {
                foreach(ComparerCheck _comp in _checks)
                {
                    if(_comp == ComparerCheck.Length && !CompareLength(_value, _comparer))
                    {
                        Console.WriteLine("Character length does not match.");
                        IsValid = false;
                        return;
                    }
                }
            }
        }

        private static bool CompareLength(string _v, string _c) =>_v.Length == _c.Length;

        /// <summary>
        /// If you want to remove an argument for whatever reason.
        /// </summary>
        /// <param name="_initiatorKey"></param>
        public static void RemoveArgument(string _initiatorKey)
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
        private static bool IsValueExists(string _key) => _actions.TryGetValue(_key, out Action _invocation);


        /// <summary>
        /// Check if the key value exists before invoking its value.
        /// </summary>
        public static void Execute(string[] _args)
        {
            foreach (string _s in _args)
            {
                //Execute existing action
                if (IsValueExists(_s))
                    _actions[_s]();
            }
        }
    }
    public enum ComparerCheck
    {
        /// <summary>
        /// Use character length if you wan't to test the exact same number of character
        /// </summary>
        Length = 0,
    }
}