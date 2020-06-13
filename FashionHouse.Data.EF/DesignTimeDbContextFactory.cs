using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace FashionHouse.Data.EF
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        private string _connectionString
        {
            get
            {
                return "data source=localhost;initial catalog=FashionHouse;Integrated Security=True;";
            }
        }

        public MyContext CreateDbContext(string[] args)
        {
            return new MyContext(_connectionString);
        }
    }
}
