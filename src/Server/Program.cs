using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Qx.ApiFx.Core;

namespace Qx.Workflow.Processor.Processor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.AddPack();

            var app = builder.Build();
            app.UsePack();

            app.Run();
        }
    }
}