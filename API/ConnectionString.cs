namespace API
{
    public class ConnectionString
    {
        public string cs {get; set;}

        public ConnectionString(){
            string server = "r4wkv4apxn9btls2.cbetxkdyhwsb.us-east-1.rds.amazonaws.com";
            string database = "b5gxyj6mly9agtop";
            string port = "3306";
            string userName = "drbrqleoiuey2ji3";
            string password = "	aujziqvcjdsgvwdl";

            cs = $@"server = {server};user = {userName};database = {database};port = {port};password = {password};";
        }
    }
}