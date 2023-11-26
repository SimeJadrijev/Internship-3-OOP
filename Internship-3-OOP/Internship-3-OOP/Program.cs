using System;
using System.Collections.Concurrent;
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
            Dictionary<Contact, List<Call>> dictionary = new Dictionary<Contact, List<Call>>() { };

            int? actionChoice = Menu();
            while (actionChoice != null )
            {
                switch (actionChoice)
                {
                    case 1:
                        
                        while (true)
                        {
                            if (dictionary.Count != 0)
                                PrintAllContacts(dictionary);
                            else
                                Console.WriteLine("Lista je prazna!");

                            if (BackToMenu() == 0)
                            {
                                Console.Clear();
                                actionChoice = Menu();
                                break;
                            }

                        }

                        break;

                    case 2:
                        while (true)
                        {
                            AddNewContact(dictionary);

                            if (BackToMenu() == 0)
                            {
                                Console.Clear();
                                actionChoice = Menu();
                                break;
                            }
                        }
                        
                        break;

                    case 3:
                        while (true)
                        {
                            DeleteContact(dictionary);
                            if (BackToMenu() == 0)
                            {
                                Console.Clear();
                                actionChoice = Menu();
                                break;
                            }
                        }
                        break;

                    /*
                    case 7:
                        Environment.Exit(0);    //Odkomentirati kasnije
                        break;
                    */
                }
            }

            Console.ReadKey();
        }
        static void DeleteContact(Dictionary<Contact, List<Call>> dictionary)
        {
            Console.WriteLine();
            var contactIsFound = false;
            while (!contactIsFound)
            {
                Console.Write("Unesite ime i prezime kontakta koji želite izbrisati: ");
                var contactForDeletion = Console.ReadLine();

                Contact contactToRemove = dictionary.Keys.FirstOrDefault(contact => contact.NameAndSurname == contactForDeletion);

                foreach (var key in dictionary.Keys)
                {
                    if (key.NameAndSurname == contactForDeletion)
                    {
                        contactIsFound = true;
                        break;
                    }
                }

                if (contactToRemove != null)
                {
                    dictionary.Remove(contactToRemove);
                    contactIsFound = true;
                    Console.WriteLine("Uspješno ste izbrisali kontakt " + contactForDeletion);
                }
                else
                {
                    Console.WriteLine("Kontakt nije pronađen!");
                }
                Console.WriteLine();
            }


        }

        static int BackToMenu()
        {
            Console.Write("Za povratak na početni Menu unesite '0': ");
            string input = Console.ReadLine();

            if (input == "0")
                return 0;
            else
                return 1;

        }


        static void AddNewContact(Dictionary<Contact, List<Call>> dictionary)
        {
            Console.WriteLine();
            string nameAndSurname = null, preference = null;
            string phoneNumber = null;
            
            while (string.IsNullOrWhiteSpace(nameAndSurname))
            {
                Console.Write("Unesite ime i prezime kontakta: ");
                nameAndSurname = Console.ReadLine();
            }

            while (true)
            {
                Console.Write("Unesite broj mobitela (9-10 znamenki bez ikakvih drugih znakova): ");
                phoneNumber = Console.ReadLine();
                var checkIfPhoneNumberIsNumber = IntInput(phoneNumber);
                if (checkIfPhoneNumberIsNumber != null && phoneNumber.Length >= 9 && phoneNumber.Length <= 10)
                    break;
                    
            }

            while (true)
            {
                Console.Write("Unesite preferencu kontakta (favorit / normalan / blokiran): ");
                preference = Console.ReadLine();
                if (preference == "favorit" || preference == "normalan" || preference == "blokiran")
                    break;
            }

            Contact newContact = new Contact(nameAndSurname, phoneNumber, preference);
            dictionary.Add(newContact, null);
            Console.WriteLine($"Uspješno ste dodali novi kontakt ({nameAndSurname})");


        }
        static void PrintAllContacts(Dictionary<Contact, List<Call>> dictionary)
        {
            foreach(var contact in dictionary.Keys)
            {
                Console.WriteLine($"{contact.NameAndSurname} - {contact.PhoneNumber} - {contact.Preference}");
            }
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
