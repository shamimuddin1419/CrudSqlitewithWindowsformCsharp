using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplierMgtApp
{
  public  class DataAccess
    {
      
        static string _ConnectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
       


        static SQLiteConnection _Connection = null;
        public static SQLiteConnection Connection
        {
            get
            {
                if (_Connection == null)
                {
                    _Connection = new SQLiteConnection(_ConnectionString);
                    _Connection.Open();

                    return _Connection;
                }
                else if (_Connection.State != System.Data.ConnectionState.Open)
                {
                    _Connection.Open();

                    return _Connection;
                }
                else
                {
                    return _Connection;
                }
            }
        }

        public static DataSet GetDataSet(string sql)
        {
            SQLiteCommand cmd = new SQLiteCommand(sql, Connection);
            SQLiteDataAdapter adp = new SQLiteDataAdapter(cmd);

            DataSet ds = new DataSet();
            adp.Fill(ds);
            Connection.Close();

            return ds;
        }

        public static DataTable GetDataTable(string sql)
        {
            Console.WriteLine(sql);
            DataSet ds = GetDataSet(sql);

            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }

        public static int ExecuteSQL(string sql)
        {
            SQLiteCommand cmd = new SQLiteCommand(sql, Connection);
            return cmd.ExecuteNonQuery();
        }
    }
}
