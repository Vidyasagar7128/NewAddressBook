using System;
using System.Collections.Generic;
using System.Text;

namespace NewAddressBook
{
	class Contact
	{
		public string firstName { set; get; }
		public string lastName { set; get; }
		public string address { set; get; }
		public string city { set; get; }
		public string state { set; get; }
		public long zip { set; get; }
		public long phone { set; get; }
		public string email { set; get; }
	}
}