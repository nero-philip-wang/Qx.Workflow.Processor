using Microsoft.EntityFrameworkCore;
using Qx.ApiFx.Core;
using System.Collections.Generic;
using System.Text.Json;

namespace Qx.Workflow.Processor
{
    public class AppDbContext : Qx.ApiFx.EFCore.AppDbContext
    {
        public AppDbContext()
        { }

        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Dept> Depts { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Workflow> Workflows { get; set; }

        public DbSet<WorkflowNode> WorkflowNodes { get; set; }

        public DbSet<WorkflowInstance> WorkflowInstances { get; set; }

        public DbSet<ApprovalLog> ApprovalLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSnakeCaseNamingConvention()
                .UseNpgsql("Host=127.0.0.1;Port=5432;Database=wf;Username=postgres;Password=aaaa1234;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dept>().HasMany(c => c.Members).WithMany();
            modelBuilder.Entity<Role>().HasMany(c => c.Members).WithMany();
            modelBuilder.Entity<User>();

            modelBuilder.Entity<Workflow>().HasMany(c => c.Nodes);
            modelBuilder.Entity<WorkflowNode>()
                .Property(c => c.People)
                .HasConversion(c => c.ToJsonString(), c => c.FromJsonString<NodePeople>());
            modelBuilder.Entity<WorkflowNode>()
                .Property(c => c.NextStep)
                .HasConversion(c => c.ToJsonString(), c => c.FromJsonString<List<WorkflowNextStep>>());

            modelBuilder.Entity<WorkflowInstance>()
                .Property(c => c.Workflow)
                .HasConversion(c => c.ToJsonString(), c => c.FromJsonString<Workflow>());
            modelBuilder.Entity<WorkflowInstance>()
                .Property(c => c.CurrentNode)
                .HasConversion(c => c.ToJsonString(), c => c.FromJsonString<WorkflowNode>());
            modelBuilder.Entity<WorkflowInstance>()
                .OwnsOne(c => c.Promoter);
            modelBuilder.Entity<WorkflowInstance>()
                .OwnsOne(c => c.PromoterDept);

            modelBuilder.Entity<ApprovalLog>()
                .Property(c => c.ExtraData)
                .HasConversion(c => c.ToJsonString(), c => c.FromJsonString<JsonElement>());

            base.OnModelCreating(modelBuilder);
        }
    }
}