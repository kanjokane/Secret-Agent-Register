using System;
using System.Linq;
using static System.Console;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Threading;

namespace SecretAgent
{
    class Program
    {
        static void Main(string[] args)
        {
            bool applicationRunning = true;

            
            Agent[] agentArray = new Agent[0];
            int nextAgent = 0;


            do
            {
                CursorVisible = false;

                WriteLine("1. Add agent");
                WriteLine("2. List agents");
                WriteLine("3. Exit");

                ConsoleKeyInfo input;
                bool invalidChoice;

                do
                {
                    input = ReadKey(true);

                    invalidChoice = !(input.Key == ConsoleKey.D1 || input.Key == ConsoleKey.NumPad1
                        || input.Key == ConsoleKey.D2 || input.Key == ConsoleKey.NumPad2
                        || input.Key == ConsoleKey.D3 || input.Key == ConsoleKey.NumPad3);

                } while (invalidChoice);

                CursorVisible = true;

                Clear();

                switch (input.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        {

                            Write("First name: ");

                            string firstname = ReadLine();

                            Write("Last name: ");

                            string lastname = ReadLine();

                            Write("Social Security Number: ");

                            string ssn = ReadLine();

                            Write("Clearance (None, TopSecret, Cosmic): ");

                            var clear = (Clearance)Enum.Parse(typeof(Clearance), ReadLine(), true);

                            Agent newAgent = new Agent(firstName: firstname, lastName: lastname, socialSecurityNumber: ssn, clearance: clear);

                            
                            bool agentNotFound = true;

                            for (int i = 0; i < agentArray.Length; i++)
                            { 
                                if (agentArray[i] == null)
                                {

                                    continue;

                                }
                                else if (agentArray[i].socialsecuritynumber == ssn)
                                {

                                    WriteLine("Agent already registered!");
                                    Thread.Sleep(1000);
                                    agentNotFound = false;
                                    break;
                                }
                                
                            }
                            if (agentNotFound)
                            {
                                //Gör arrayen dynamisk
                                if (nextAgent >= agentArray.Length)  //Om antalet element överstiger Arrayens oföränderliga längd
                                {
                                    Agent[] extendArray = new Agent[agentArray.Length + 1]; //Skapar ny Array med den ursprungliga Arrayens längd plus en

                                    for (int i = 0; i < agentArray.Length; i++)
                                    {
                                        extendArray[i] = agentArray[i];//Loopar genom/kopierar varje element från ursprungliga till nya Arrayen
                                    }

                                    agentArray = extendArray;//Tilldelar den ursprungliga Arrayen, som den nya utan statisk längd.

                                }
                                agentArray[nextAgent++] = newAgent; //Lägger till nya agent som vanligt.

                                WriteLine("Agent registered");
                                Thread.Sleep(1000);
                            }

                            Clear();
                        }

                        break;

                    // List agents
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        {
                            /*for (int i = 0; i < agentArray.Length; i++)
                            {
                                WriteLine($"{agentArray[i].firstname} {agentArray[i].lastname}, {agentArray[i].clear}");
                            }*/
                            foreach (var agent in agentArray)
                            {
                                WriteLine($"{agent.firstname} {agent.lastname}: {agent.clear}");
                            }

                            ReadLine();
                        }

                        break;

                    case ConsoleKey.D3:
                        applicationRunning = false;
                        break;
                }

                Clear();

            } while (applicationRunning);
        }
    }
    class Agent // En "class" är en Referenstyp
    {
        
        // vi har här 4 fält
        public string firstname;
        public string lastname;
        public string socialsecuritynumber;
        public Clearance clear;
      

        public Agent(string firstName, string lastName, string socialSecurityNumber, Clearance clearance)
        {
            firstname = firstName;
            lastname = lastName;
            socialsecuritynumber = socialSecurityNumber;
            clear = clearance;

        }
    }
    
    enum Clearance
    {
        None,
        TopSecret,
        Cosmic
    }
}