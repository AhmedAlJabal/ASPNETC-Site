using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTourBusinessObjects
{
    public class Customer : Item
    {
        
        private string Lname;
        private string Fname;
        private string phone;
        private string address;
        private string city;
        private string region;
        private string postalCode;
        private string country;
        private string fax;
        private string email;
        private string miles;
        private string password;
        private string creditCardNumber;
        private string securityCode;

        public Customer(string id)
            : base(id)
        {

        }

        public Customer()
        {

        }

        public string CustomerID
        {
            get { return base.getID(); }
            set { base.setID(value); }
        }

        public string LName
        {
            get { return Lname; }
            set { Lname = value; }
        }

        public string FName
        {
            get { return Fname; }
            set { Fname = value; } 
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        public string Region
        {
            get { return region; }
            set { region = value; }
        }

        public string PostalCode
        {
            get { return postalCode; }
            set { postalCode = value; }
        }

        public string Country
        {
            get { return country; }
            set { country = value; }
        }

        public string Fax
        {
            get { return fax; }
            set { fax = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Miles
        {
            get { return miles; }
            set { miles = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string CreditCardNumber
        {
            get { return creditCardNumber; }
            set { creditCardNumber = value; }
        }

        public string SecurityCode
        {
            get { return securityCode; }
            set { securityCode = value; }
        }

    }
}
