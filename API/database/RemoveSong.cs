
using MySql.Data.MySqlClient;
using API.interfaces;
namespace API.database
{
    public class RemoveSong : IDeleteSong
    {
        public static void DropSongTable(){
            ConnectionString myConn = new ConnectionString();
            string cs = myConn.cs;

            using var conn = new MySqlConnection(cs);
            conn.Open();

            string stm = @"DROP TABLE IF EXISTS songs";

            using var cmd = new MySqlCommand(stm, conn);
            cmd.ExecuteNonQuery();
        }

        //method for the DELETE controller
        public void Delete(int id){
            ConnectionString myConn = new ConnectionString();
            string cs = myConn.cs;

            using var conn = new MySqlConnection(cs);
            conn.Open();

            string stm = "UPDATE songs SET Deleted = @newValue WHERE ID = @idValue";

            using var cmd = new MySqlCommand(stm, conn);


            cmd.Parameters.AddWithValue("@newValue", true);
            cmd.Parameters.AddWithValue("@idValue", id);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}