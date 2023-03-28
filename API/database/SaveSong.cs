using API.interfaces;
using MySql.Data.MySqlClient;
namespace API.database
{
    public class SaveSong : ISaveSong
    {
        public static void CreateSongTable(){
            ConnectionString myConn = new ConnectionString();
            string cs = myConn.cs;

            using var conn = new MySqlConnection(cs);
            conn.Open();

            string stm = @"CREATE TABLE songs(ID INTEGER PRIMARY KEY AUTO_INCREMENT, Title TEXT, Artist TEXT, DateAdded TEXT, Favorited BOOLEAN, Deleted BOOLEAN)";

            using var cmd = new MySqlCommand(stm, conn);
            cmd.ExecuteNonQuery();
        
        }
        //method for the POST controller
        public void CreateSong(Song song){
            ConnectionString myConn = new ConnectionString();
            string cs = myConn.cs;

            using var conn = new MySqlConnection(cs);
            conn.Open();

            string stm = @"INSERT INTO songs(Title, Artist, DateAdded, Favorited, Deleted) VALUES(@Title, @Artist, @DateAdded, @Favorited, @Deleted)";

            using var cmd = new MySqlCommand(stm,conn);

            cmd.Parameters.AddWithValue("@Title", song.Title);
            cmd.Parameters.AddWithValue("@Artist", song.Artist);
            cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
            cmd.Parameters.AddWithValue("@Favorited", false);
            cmd.Parameters.AddWithValue("@Deleted", false);

            cmd.ExecuteNonQuery();
            conn.Close();

        }
        //method for the PUT controller
        public void Save(int id, Song song){
            ConnectionString myConn = new ConnectionString();
            string cs = myConn.cs;

            using var conn = new MySqlConnection(cs);
            conn.Open();

            string stm = "UPDATE songs SET Title = @Title, Artist = @Artist, Favorited = @Favorited WHERE ID = @idValue";

            using var cmd = new MySqlCommand(stm, conn);


            cmd.Parameters.AddWithValue("@Title", song.Title);
            cmd.Parameters.AddWithValue("@Artist", song.Artist);
            cmd.Parameters.AddWithValue("@Favorited", song.Favorited);
            cmd.Parameters.AddWithValue("@idValue", id);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}