using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace NewAddressBook
{
	class AddressBook :IComparer
	{
		private string connectionPath = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        readonly SqlConnection sqlConnection = new SqlConnection();
		ArrayList arrayList = new ArrayList();
		string csvFile = @"C:\Users\vidya\Desktop\DotNet\DummyFiles\csvFile.csv";
		string jsonFile = @"C:\Users\vidya\Desktop\DotNet\DummyFiles\jsonFile.json";
		
		int IComparer.Compare(object x, object y)
		{
			Contact m = (Contact)x;
			Contact n = (Contact)y;
			return m.firstName.CompareTo(n.firstName);
		}
        /// <summary>
        /// Create Contact
        /// </summary>
        public void Create(string name)
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
			using (var writer = new StreamWriter(csvFile))
            {
				using (var csvExport = new CsvWriter(writer, CultureInfo.InvariantCulture))
				{
					csvExport.WriteRecords(arrayList);
				}
			}
			///Writing JSON File
			using (var writer = new StreamWriter(jsonFile))
			{
				String jsonData = JsonConvert.SerializeObject(arrayList);
				writer.WriteLine(jsonData);
			}
			/// Save Users to Database using ADO.NET
			sqlConnection.ConnectionString = connectionPath;
			SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "sp_newAddressBook";

            sqlCommand.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = firstName;
            sqlCommand.Parameters.Add("@Lastname", SqlDbType.VarChar).Value = lastName;
            sqlCommand.Parameters.Add("@Address", SqlDbType.VarChar).Value = address;
            sqlCommand.Parameters.Add("@City", SqlDbType.VarChar).Value = city;
            sqlCommand.Parameters.Add("@State", SqlDbType.VarChar).Value = state;
            sqlCommand.Parameters.Add("@ZIP", SqlDbType.VarChar).Value = zip;
            sqlCommand.Parameters.Add("@Phone", SqlDbType.VarChar).Value = phone;
            sqlCommand.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
            sqlCommand.Parameters.Add("@BookName", SqlDbType.VarChar).Value = name;

            try
            {
                sqlConnection.Open();
                int count = sqlCommand.ExecuteNonQuery();
                if (count == -1)
                    Console.WriteLine($"Contact Created Succesfully...");
                else
                    Console.WriteLine($"Failed to Create Contact...");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Message: {e.Message}");
            }
            finally
            {
                sqlConnection.Close();
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
					Console.WriteLine($"{i.firstName} {i.lastName} {i.address} {i.city} {i.zip} {i.phone} {i.email}");
					Console.WriteLine("-----------------------------------");
				}
			}
			///Reading Json File
			using (var reader = new StreamReader(jsonFile))
            {
				var stringData = reader.ReadLine();
				var data = JsonConvert.DeserializeObject<List<Contact>>(stringData);
				Console.WriteLine("Data Fetched Succesfully from JSON File!");
				Console.WriteLine("-----------------------------------");
				foreach (Contact i in data)
                {
					Console.WriteLine($"{i.firstName} {i.lastName} {i.address} {i.city} {i.zip} {i.phone} {i.email}");
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

		public void repeat(string bookName)
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
					Create(bookName);
					repeat(bookName);
					break;
				case 2:
					showAllContacts();
					repeat(bookName);
					break;
				case 3:
					editData();
					repeat(bookName);
					break;
				case 4:
					DeleteData();
					repeat(bookName);
					break;
				case 5:
					SearchContact();
					repeat(bookName);
					break;
				case 0:
					Console.WriteLine("Exit");
					break;
			}
		}
	}
}
/*
 JsonSerializer serializer = new JsonSerializer();
			using (var sw = new StreamWriter(csvFile))
			using (JsonTextWriter writer1 = new JsonTextWriter(sw)
			{
				serializer.Serialize(writer1,arrayList);
			}
 */