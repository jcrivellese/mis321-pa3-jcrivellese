using API.interfaces;
using API.database;
namespace API
{
        public class Song
    {
        //auto implemented properties for Song
        public int ID {get; set;}
        public string Title {get; set;}
        public string Artist {get; set;}
        public DateTime DateAdded {get; set;}
        public bool Favorited {get; set;}
        public bool Deleted {get; set;}
        private static int MAX_ID{get; set;}

        //constructor for a new Song
        public Song(string title, string artist){
            this.ID = MAX_ID + 1;
            this.Title = title;
            this.Artist = artist;
            DateAdded = DateTime.Now;
            Favorited = false;
            Deleted = false;
            MAX_ID++;

        }
        //ToString method to print out song details
        public string ToString(){
            return Title + "\t" + Artist + "\t" + DateAdded + "\t" + Favorited;
        }
    }
}