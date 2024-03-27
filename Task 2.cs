using System;
using System.Data;
using System.Data.SqlClient;

class Program
{ 
    static void Main(string[] args)
    {
        string connectionString = "Data Source = DESKTOP-C3E8P2C; database = StudentDataBase; Integrated Security = true;";
        string query = "SELECT * from Students";

        //Create a Data Adapter and Dataset

        using (SqlConnection connection = new SqlConnection(
            connectionString))
        {
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataSet dataSet = new DataSet();

            //Fill the DataSet with the Data from Students Table

            adapter.Fill(dataSet, "Students");

            //Display Data from the DataSet

            foreach(DataRow row in dataSet.Tables["Students"].Rows)
            {
                Console.WriteLine("Name: " + row["FirstName"] + ", Age:" + row["Age"]);
            }

            //Modifying data within the dataSet

            foreach(DataRow row in dataSet.Tables["Students"].Rows)
            {
                //Change the age of each student

                row["Age"] = (int)row["Age"] + 1;
                Console.WriteLine("Name: " + row["FirstName"] + ", Age: " + row["Age"]);
            }

            //update changes back to the database

            using (SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter))
            {
                adapter.Update(dataSet, "Students");
            }

            //Filter the data with a specific age using DataView

            DataView dataView = new DataView(dataSet.Tables["Students"]);
            dataView.RowFilter = "Age = 32";

            //display filtered data

            foreach(DataRowView rowView in dataView)
            {
                DataRow row = rowView.Row;
                Console.WriteLine("Name: " + row["FirstName"] + ", Age: " + row["Age"]);
            }
        }


    }
}