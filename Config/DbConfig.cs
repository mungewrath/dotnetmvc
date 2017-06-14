namespace dotnetmvc.Config
{
    public class DbConfig
    {
        // Postgres connection string in the format "server=server.aws.com;user id=cooluser;password=highlysecure;database=attachmenttable"
        public string ConnectionString { get; set; }
    }
}