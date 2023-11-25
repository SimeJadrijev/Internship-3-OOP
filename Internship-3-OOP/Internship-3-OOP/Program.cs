using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_3_OOP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu();
            Console.ReadKey();
        }
        static int? Menu()
        {
            Console.WriteLine("- MENU - \n\n" +
                                " 1 - Ispis svih kontakata \n" +
                                " 2 - Dodavanje novih kontakata u imenik \n" +
                                " 3 - Brisanje kontakata iz imenika \n" +
                                " 4 - Editiranje pereference kontakata \n" +
                                " 5 - Upravljanje kontaktom koje otvara podmenu \n" +
                                " 6 - Ispis svih poziva \n" +
                                " 7 - Izlaz iz aplikacije \n" );

            int? actionChoice;
            do
            {
                Console.Write("Odaberite jednu od ponuđenih opcija: ");
                actionChoice = IntInput(Console.ReadLine());
            } while (actionChoice == null || actionChoice < 1 || actionChoice > 7);

            return actionChoice;
        }
        static int? IntInput(string input)
        {
            var result = false;
            int number = 0;

            result = int.TryParse(input, out number);

            if (result)
                return number;
            else
                return null;
        }
    }
}
