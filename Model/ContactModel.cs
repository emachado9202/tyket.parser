using System;
namespace Tyket.Parser.Model
{
	public class ContactModel
	{
		public ContactModel()
		{
		}
		public string Id { get; set; }
		public string Full_Name { get; set; }
		public double Yearly_Income { get; set; }
		public ContactAddressModel Address { get; set; }

		public string ToString()
		{
			return $"Id: {Id}, Full Name: {Full_Name}, Income: {String.Format("{0:C}", Yearly_Income)}";
		}
	}
}

