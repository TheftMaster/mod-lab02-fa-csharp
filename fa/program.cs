using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fans
{
    public class State
    {
        public string Name;
        public Dictionary<char, State> Transitions;
        public bool IsAcceptState;
    }

    public class FA1
    {
        private State InitialState;
        private Dictionary<string, State> states;

        public FA1()
        {
            InitializeStates();
            InitializeTransitions();
            SetInitialState();
        }

        private void InitializeStates()
        {
            states = new Dictionary<string, State>();

            State a = new State()
            {
                Name = "a",
                IsAcceptState = false,
                Transitions = new Dictionary<char, State>()
            };

            State b = new State()
            {
                Name = "b",
                IsAcceptState = false,
                Transitions = new Dictionary<char, State>()
            };

            State c = new State()
            {
                Name = "c",
                IsAcceptState = false,
                Transitions = new Dictionary<char, State>()
            };

            State d = new State()
            {
                Name = "d",
                Transitions = new Dictionary<char, State>(),
                IsAcceptState = true
            };

            State e = new State()
            {
                Name = "e",
                Transitions = new Dictionary<char, State>(),
                IsAcceptState = false
            };

            states["a"] = a;
            states["b"] = b;
            states["c"] = c;
            states["d"] = d;
            states["e"] = e;
        }

        private void InitializeTransitions()
        {
            states["a"].Transitions['0'] = states["c"];
            states["a"].Transitions['1'] = states["b"];
            states["b"].Transitions['0'] = states["d"];
            states["b"].Transitions['1'] = states["b"];
            states["c"].Transitions['0'] = states["e"];
            states["c"].Transitions['1'] = states["d"];
            states["d"].Transitions['0'] = states["e"];
            states["d"].Transitions['1'] = states["d"];
            states["e"].Transitions['0'] = states["e"];
            states["e"].Transitions['1'] = states["e"];
        }

        private void SetInitialState()
        {
            InitialState = states["a"];
        }

        public bool? Run(IEnumerable<char> s)
        {
            State current = InitialState;
            foreach (var c in s)
            {
                if (current.Transitions.TryGetValue(c, out var nextState))
                {
                    current = nextState;
                }
                else
                {
                    return null;
                }
            }
            return current.IsAcceptState;
        }
    }

    public class FA2
    {
        private State InitialState;

        public FA2()
        {
            State a = new State()
            {
                Name = "a",
                IsAcceptState = false,
                Transitions = new Dictionary<char, State>()
            };

            State b = new State()
            {
                Name = "b",
                IsAcceptState = false,
                Transitions = new Dictionary<char, State>()
            };

            State c = new State()
            {
                Name = "c",
                IsAcceptState = false,
                Transitions = new Dictionary<char, State>()
            };

            State d = new State()
            {
                Name = "d",
                IsAcceptState = true,
                Transitions = new Dictionary<char, State>()
            };

            a.Transitions['0'] = b;
            a.Transitions['1'] = c;
            b.Transitions['0'] = a;
            b.Transitions['1'] = d;
            c.Transitions['0'] = d;
            c.Transitions['1'] = a;
            d.Transitions['0'] = c;
            d.Transitions['1'] = b;

            InitialState = a;
        }

        public bool? Run(IEnumerable<char> s)
        {
            State current = InitialState;
            foreach (var c in s)
            {
                current = current.Transitions[c];
                if (current == null)
                    return null;
            }
            return current.IsAcceptState;
        }
    }

    public class FA3
    {
        private State InitialState;

        public FA3()
        {
            State a = new State()
            {
                Name = "a",
                IsAcceptState = false,
                Transitions = new Dictionary<char, State>()
            };

            State b = new State()
            {
                Name = "b",
                IsAcceptState = false,
                Transitions = new Dictionary<char, State>()
            };

            State c = new State()
            {
                Name = "c",
                IsAcceptState = false,
                Transitions = new Dictionary<char, State>()
            };

            State d = new State()
            {
                Name = "d",
                IsAcceptState = true,
                Transitions = new Dictionary<char, State>()
            };

            a.Transitions['0'] = a;
            a.Transitions['1'] = b;
            b.Transitions['0'] = c;
            b.Transitions['1'] = b;
            c.Transitions['0'] = d;
            c.Transitions['1'] = b;
            d.Transitions['0'] = d;
            d.Transitions['1'] = d;

            InitialState = a;
        }

        public bool? Run(IEnumerable<char> s)
        {
            State current = InitialState;
            foreach (var c in s)
            {
                current = current.Transitions[c];
                if (current == null)
                    return null;
            }
            return current.IsAcceptState;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            String s = "0000010111";

            FA1 fa1 = new FA1();
            bool? result1 = fa1.Run(s);
            Console.WriteLine("FA1 result: " + result1);

            FA2 fa2 = new FA2();
            bool? result2 = fa2.Run(s);
            Console.WriteLine("FA2 result: " + result2);

            FA3 fa3 = new FA3();
            bool? result3 = fa3.Run(s);
            Console.WriteLine("FA3 result: " + result3);
        }
    }
}
