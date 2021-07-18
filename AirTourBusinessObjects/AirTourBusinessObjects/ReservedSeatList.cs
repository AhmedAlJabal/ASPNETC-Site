using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTourBusinessObjects
{
    public class ReservedSeatList : DataListJoin
    {
        public ReservedSeatList()
            : base("[ReservedSeat]",
            "PassengerID", "ScheduledFlightID")
        { }

        protected override void GenerateList()
        {
            ReservedSeat reservedSeat = new ReservedSeat();
            SetDataTableColumns(reservedSeat);
            List.Clear();

            while (Reader.Read())
            {
                reservedSeat = new ReservedSeat(Reader.GetValue(0).ToString(),
                    Reader.GetValue(1).ToString());
                base.SetValues(reservedSeat);
                List.Add(reservedSeat);
                AddDataTableRow(reservedSeat);
            }
            Reader.Close();
            Connection.Close();
        }


    }
}
