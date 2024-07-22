using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace Qx.Workflow.Processor
{
    public class AppDbContextPack : ApiFx.EFCore.AppDbContextPack<Qx.Workflow.Processor.AppDbContext>
    {
        public AppDbContextPack(IConfiguration configuration, IHostEnvironment env) : base(configuration, env)
        {
        }

        protected override DbContextOptionsBuilder UseServe(DbContextOptionsBuilder builder)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

            builder.UseNpgsql("Host=127.0.0.1;Port=5432;Database=wf;Username=postgres;Password=aaaa1234;");
            return builder;
        }
    }
}