using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NewAddressBook
{
	class AddressBook
	{
		ArrayList arrayList = new ArrayList();

		/// <summary>
		/// Create Contact
		/// </summary>
		public void Create()
		{
			Console.WriteLine("First Name: ");
			string firstName = Console.ReadLine();
			Console.WriteLine("Last Name: ");
			string lastName = Console.ReadLine();

			Console.WriteLine("Address: ");
			string address = Console.ReadLine();
			Console.WriteLine("City: ");
			string city = Console.ReadLine();

			Console.WriteLine("State: ");
			string state = Console.ReadLine();
			Console.WriteLine("Zip: ");
			long zip = long.Parse(Console.ReadLine());

			Console.WriteLine("Phone No: ");
			long phone = long.Parse(Console.ReadLine());
			Console.WriteLine("Email: ");
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
		}
		/// <summary>
		/// Show Contact
		/// </summary>
		public void showAllContacts()
		{
			foreach (Contact data in arrayList)
			{
				Console.WriteLine($"{data.firstName} {data.lastName} {data.address} {data.city} {data.state} {data.zip} {data.phone} {data.email}");
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

				case 0:
					Console.WriteLine("Exit");
					break;
			}
		}
	}
}