//Jose Angulo

using System;
using System.Numerics;

namespace Default
{
    //State Object with their delcared transitions
    public class state
    {
        public string _name;
        public List<state> _transitions = new List<state>();

        public state(string name)
        {
            _name = name;
        }

        public void addTransition(state newState)
        {
            _transitions.Add(newState);
        }
    }
    //A Simple Program
    public class DFA
    {
        public List<state> _states = new List<state>();
        public state _startState;
        public List<string> _acceptanceStates;

        public DFA(List<List<string>> relationships, string startState, List<string> acceptanceStates)
        {
            //Adds to list of states
            foreach (var item in relationships)
            {
                _states.Add(new state(item[0]));
            }

            //Finds the starting state
            foreach (var item in _states)
            {
                if (item._name == startState)
                    _startState = item;
            }

            //adds all the transitions for each state to remember
            for (int i = 0; i < _states.Count; i++)
            {
                for (int j = 1; j <= _states.Count; j++)
                {
                    foreach (var item in _states)
                    {
                        if (item._name == relationships[i][j])
                        {
                            _states[i].addTransition(item);
                        }
                            
                    }
                    
                }
            }
            //list of acceptance states

            _acceptanceStates = acceptanceStates;
        }

        public void simulation(string input)
        {
            //changes the string input chars for inputting single digits
            char[] inputs = input.ToCharArray();

            //current state
            state myState = _startState;

            //Cylces through each state transition
            foreach (var item in inputs)
            {
                myState = myState._transitions[item % 48]; //Converting char to int
            }

            //if the current state is valid it prints accepted

            for (int i = 0; i < _acceptanceStates.Count; i++)
            {
                if (myState._name == _acceptanceStates[i])
                {
                    Console.WriteLine("String accepted");
                    return;
                }
            }
            //if current state wasn't valid it prints not accepted

            Console.WriteLine("String not accepted");

        }

    }
    class Program
    {

        static void Main(string[] args)
        {
            //transitions user must make sure they are of the format
            //List<List<string>> myString = newList<List<string>> { new List<string> {string1, string2, string3}, .. }
            List<List<string>> relationship = new List<List<string>>
            {
                new List<string> { "q1", "q1", "q2"},
                new List<string> { "q2", "q1", "q2"}
            };
            string startState = "q1";
            List<string> acceptanceStates = new List<string>{"q1"};

            //Initializing DFA
            DFA myDfa = new DFA(relationship, startState, acceptanceStates);

            /*1. 10101
            2. 0010
            3. 0010100
            4. 1000
            5.  (the empty string, some people use the letter E for that)*/

            string test1 = "10101";
            string test2 = "0010";
            string test3 = "0010100";
            string test4 = "1000";
            string test5 = "";

            //Test 1
            Console.Write($"{test1} : ");
            myDfa.simulation(test1);

            //Test 2
            Console.Write($"{test2} : ");
            myDfa.simulation(test2);

            //Test 3
            Console.Write($"{test3} : ");
            myDfa.simulation(test3);

            //Test 4
            Console.Write($"{test4} : ");
            myDfa.simulation(test4);

            //Test 5
            Console.Write($"{test5} : ");
            myDfa.simulation(test5);


            //Extra Credit

            Console.WriteLine("::EXTRA CREDIT::");





        }
    }
}