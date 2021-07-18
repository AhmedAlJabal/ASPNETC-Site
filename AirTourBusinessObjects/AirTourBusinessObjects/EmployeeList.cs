using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTourBusinessObjects
{
    public class EmployeeList : DataList
    {
        public EmployeeList()
           : base("Employee", "EmployeeID")
        { }

        protected override void GenerateList()
        {
            Employee employee = new Employee();
            SetDataTableColumns(employee);
            List.Clear();

            while (Reader.Read())
            {
                employee = new Employee(Reader.GetValue(0).ToString());
                base.SetValues(employee);
                List.Add(employee);
                AddDataTableRow(employee);
            }
            Reader.Close();
            Connection.Close();

        }
    }
}
