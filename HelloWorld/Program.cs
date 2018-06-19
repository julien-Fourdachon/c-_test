using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;


namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Veuillez saisir l'heure du bonjour");
            String result = ReadLine();
            Int32.TryParse(result, out int choice);

            
            WriteLine("Veuillez saisir l'heure du bon après");
            String result1 = ReadLine();
            Int32.TryParse(result1, out int choice1);

            WriteLine("Veuillez saisir l'heure du bonsoir");
            String result2 = ReadLine();
            Int32.TryParse(result, out int choice2);



            Message msg = new Message(choice, choice1, choice2);
            String message = msg.GetHelloMessage();

            String response = "";

            do
            {
                WriteLine(msg.message + " " + System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                WriteLine("Veuillez saisir \"exit\" pour sortir");
                response = ReadLine();
                            }

            while (!response.Equals("exit"));
                      
                

        }


    }
}
