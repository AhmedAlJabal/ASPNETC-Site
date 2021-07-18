using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AirTourBusinessObjects
{
    public class CustomerList : DataList
    {

        public CustomerList()
           : base("Customer", "CustomerID")
        { }

        protected override void GenerateList()
        {
            Customer customer = new Customer();
            SetDataTableColumns(customer);
            List.Clear();

            while (Reader.Read())
            {
                customer = new Customer(Reader.GetValue(0).ToString());
                base.SetValues(customer);
                List.Add(customer);
                AddDataTableRow(customer);
            }
            Reader.Close();
            Connection.Close();

        }

    }
}
