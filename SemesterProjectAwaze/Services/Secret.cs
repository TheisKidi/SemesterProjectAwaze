﻿namespace SemesterProjectAwaze.Services
{
    public static class Secret
    {
        public static string GetConnectionString
        {
            get
            {
                return "Data Source=mssql17.unoeuro.com;Initial Catalog=hasseh_com_db_awaze;User ID=hasseh_com;Password=9dHEhAnRmgyfB4e2zb63;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            }
        }
    }
}
