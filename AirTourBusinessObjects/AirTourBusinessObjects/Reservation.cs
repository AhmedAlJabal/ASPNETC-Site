using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTourBusinessObjects
{
    public class Reservation : Item
    {
        private string customerID;
        private string employeeID;
        private string paid;
        private string reservationDate;
        private string price;

        public Reservation(string id)
            : base(id)
        {

        }

        public Reservation()
        {

        }

        public string ReservationID
        {
            get { return base.getID(); }
            set { base.setID(value); }
        }

        public string CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        public string EmployeeID
        {
            get { return employeeID; }
            set { employeeID = value; }
        }

        public string Paid
        {
            get { return paid; }
            set { paid = value; }
        }

        public string ReservationDate
        {
            get { return reservationDate; }
            set { reservationDate = value; }
        }

        public string Price
        {
            get { return price; }
            set { price = value; }
        }


    }
}
