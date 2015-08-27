using System;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Web.Hosting;

internal static class LocalDBHelper
{
    public const string DB_DIRECTORY = "ErrorData";
    public const string DB_DEFAULT_NAME = "Unhandled";

    internal static SqlConnection GetLocalDB(string dbName, out bool exists, bool deleteIfExists = false)
    {
        exists = false;
        try
        {
            string outputFolder = Path.Combine(Path.GetDirectoryName(HostingEnvironment.ApplicationPhysicalPath), DB_DIRECTORY);
            string mdfFilename = dbName + ".mdf";
            string dbFileName = Path.Combine(outputFolder, mdfFilename);
            string logFileName = Path.Combine(outputFolder, String.Format("{0}_log.ldf", dbName));
            // Create Data Directory If It Doesn't Already Exist.
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // If the file exists, and we want to delete old data, remove it here and create a new database.
            if (File.Exists(dbFileName) && deleteIfExists)
            {
                if (File.Exists(logFileName)) File.Delete(logFileName);
                File.Delete(dbFileName);
                CreateDatabase(dbName, mdfFilename);
            }
            // If the database does not already exist, create it.
            else if (!File.Exists(dbFileName))
            {                
                CreateDatabase(dbName, dbFileName);
            }
            else{
                exists = true;
            }

            // Open newly created, or old database.
            string connectionString = String.Format(@"Data Source=(LocalDB)\v11.0;Integrated Security=false;AttachDbFileName={1}", dbName, dbFileName);
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
        catch
        {
            throw;
        }
    }

    internal static SqlConnection GetLocalDB(string dbName, bool deleteIfExists = false)
    {
        bool exists = false;
        return GetLocalDB(dbName, out exists, deleteIfExists);
    }

    public static bool CreateDatabase(string dbName, string dbFileName)
    {
        try
        {
            string connectionString = String.Format(@"Data Source=(LocalDB)\v11.0;Initial Catalog=master;Integrated Security=true");
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();


                DetachDatabase(dbName);

                cmd.CommandText = String.Format("CREATE DATABASE {0} ON (NAME = N'{0}', FILENAME = '{1}')", dbName, dbFileName);
                cmd.ExecuteNonQuery();
            }

            if (File.Exists(dbFileName)) return true;
            else return false;
        }
        catch
        {
            throw;
        }
    }

    public static bool DetachDatabase(string dbName)
    {
        try
        {
            string connectionString = String.Format(@"Data Source=(LocalDB)\v11.0;Initial Catalog=master;Integrated Security=true");
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = String.Format("exec sp_detach_db '{0}'", dbName);
                cmd.ExecuteNonQuery();

                return true;
            }
        }
        catch
        {
            return false;
        }
    }

  
}