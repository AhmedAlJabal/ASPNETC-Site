using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTourBusinessObjects
{
    public class ReservedSeat : ItemJoin
    {
        private string @class;
        private string status;
        private string sector;

        public ReservedSeat(string id, string idJoin)
            : base(id, idJoin)
        {

        }

        public ReservedSeat()
        { }

        public string PassengerID
        {
            get { return base.getID(); }
            set { base.setID(value); }
        }

        public string ScheduledFlightID
        {
            get { return base.getIdJoin(); }
            set { base.setIdJoin(value); }
        }

        public string Class
        {
            get { return @class; }
            set { @class = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public string Sector
        {
            get { return sector; }
            set { sector = value; }
        }

    }
}
