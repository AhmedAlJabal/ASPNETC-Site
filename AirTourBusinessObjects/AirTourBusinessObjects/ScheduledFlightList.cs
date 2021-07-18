using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTourBusinessObjects
{
    public class ScheduledFlightList : DataList
    {

        public ScheduledFlightList()
           : base("ScheduledFlight", "ScheduledFlightID")
        { }

        protected override void GenerateList()
        {
            ScheduledFlight scheduledFlight = new ScheduledFlight();
            SetDataTableColumns(scheduledFlight);
            List.Clear();

            while (Reader.Read())
            {
                scheduledFlight = new ScheduledFlight(Reader.GetValue(0).ToString());
                base.SetValues(scheduledFlight);
                List.Add(scheduledFlight);
                AddDataTableRow(scheduledFlight);
            }
            Reader.Close();
            Connection.Close();

        }


    }
}
