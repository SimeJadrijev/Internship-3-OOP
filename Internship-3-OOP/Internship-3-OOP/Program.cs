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
            Contact firstContact = new Contact("Marko Livaja", "095123456", "favorit");

            Call firstCall = new Call();
            List<Call> firstCallsList = new List<Call>();

            Dictionary<Contact, List<Call>> dictionary = new Dictionary<Contact, List<Call>>();

            dictionary.Add(firstContact, firstCallsList);

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

                    case 4:
                        while (true)
                        {
                            EditPreference(dictionary);
                            if (BackToMenu() == 0)
                            {
                                Console.Clear();
                                actionChoice = Menu();
                                break;
                            }
                        }
                        break;

                    case 5:
                        var submenuChoice = Submenu();
                        switch (submenuChoice)
                        {
                            case 1:
                                while (true)
                                {
                                    PrintAllCallsWithCertainContact(dictionary);
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
                                    NewCall(dictionary);
                                    if (BackToMenu() == 0)
                                    {
                                        Console.Clear();
                                        actionChoice = Menu();
                                        break;
                                    }
                                }
                                break;
                            case 3:
                                Console.Clear();
                                actionChoice = Menu();
                                break;
                        }
                        break;
                    case 6:
                        while (true)
                        {
                            PrintAllCalls(dictionary);
                            if (BackToMenu() == 0)
                            {
                                Console.Clear();
                                actionChoice = Menu();
                                break;
                            }
                        }
                        break;
                        break;
                        
                        case 7:
                            Environment.Exit(0);    
                            break;
                        
                }
            }

            Console.ReadKey();
        }

        static void PrintAllCalls(Dictionary<Contact, List<Call>> dictionary)
        {
            Console.WriteLine();

            foreach (var contact in dictionary)
            {
                Console.WriteLine("Ispis poziva za kontakt: " + contact.Key.NameAndSurname);
                foreach (var call in contact.Value)
                {
                    Console.WriteLine($"Uspostava poziva: {call.CallStart} \nStatus poziva: {call.CallStatus}");
                }
                Console.WriteLine();
            }
        }

        static void NewCall(Dictionary<Contact, List<Call>> dictionary)
        {
            var breakWhileLoop = false;

            while (true)
            {
                Console.Write("Unesite ime i prezime kontakta kojem želite poslati poziv: ");
                var contactName = Console.ReadLine();

                var contactIsFound = false;
                var contactPreference = "temporary value";
                Contact requestedContact = null;             

                foreach (var contact in dictionary.Keys)
                {
                    if (contact.NameAndSurname == contactName)
                    {
                        requestedContact = contact;
                        contactIsFound = true;
                        contactPreference = contact.Preference;
                        break;
                    }
                }

                if (!contactIsFound)
                {
                    Console.WriteLine("Kontakt nije pronađen!");
                }
                else
                {
                    bool isInCall = false;

                    foreach (var calls in dictionary.Values)
                    {
                        if (calls != null)
                        {
                            foreach (var call in calls)
                            {
                                if (call.CallStatus == "u_tijeku")
                                {
                                    Console.WriteLine("Kontakt je već u pozivu!");
                                    isInCall = true;
                                    break;
                                }
                            }
                        }
                    }

                    if (isInCall)
                        break;
                    else if (contactPreference == "blokiran")
                    {
                        Console.WriteLine("Ne možete uputiti poziv jer ste blokirali kontakt!");
                        break;
                    }
                    else
                    {
                        string[] callStatusOptions = new[] { "u tijeku", "propusten", "zavrsen" };

                        Random rnd = new Random();
                        var randomNumber = rnd.Next(0, 3);
                        System.Threading.Thread.Sleep(100); 

                        var randomCallStatus = callStatusOptions[randomNumber];
                        var randomCallDuration = rnd.Next(1, 21);

                        Call newCall = new Call(DateTime.Now, randomCallStatus);

                        dictionary[requestedContact].Add(newCall);
                        Console.WriteLine("Poziv je uspješno upućen!");

                        breakWhileLoop = true;

                    }

                    if (breakWhileLoop)
                    {
                        break;
                    }
                }

                
            }
        }

        static void PrintAllCallsWithCertainContact(Dictionary<Contact, List<Call>> dictionary)
        {
            Console.WriteLine();


            while (true)
            {
                Console.Write("Unesite ime i prezime kontakta kojim želite upravljati: ");
                var contactName = Console.ReadLine();

                var contactIsFound = false;
                Contact requestedContact = null;

                foreach (var contact in dictionary.Keys)
                {
                    if (contact.NameAndSurname == contactName)
                    {
                        requestedContact = contact;
                        contactIsFound = true;
                        break;
                    }
                }

                if (contactIsFound)
                {
                    var callsForThisContact = dictionary[requestedContact];
                    foreach (var item in callsForThisContact)
                    {
                        Console.WriteLine($"Upostava poziva: {item.CallStart} \nStatus poziva: {item.CallStatus}");
                    }
                    break;
                }
                else
                    Console.WriteLine("Kontakt nije pronađen!");
            }


        }

        static int? Submenu()
        {
            Console.WriteLine();
            Console.WriteLine("- SUBMENU - \n\n" +
                            " 1 - Ispis svih poziva s određenim kontaktom \n" +
                            " 2 - Kreiranje novog poziva \n" +
                            " 3 - Izlaz");
            int? actionChoice;
            do
            {
                Console.Write("Odaberite jednu od ponuđenih opcija: ");
                actionChoice = IntInput(Console.ReadLine());
            } while (actionChoice == null || actionChoice < 1 || actionChoice > 3);

            return actionChoice;

        }

        static void EditPreference(Dictionary<Contact, List<Call>> dictionary)
        {
            Console.WriteLine();

            while (true)
            {
                Console.Write("Unesite ime i prezime kontakta kojemu želite promijeniti preferencu: ");
                var contactName = Console.ReadLine();
                Contact contactForEditing = null;

                foreach (var contact in dictionary.Keys)
                {
                    if (contact.NameAndSurname == contactName)
                    {
                        contactForEditing = contact;
                        break;
                    }
                }

                if (contactForEditing != null)
                {
                    var contactIsEdited = false;
                    Console.WriteLine($"Trenutna preferenca za kontakt {contactForEditing.NameAndSurname} je '{contactForEditing.Preference}'");
                    while (true)
                    {
                        Console.Write("Unesite novu preferencu (favorit / normalan / blokiran): ");
                        var newPreference = Console.ReadLine();
                        if (newPreference == "favorit" || newPreference == "normalan" || newPreference == "blokiran")
                        {
                            contactForEditing.Preference = newPreference;
                            contactIsEdited = true;
                            Console.WriteLine($"Uspješno ste dodali novu preferencu '{newPreference}' kontaktu '{contactName}'");
                            break;
                        }else
                            Console.WriteLine("Unesite jednu od ponuđenih opcija! \n");
                    }
                    if (contactIsEdited)
                        break;
                }
                Console.WriteLine("Ne postoji taj kontakt!");
            }
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
            var doesContactAlreadyExist = false;
            foreach (var contact in dictionary.Keys)
            {
                if (contact.PhoneNumber == phoneNumber)
                {
                    Console.WriteLine("U imeniku već postoji kontakt s istim brojem mobitela!");
                    doesContactAlreadyExist = true;
                    break;
                }
            }
            if (!doesContactAlreadyExist)
            {
                dictionary.Add(newContact, new List<Call>());
                Console.WriteLine($"Uspješno ste dodali novi kontakt ({nameAndSurname})");
            }



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
                                " 5 - Upravljanje kontaktom \n" +
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
