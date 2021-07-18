using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AirTourBusinessObjects
{
    public class DataList
    {

        protected string table;
        protected SqlConnection connection;
        protected SqlCommand command;
        protected SqlDataReader reader;
        protected string idField;
        protected DataTable dataTable;
        protected List<Item> list;

        public string IdField
        {
            get { return idField; }
            set { idField = value; }
        }

        public DataTable DataTable
        {
            get { return dataTable; }
            set { dataTable = value; }
        }


        //protected as used only by subclasses
        protected SqlConnection Connection
        {
            get { return connection; }
            set { connection = value; }
        }

        //protected as used only by subclasses
        protected SqlDataReader Reader
        {
            get { return reader; }

            set
            {
                reader = value;
            }
        }

        public List<Item> List
        {
            get { return list; }

            set
            {
                list = value;
            }
        }

        public DataList(string table, string idField)
        {
            
            this.table = table;
            connection =
                new SqlConnection(AirTourBusinessObjects.Properties.Settings.Default.AirtoursConnectionString);
            command = connection.CreateCommand();
            this.idField = idField;
            dataTable = new DataTable();
            list = new List<Item>();
        }


        //the following method will return all of the records from the table that we pass into the method
        public void Populate()
        {
            connection.Open();
            command.CommandText = "SELECT * FROM " + table;
            reader = command.ExecuteReader();
            GenerateList();
        }


        //the following method will return a single record from a table with the ID of the passed item
        public void Populate(Item item)
        {
            //opening a connection
            connection.Open();

            //clearing the parameters
            command.Parameters.Clear();

            //we will add the item ID as a parameter
            command.Parameters.AddWithValue("@id", item.getID());

            //the command
            command.CommandText = "SELECT * FROM " + table + " WHERE " + idField + " = @id";

            //executing
            reader = command.ExecuteReader();

            //if we execute and the reader has a value it will setValues for the passed in item
            if (reader.Read())
            {
                SetValues(item);
                
            }
            
            //closing the reader and the conenction
            reader.Close();
            connection.Close();

        }


        //creating dataTable columns for a specified item
        public void SetDataTableColumns(Item item)
        {
            //clearing the datatable
            dataTable.Clear();
            dataTable.Columns.Clear();

            //getting the item type
            Type type = item.GetType();

            //creating a properties list and looping through it
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                //adding a dataTable column with the proberty name
                dataTable.Columns.Add(property.Name);
            }

        }


        //adding a datatable row for the item
        public void AddDataTableRow(Item item)
        {
            //used for the index of the list
            int count = 0;

            //getting the item type and then creating a list of them
            Type type = item.GetType();
            PropertyInfo[] properties = type.GetProperties();

            //creating a list with the size of the properties of the item
            string[] values = new string[properties.Count()];

            //looping through the properties list and equaling its value to the (values) list
            foreach (PropertyInfo property in properties)
            {
                values[count] = property.GetValue(item).ToString();
                count++;
            }
            //adding the values list to the datatable as a cloumn
            dataTable.Rows.Add(values);
        }

        
        //needed so that it can be overridden in subclasses
        protected virtual void GenerateList()
        { }

        //setting values to a passed item
        protected void SetValues(Item item)
        {
            //getting the item type and making a list of its properties
            Type type = item.GetType();

            PropertyInfo[] properties = type.GetProperties();

            //for each property we will set the value or the item to the value we get from the reader
            int fieldCount = 0;
            foreach (PropertyInfo property in properties)
            {
                property.SetValue(item, reader.GetValue(fieldCount).ToString());
                fieldCount++;
            }
        }

        //used to update an item
        public void Update(Item item)
        {
            connection.Open();

            //clearing the parameters
            command.Parameters.Clear();

            //getting the item type and creating a property list and then looping throught it
            Type type = item.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(item) != null) //if the property value is not null
                {
                    //if the current property is not the ID Field
                    if (!property.Name.Equals(idField, StringComparison.InvariantCultureIgnoreCase))
                    {
                        //use parameter for the user value - prevent SQL injection
                        command.Parameters.AddWithValue("@id", item.getID());
                        command.Parameters.AddWithValue("@value", property.GetValue(item));

                        //generate SQL Update string for the current property name and value
                        command.CommandText = "UPDATE " + table +
                            " SET " + property.Name + " = @value WHERE " + idField + " =  @id";
                        command.ExecuteNonQuery(); //execute command; update the database
                        command.Parameters.Clear(); //clear parameter for next iteration of loop
                    }
                }
            }
            connection.Close();
        }

        //used to add a new record of an item
        public void Add(Item item)
        {
            connection.Open();

            //following block of code is used to getting the datatable key informations
            command.CommandText = "SELECT * FROM " + table;
            reader = command.ExecuteReader(CommandBehavior.KeyInfo);
            DataTable schemaTable = reader.GetSchemaTable();
            reader.Close();

            //clearing the parameters
            command.Parameters.Clear();

            //getting the item type and then creating a property list of it
            Type type = item.GetType();
            PropertyInfo[] properties = type.GetProperties();
            int count = 0;
            //create the first part of the Add SQL string
            string addString = "INSERT INTO " + table + "(";

            //add each item property name to the string
            foreach (PropertyInfo property in properties)
            {
                if (!schemaTable.Rows[count]["IsAutoIncrement"].ToString().Equals("True"))
                {
                    addString += property.Name;
                    count++;
                    //add a comma until end of Properties collection is reached
                    if (count < properties.Count())
                    { addString += ", "; }
                }
                else
                {
                    count++;
                }
            }


            //start second part of Add string
            addString += ") VALUES(";
            count = 0;
            int paramCounter = 1;
            //add each item property value to the string
            foreach (PropertyInfo property in properties)
            {
                if (!schemaTable.Rows[count]["IsAutoIncrement"].ToString().Equals("True"))
                {
                    if (property.GetValue(item) != null)
                    {
                        command.Parameters.AddWithValue("@" + paramCounter, property.GetValue(item));
                        addString += "@" + paramCounter; //insert parameter in string
                        paramCounter++;
                    }
                    else
                    { addString += "NULL"; }
                    count++;
                    //add a comma until end of Properties collection is reached
                    if (count < properties.Count())
                    { addString += ", "; }
                }
                else
                {
                    count++;
                }
            }
            //add bracket at end of Add string
            addString += ")";

            command.CommandText = addString;
            try
            { command.ExecuteNonQuery(); }
            catch (SqlException ex)
            {
                item.setValid(false);
                item.setErrorMessage(ex.Message);
            }
            connection.Close();
        }

        //deleting a record of an item
        public void Delete(Item item)
        {
            connection.Open();

            //clearing the parameters
            command.Parameters.Clear();

            //adding a parameter with the value of the item ID
            command.Parameters.AddWithValue("@id", item.getID());

            //the command
            command.CommandText = "DELETE FROM " + table +
                " WHERE " + idField + " = @id";

            //trying to execute the command and if it fails the program will catch the error and throw it in the item Error message
            try { command.ExecuteNonQuery(); }
            catch (SqlException ex)
            {
                item.setValid(false);
                item.setErrorMessage(ex.Message);
            }
            connection.Close();
        }

        //function used to generate a list of an object where a field = value 
        public void Filter(string field, string value)
        {
            connection.Open();

            //clearing the parameter
            command.Parameters.Clear();

            //set the sql command
            command.CommandText = $"SELECT * FROM {table} WHERE {field} = @value";
            //set the value
            command.Parameters.AddWithValue("@value", value);
            //open the connection
            
            //execute the reader
            reader = command.ExecuteReader();
            //create the list of objects
            GenerateList();
            //close the connection
            connection.Close();
            //close the reader
            Reader.Close();
        }

        //function number 1
        //function used to get the total value of a certain column
        public double TotalValue(string column)
        {
            //the total variable
            double total =0;

            //opening a connection
            connection.Open();

            //clearing the parameters
            command.Parameters.Clear();

            //the command and then executing it
            command.CommandText = "SELECT SUM(" +column +") From " + table;
            reader = command.ExecuteReader();

            //if the reader read data it will make the total variable equals to the redear value
            if (reader.Read())
            {
                total = reader.GetDouble(0);
            }

            //closing the reader and returning the total 
            reader.Close();
            connection.Close();
            return total;
        }

        //function number 2
        //returns the maxID (biggest ID value) of a certain table that the user chooses
        public int GetMaxID()
        {
            //the maxID variable
            int maxID =0;

            //opening a connection
            connection.Open();

            //clearing the parameter
            command.Parameters.Clear();

            //the command and then executing it
            command.CommandText = "select max (" + idField + ") from " + table;
            reader = command.ExecuteReader();

            //if the reader reads a value it will make the maxID variable equals to the reader value
            if (reader.Read())
            {
                maxID = reader.GetInt32(0);
            }

            //closing the connection and returning the maxID
            reader.Close();
            connection.Close();
            return maxID;
        }

        //function number 3
        //this function will return the sum of a certain field(sumField)  wherea column name and its value matches
        public double TotalValue(string columnName, string ColumnValue, string sumField)
        {
            //the total variable
            double total = 0;

            //openign a connection
            connection.Open();

            //clearing the parameters
            command.Parameters.Clear();

            //adding the column value
            command.Parameters.AddWithValue("@columnValue",ColumnValue);

            //the command and executing it
            command.CommandText = "select sum(" + sumField + ") from " + table + " where " + columnName + " = @columnValue";
            reader = command.ExecuteReader();

            //if the reader can read data it will change the total value to the reader value
            if (reader.Read())
            {
                total = Convert.ToDouble(reader.GetValue(0));
            }

            //closing reader and connection then returning the total value
            reader.Close();
            connection.Close();

            return total;
        }

        //function number 4
        //this function will return a list where a column and its value matches And another column and its value matches
        public void Filter(string FilterColumn1, string FilterValue1,
             string FilterColumn2, string FilterValue2)
        {
            //opening the connection
            connection.Open();

            //clearing the parameters
            command.Parameters.Clear();

            //adding the filtered value to the parameters
            command.Parameters.AddWithValue("@value1",FilterValue1);
            command.Parameters.AddWithValue("@value2",FilterValue2);

            //the command
            command.CommandText = "select * from " + table + " where " + FilterColumn1 + " = @value1" +
                " and " + FilterColumn2 + " = @value2";

            //executing the command and then calling the generateList method to generate the lsit
            reader = command.ExecuteReader();
            GenerateList();
        }

        //function 5
        //this method will return the total value of a field(sumField) where a field1 = value1 and  field2 = value2
        public double TotalValue(string sumField, string FieldName1, string FieldValue1,
            string FieldName2, string FieldValue2)
        {
            //the total variable
            double total = 0;
            //opening a connection and clearing the parameters
            connection.Open();
            command.Parameters.Clear();

            //adding the two values fields as parameters
            command.Parameters.AddWithValue("@value1", FieldValue1);
            command.Parameters.AddWithValue("@value2", FieldValue2);

            //the command and executing it
            command.CommandText = "select sum(" + sumField + ") from " + table + " where " + FieldName1 + " = @value1" +
                " and " + FieldName2 + " =" +
                " @value2 ";

            reader = command.ExecuteReader();

            //if the reader has read data the total variable value will change to the reader read value
            if (reader.Read())
            {
                total = Convert.ToDouble(reader.GetValue(0));
            }

            //closing the connection and returning the total
            reader.Close();
            connection.Close();

            return total;

        }

        //function 6
        //this function will return the number of records for a certain field(countfield) where a column1 = value1 and column2 = field2
        public int TotalCount(string countField, string ColumnField1, string ColumnValue1,
            string ColumnField2, string ColumnValue2)
        {
            //total variable
            int total = 0;

            //opening a connection and clearing its parameters
            connection.Open();
            command.Parameters.Clear();

            //adding the two field values as parameters
            command.Parameters.AddWithValue("@fieldValue2", ColumnValue2);
            command.Parameters.AddWithValue("@fieldValue1", ColumnValue1);

            //the commmand and executing it
            command.CommandText = "select count(" + countField + ") from " + table + " where " + ColumnField1 + " " +
                " = @fieldValue1 and " + ColumnField2 + " = @fieldValue";
            reader = command.ExecuteReader();

            //if the reader reads data it will change the total value to the reader value
            if (reader.Read())
            {
                total = Convert.ToInt32(reader.GetValue(0));
            }

            //closing the reader and the connection and returning the total
            reader.Close();
            connection.Close();

            return total;
        }

        //function 7
        //this function will return a string list for a certain column and will sort them depending on the passed parameter
        public List<string> UniqueValues(string columnName, string sortType)
        {
            //creating the list
            List<string> list = new List<string>();

            //opening the connection and clearing the parameters
            connection.Open();
            command.Parameters.Clear();

            //the command and executing it
            command.CommandText = "select DISTINCT " + columnName + " from " + table + " Order BY " + columnName +" "+ sortType;
            reader = command.ExecuteReader();

            //while the reader can read data it will add the reader value to the list
            while (reader.Read())
            {
                list.Add(reader.GetValue(0).ToString());
            }

            //close the reader and the connection and return the list
            Reader.Close();
            Connection.Close();
            return list;
        }

        //function 8
        //generates a list of all records in a table where a certain column = a value and another column is larger than a value (column2 > value2)
        public void FilterPlus(string ColumnName1, string ColumnValue1,
            string ColumnName2, string ColumnValue2)
        {
            //opening a connection and clearing the parameters
            connection.Open();
            command.Parameters.Clear();

            //adding the values as parameters
            command.Parameters.AddWithValue("@value1",ColumnValue1);
            command.Parameters.AddWithValue("@value2", ColumnValue2);

            //the command and then executing it
            command.CommandText = "select * from " + table + " where " + ColumnName1 + "= @value1 and "
                + ColumnName2 + " > @value2";
            reader = command.ExecuteReader();

            //calling the GenerateList method
            GenerateList();
        }

        //function 9
        //checks if a record exists in a table where a certain column = to a value
        public bool CheckChildRecords(string columnName, string columnValue)
        {
            //setting the bool
            bool found = false;

            //opening a connection and clearing the parameters
            connection.Open();
            command.Parameters.Clear();

            //adding the value as a parameter
            command.Parameters.AddWithValue("@value", columnValue);

            //the command and executing it
            command.CommandText = "select * from " + table + " where " + columnName + " =@value";
            reader = command.ExecuteReader();

            //if the reader Read some data it will change the bool to true
            if (reader.Read())
            {
                found = true;
            }

            //closing the reader and the connection and then returning the found bool
            reader.Close();
            connection.Close();
            return found;
        }

        //function 10
        //deleting all records in a certain table where a column name = a value
        public void Delete(string columnName, string columnValue)
        {
            //opening the connecton and clearing the parameters
            connection.Open();
            command.Parameters.Clear();

            //adding the cvalue as a parameter
            command.Parameters.AddWithValue("@value",columnValue);

            //the command
            command.CommandText = "delete from " + table + " where " + columnName + " =@value";

            //trying to execute the command and if it fails it will catch the error and write it in the console
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.Write(ex);
            }

            //clsoing the connection
            connection.Close();

        }

        //function 11
        //deleting all records from two joined tables where a column = a value
        public void Delete(string column, string value,
            string joinColumn, string joinTableName)
        {
            //opening the connection and clearing the parameters
            connection.Open();
            command.Parameters.Clear();

            //adding the value as a parameter
            command.Parameters.AddWithValue("@value", value);

            //the command
            command.CommandText = $"delete {table} from {table}, {joinTableName}" +
                $"  where {table}.{joinColumn} = {joinTableName}.{joinColumn} and" +
                $" {table}.{column} = @value";

            //trying to execute the command and if it fails an error will be written in the console
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.Write(ex);
            }

            //closing the connection
            connection.Close();

        }

        //fucnction 12
        //this method will create a list of item objects  by joining in two tables where a column value is equal to a column
        //and the key value in the two tables are the same
        public void FilterJoin(string joinTableName, string key, string column, string value)
        {
            //opening a connection and clearing the parameters
            connection.Open();
            command.Parameters.Clear();

            //adding the key value and the value to the parameters
            command.Parameters.AddWithValue("@key", key);
            command.Parameters.AddWithValue("@value", value);

            //the command and executing it
            command.CommandText = $"select * from {table}, {joinTableName} where {table}.@key = {joinTableName}.$key and {table}.{column} =@value";
            reader = command.ExecuteReader();

            //calling the generate list method
            GenerateList();


        }


        //function 13
        //this method will generate a list by joining in 2 tables where the 3 columns = to 3 values
        //and the joined column in both tables equals to each other
        public void Filter(string ColumnName, string ColumnValue, string joinedColumn,
            string joinedTable, string QuereyColumnName1, string QueryColumnValue1,
            string QueryColumnName2, string QueryColumnValue2)
        {
            //opening a connection and clearing the parameters
            connection.Open();
            command.Parameters.Clear();

            //adding the columsn values as parameters
            command.Parameters.AddWithValue("@columnValue", ColumnValue);
            command.Parameters.AddWithValue("@QueryColumnValue1", QueryColumnValue1);
            command.Parameters.AddWithValue("@QueryColumnValue2", QueryColumnValue2);

            //the command and executing it
            command.CommandText = $"select * from {table} where {QuereyColumnName1} = @QueryColumnValue1 and" +
                $" {QueryColumnName2} = @QueryColumnValue2 and  {joinedColumn}" +
                $" in (select {joinedColumn} from {joinedTable} where {ColumnName} = @columnValue)";
                 
            reader = command.ExecuteReader();

            //calling the generate list method
            GenerateList();

        }





    }
}
