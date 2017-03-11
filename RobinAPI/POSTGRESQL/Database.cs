using Npgsql;
using System.Data;

namespace RobinAPI.POSTGRESQL
{
    public class Database
    {
        private const string host = "192.168.254.40";
        private const string username = "postgres";
        private const string password = "password";
        private const string database = "postgres";

        private NpgsqlConnection databaseConnection;

        // Create a connection to the database
        public Database()
        {
            databaseConnection = new NpgsqlConnection($"Host={host};Username={username};Password={password};Database={database}");
            databaseConnection.Open();
        }

        // Cleanup and disconnect.
        // Lol I doubt this does anything but whatever.
        ~Database()
        {
            databaseConnection.Close();
        }

        public NpgsqlDataReader Query(string command)
        {
            var ownershipPassed = false;

            try
            {
                NpgsqlCommand databaseCommand = new NpgsqlCommand(command, databaseConnection);
                NpgsqlDataReader dataReader = databaseCommand.ExecuteReader(CommandBehavior.CloseConnection);
                ownershipPassed = true;

                return dataReader;
            }
            finally
            {
                if (!ownershipPassed)
                {
                    databaseConnection.Dispose();
                }
            }
        }
    }
}