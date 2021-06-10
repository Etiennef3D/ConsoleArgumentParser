using System;
using System.Collections.Generic;

namespace ConsoleArgumentParser
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

        public static bool IsValid = true;

        /// <summary>
        /// Adding an argument to invoke it later. You can register any string you need.
        /// </summary>
        public static void AddArgument(string _initiator, Action _invocation)
        {
            if (!IsValid) return;
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
        /// Out value is used to get the parsed value in the wanted format.
        /// </summary>
        /// <param name="_value">The value we want to be tested</param>
        /// <param name="_comparer">The comparer in the good format</param>
        public static void CheckArgumentFormat<T>(string _value, string _comparer, out T _valueType, ComparerCheck[] _checks = null)
        {
            if(_checks != null)
            {
                foreach(ComparerCheck _comp in _checks)
                {
                    if(_comp == ComparerCheck.Length && !CompareLength(_value, _comparer))
                    {
                        Console.WriteLine("Character length does not match.");
                        IsValid = false;
                        break;
                    }
                    if (_comp == ComparerCheck.Type && !CompareValueType<T>(_value, out _valueType))
                    {
                        Console.WriteLine($"Type not valid. Required format: {typeof(T)}.");
                        IsValid = false;
                        break;
                    }
                }
            }
            _valueType = default;
        }

        private static bool CompareValueType<T>(string _v, out T _valueType)
        {
            //Get value type
            _valueType = _v.ChangeType<T>();
            if (_valueType is ushort) return ushort.TryParse(_v, out ushort us);
            if (_valueType is short) return short.TryParse(_v, out short _s);
            if (_valueType is uint) return uint.TryParse(_v, out uint _ui);
            if (_valueType is int) return int.TryParse(_v, out int _i);
            if (_valueType is ulong) return ulong.TryParse(_v, out ulong _ul);
            if (_valueType is long) return long.TryParse(_v, out long _l);
            if (_valueType is string) return true;
            return false;
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
            Console.WriteLine("Successfully parsed all arguments. Invoking actions.");
            foreach (string _s in _args)
            {
                //Execute existing action
                if (IsValueExists(_s))
                    _actions[_s]();
            }
        }

        public static T ChangeType<T>(this object obj)
        {
            return (T)Convert.ChangeType(obj, typeof(T));
        }
    }
    public enum ComparerCheck
    {
        /// <summary>
        /// Use character length if you wan't to test the exact same number of character
        /// </summary>
        Length = 0,

        /// <summary>
        /// Check if the out keyword is of the same type 
        /// </summary>
        Type = 1,
    }
}