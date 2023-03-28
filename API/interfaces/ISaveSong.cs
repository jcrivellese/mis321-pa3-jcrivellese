namespace API.interfaces
{
    public interface ISaveSong
    {
        public void CreateSong(Song song);
        public void Save(int id, Song song);
    }
}