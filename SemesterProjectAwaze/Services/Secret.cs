namespace SemesterProjectAwaze.Services
{
    public static class Secret
    {
        public static string GetConnectionString
        {
            get
            {
                return "Data Source=mssql17.unoeuro.com;User ID=hasseh_com;Password=********;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            }
        }
    }
}
