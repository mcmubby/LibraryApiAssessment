using System;
using LibraryApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryApi.Helper
{
    public static class DbMigration
    {
        public static void MigrateDatabaseContext(IServiceProvider svp)
        {
            var applicationDbContext = svp.GetRequiredService<ApplicationDbContext>();
            applicationDbContext.Database.Migrate();
        }
    }
}