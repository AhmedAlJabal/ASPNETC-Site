using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTourBusinessObjects
{
    public class ScheduledFlight : Item
    {
        private string flightID;
        private string flightDate;

        public ScheduledFlight(string id)
            : base(id)
        {

        }

        public ScheduledFlight()
        {
            
        }

        public string ScheduledFlightID
        {
            get { return base.getID(); }
            set { base.setID(value); }
        }

        public string FlightID
        {
            get { return flightID; }
            set { flightID = value; }
        }

        public string FlightDate
        {
            get { return flightDate; }
            set { flightDate = value; }
        }


    }
}
