using API.interfaces;
using MySql.Data.MySqlClient;
namespace API.database
{
    public class ReadSongData : IGetAllSongs, IGetSong
    {
        public List<Song> GetAllSongs(){
            ConnectionString myConn = new ConnectionString();
            string cs = myConn.cs;

            using var conn = new MySqlConnection(cs);
            conn.Open();

            string stm = "SELECT * FROM songs";
            using var cmd = new MySqlCommand(stm, conn);

            MySqlDataReader reader = cmd.ExecuteReader();
            List<Song> allSongs = new List<Song>();
            while(reader.Read()){
                allSongs.Add(new Song(){ID = reader.GetInt32(0), Title = reader.GetString(1), Artist = reader.GetString(2), DateAdded = DateTime.Parse(reader.GetString(3)), Favorited = reader.GetBoolean(4), Deleted = reader.GetBoolean(5)});
            }
            return allSongs;
        }
        public Song GetSong(int id){
            ConnectionString myConn = new ConnectionString();
            string cs = myConn.cs;

            using var conn = new MySqlConnection(cs);
            conn.Open();

            string stm = "SELECT * FROM songs WHERE ID = @ID";
            using var cmd = new MySqlCommand(stm, conn);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Prepare();
            MySqlDataReader reader = cmd.ExecuteReader();

            reader.Read();
            return new Song(){ID = reader.GetInt32(0), Title = reader.GetString(1), Artist = reader.GetString(2), DateAdded = DateTime.Parse(reader.GetString(3)), Favorited = reader.GetBoolean(4), Deleted = reader.GetBoolean(5)};

        }
    }
}