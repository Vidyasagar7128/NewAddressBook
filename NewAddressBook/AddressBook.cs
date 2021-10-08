using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
			foreach (Contact data in arrayList)
			{
				Console.WriteLine($"{data.firstName} {data.lastName} {data.address} {data.city} {data.state} {data.zip} {data.phone} {data.email}");
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
		/// Switch for Repeat Process
		/// </summary>
		public void repeat()
		{
			Console.WriteLine("");
			Console.WriteLine("press 1 to create contact :");
			Console.WriteLine("press 2 to print contacts :");
			Console.WriteLine("press 3 to edit contacts :");
			Console.WriteLine("press 4 to delete contact :");
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
				case 0:
					Console.WriteLine("Exit");
					break;
			}
		}
	}
}