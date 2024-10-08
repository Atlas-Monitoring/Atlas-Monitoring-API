﻿using Atlas_Monitoring.Core.Infrastructure.DataBases;
using Microsoft.EntityFrameworkCore;

namespace Atlas_Monitoring
{
    public static class EnsureMigration
    {
        public static void EnsureMigrationOfContext<T>(this IApplicationBuilder app) where T : DbContext
        {
            var context = app.ApplicationServices.GetService<T>();
            context.Database.Migrate();
        }
    }
}
