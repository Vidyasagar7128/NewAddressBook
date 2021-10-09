using CsvHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace NewAddressBook
{
	class AddressBook :IComparer
	{
		ArrayList arrayList = new ArrayList();
		string csvFile = @"C:\Users\vidya\Desktop\DotNet\DummyFiles\csvFile.csv";

		int IComparer.Compare(object x, object y)
		{
			Contact m = (Contact)x;
			Contact n = (Contact)y;
			return m.firstName.CompareTo(n.firstName);
		}
		/// <summary>
		/// Create Contact
		/// </summary>
		public void Create()
		{
			Console.Write("First Name: ");
			string firstName = Console.ReadLine();
			Console.Write("Last Name: ");
			string lastName = Console.ReadLine();

			Console.Write("Address: ");
			string address = Console.ReadLine();
			Console.Write("City: ");
			string city = Console.ReadLine();

			Console.Write("State: ");
			string state = Console.ReadLine();
			Console.Write("Zip: ");
			long zip = long.Parse(Console.ReadLine());

			Console.Write("Phone No: ");
			long phone = long.Parse(Console.ReadLine());
			Console.Write("Email: ");
			string email = Console.ReadLine();
			Contact contact = new Contact()
			{
				firstName = firstName,
				lastName = lastName,
				address = address,
				city = city,
				state = state,
				zip = zip,
				phone = phone,
				email = email,
			};
			arrayList.Add(contact);
			foreach(Contact ct in this.arrayList)
            {
                if (ct.firstName.Equals(firstName) && arrayList.Count !=1)
                {
					Console.WriteLine("Username already Exist!");
					Del(arrayList.IndexOf(ct));
					break;
                }
                else
                {
					Console.WriteLine("Data Added!");
					break;
                }
            }
			///Writing CSV File
			using(var writer = new StreamWriter(csvFile))
			using(var csvExport = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
				csvExport.WriteRecords(arrayList);
            }
		}
		/// <summary>
		/// Remove Duplicate Contact by username
		/// </summary>
		public void Del(int index)
        {
			for(int i = 0; i < arrayList.Count-1; i++)
            {
                if (arrayList[i].Equals(arrayList[index]))
                {
					this.arrayList.RemoveAt(arrayList.Count-1);
                }
            }
        }
		public void showAllContacts()
		{
			arrayList.Sort(new AddressBook());
			Console.WriteLine("-----------------------------------");
			foreach (Contact data in arrayList)
			{
				Console.WriteLine($"{data.firstName} {data.lastName} {data.address} {data.city} {data.state} {data.zip} {data.phone} {data.email}");
				Console.WriteLine("-----------------------------------");
			}
			///Reading CSV File
			using (var reader = new StreamReader(csvFile))
			using(var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
				var records = csv.GetRecords<Contact>().ToList();
				Console.WriteLine("Data Fetched Succesfully from CSV File!");
				Console.WriteLine("-----------------------------------");
				foreach (var i in records)
                {
					Console.WriteLine($"{i.firstName} {i.lastName} {i.address} {i.phone} {i.email}");
					Console.WriteLine("-----------------------------------");
				}
			}
		}
		/// <summary>
		/// Edit Contact using Name
		/// </summary>
		public void editData()
		{
			Console.Write("Enter Name which do you want to Edit:");
			string editName = Console.ReadLine();
			int index = 0;
			Console.Write("First Name: ");
			string firstName = Console.ReadLine();
			Console.Write("Last Name: ");
			string lastName = Console.ReadLine();

			Console.Write("Address: ");
			string address = Console.ReadLine();
			Console.Write("City: ");
			string city = Console.ReadLine();

			Console.Write("State: ");
			string state = Console.ReadLine();
			Console.Write("Zip: ");
			long zip = long.Parse(Console.ReadLine());

			Console.Write("Phone No: ");
			long phone = long.Parse(Console.ReadLine());
			Console.Write("Email: ");
			string email = Console.ReadLine();
			Contact contact = new Contact()
			{
				firstName = firstName,
				lastName = lastName,
				address = address,
				city = city,
				state = state,
				zip = zip,
				phone = phone,
				email = email,
			};
			foreach (Contact ct in arrayList)
			{
				if (ct.firstName == editName)
                {
					index = arrayList.IndexOf(ct);
					for (int i = 0; i < arrayList.Count; i++)
					{
						if (arrayList[i] == arrayList[index])
						{
							arrayList[i] = contact;
						}
					}
				}
                else
					Console.WriteLine("Wrong Username");
			}
		}
		/// <summary>
		/// Delete Contact
		/// </summary>
		public void DeleteData()
		{
			Console.Write("Enter Name which do you want to Delete: ");
			string delName = Console.ReadLine();
			int index = 0;
			foreach (Contact ct in arrayList)
			{
				if (ct.firstName == delName)
					index = arrayList.IndexOf(ct);
			}
			for (int i = 0; i < arrayList.Count; i++)
			{
				if (arrayList[i] == arrayList[index])
				{
					arrayList.RemoveAt(i);
					Console.WriteLine(i);
				}
			}
		}
		/// <summary>
		/// Search Contact using City or State
		/// </summary>
		public void SearchContact()
		{
			Console.Write("Enter City or State: ");
			string search = Console.ReadLine();
			int num = 0;
			Console.WriteLine("-----------------------------------");
			foreach (Contact ct in arrayList)
			{
				if (search == ct.city || search == ct.state)
				{
					num++;
					Console.WriteLine($"{num}). {ct.firstName} {ct.lastName} {ct.city} {ct.state} {ct.zip} {ct.phone} {ct.email}");
					Console.WriteLine("-----------------------------------");
				}
			}
		}
		/// <summary>
		/// Switch for Repeat Process
		/// </summary>

		public void repeat()
		{
			Console.WriteLine("");
			Console.WriteLine("press 1 to create contact :");
			Console.WriteLine("press 2 to print contacts :");
			Console.WriteLine("press 3 to edit contacts :");
			Console.WriteLine("press 4 to delete contact :");
			Console.WriteLine("press 5 to search contact :");
			Console.WriteLine("press 0 to exit program :");
			int res = int.Parse(Console.ReadLine());
			switch (res)
			{
				case 1:
					Create();
					repeat();
					break;
				case 2:
					showAllContacts();
					repeat();
					break;
				case 3:
					editData();
					repeat();
					break;
				case 4:
					DeleteData();
					repeat();
					break;
				case 5:
					SearchContact();
					repeat();
					break;
				case 0:
					Console.WriteLine("Exit");
					break;
			}
		}
	}
}