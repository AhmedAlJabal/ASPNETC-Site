using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AirTourBusinessObjects
{
    public class DataListJoin : DataList
    {
        private string idFieldJoin;
        public DataListJoin(string table, string idField, string idFieldJoin)
            : base(table, idField)
        {
            this.idFieldJoin = idFieldJoin;
        }

        public void Populate(ItemJoin item)
        {
            Connection.Open();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@id", item.getID());
            command.Parameters.AddWithValue("@idJoin", item.getIdJoin());
            command.CommandText = "SELECT * FROM " + table + " WHERE " + IdField + " = @id" +
                " AND " + idFieldJoin + " = @idJoin";
            Reader = command.ExecuteReader();
            if (Reader.Read())
            {
                SetValues(item);
            }
            Reader.Close();
            Connection.Close();
        }

        public void Update(ItemJoin item)
        {
            Connection.Open();
            command.Parameters.Clear();
            Type type = item.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(item) != null) //if the property value is not null
                {
                    //if the current property is not the ID Field
                    if (!property.Name.Equals(IdField, StringComparison.InvariantCultureIgnoreCase))
                    {
                        //use parameter for the user value - prevent SQL injection
                        command.Parameters.AddWithValue("@id", item.getID());
                        command.Parameters.AddWithValue("@idJoin", item.getIdJoin());
                        command.Parameters.AddWithValue("@value", property.GetValue(item));

                        //generate SQL Update string for the current property name and value
                        command.CommandText = "UPDATE " + table +
                           " SET " + property.Name + " = @value WHERE " + IdField + " =  @id" +
                           " AND " + idFieldJoin + " = @idJoin";
                        command.ExecuteNonQuery(); //execute command; update the database
                        command.Parameters.Clear(); //clear parameter for next iteration of loop
                    }
                }
            }
            Connection.Close();
        }

        public void Delete(ItemJoin item)
        {
            Connection.Open();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@id", item.getID());
            command.Parameters.AddWithValue("@idJoin", item.getIdJoin());
            command.CommandText = "DELETE FROM " + table + " WHERE " + IdField + " = @id" +
                    " AND " + idFieldJoin + " = @idJoin";
            try { command.ExecuteNonQuery(); }
            catch (SqlException ex)
            {
                item.setValid(false);
                item.setErrorMessage(ex.Message);
            }
            Connection.Close();
        }

    }
}
