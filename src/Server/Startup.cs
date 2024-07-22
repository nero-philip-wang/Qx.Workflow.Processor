using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Qx.ApiFx.Auth;
using Qx.ApiFx.Core;
using System.Linq;

namespace Qx.Workflow.Processor.Processor
{
    public class Startup
    {
        private PackManager packManager;
        private IConfiguration Configuration { get; }
        private IHostEnvironment Env { get; }

        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
            packManager = new PackManager();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            packManager.AddPacks(services, Configuration, Env);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            packManager.UsePack(app);
        }
    }
}