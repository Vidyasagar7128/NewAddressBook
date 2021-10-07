using System;
using System.Collections.Generic;

namespace NewAddressBook
{
    class Program
    {
        static Dictionary<string, AddressBook> addressBooks = new Dictionary<string, AddressBook>();
        /// <summary>
        /// Print AddressBooks
        /// </summary>
        public static void printAddressBooks()
        {
            foreach (var i in addressBooks)
            {
                Console.WriteLine(i.Key);
            }
        }
        /// <summary>
        /// Set AddressBook to Contact 
        /// </summary>
        /// <param name="adbsName"></param>
        private static void setAddressBooks(String adbsName)
        {
            if (addressBooks.ContainsKey(adbsName))
            {
                AddressBook addressBook = addressBooks[adbsName];
                addressBook.repeat();
            }
            else
            {
                Console.WriteLine(adbsName + " AddressBook is not found");
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("AddressBook!");
            bool continueLoop = true;
            while (continueLoop)
            {
                Console.WriteLine(
                        "\nEnter 1. To Create AddressBook\nEnter 2. To Go Into AddressBook\nEnter 3. To Print AddressBooks\nEnter 0 To Exit");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Create AddressBook with Given Name : ");
                        String abName = Console.ReadLine();
                        AddressBook addressBook = new AddressBook();
                        addressBooks[abName] = addressBook;
                        break;
                    case 2:
                        Console.WriteLine("Set AddressBook to Contacts : ");
                        String adbsName = Console.ReadLine();
                        setAddressBooks(adbsName);
                        break;
                    case 3:
                        printAddressBooks();
                        break;
                    case 0:
                        Console.WriteLine("Exit");
                        continueLoop = false;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
