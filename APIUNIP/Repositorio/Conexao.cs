using APIUNIP.Models;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace APIUNIP.Repositorio
{
    public class Conexao
    {
        private readonly string _connectionString = "Server=bancounip.c926ki62op9g.us-east-2.rds.amazonaws.com;Port=3306;Database=db_unip;User ID=admin;Password=JQ687mwa25;";
        private readonly MySqlConnection _connection;

        public Conexao()
        {
            _connection = new MySqlConnection(_connectionString);
            _connection.Open();
            AtualizarFuso();
        }

        public void Close()
        {
            _connection.Clone();
            _connection.Dispose();
        }
        public MySqlCommand Comando()
        {
            return _connection.CreateCommand();
        }
        public void AtualizarFuso()
        {
            try
            {

                MySqlCommand command = Comando();

                string StrSql = $"SET time_zone = '-03:00';";

                command.CommandText = StrSql;

                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
        }
    }
}
