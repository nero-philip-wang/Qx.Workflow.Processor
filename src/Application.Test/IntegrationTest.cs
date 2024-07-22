using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Collections;
using System;
using Qx.ApiFx.Core;
using System.Linq;
using Microsoft.Extensions.Options;

namespace Qx.ApiFx.Core
{
    public abstract class IntegrationTest
    {
        public IntegrationTest()
        {
            AppSetting = new Hashtable()
            {
                ["ConnectionStrings"] = new Hashtable() { },
                ["AspNetPack"] = new Hashtable()
                {
                    ["Core"] = new Hashtable()
                    {
                        ["FilePrefix"] = new[] { "Qx.Workflow.Processor" }
                    }
                }
            };
        }

        public Hashtable AppSetting { get; set; }

        protected (IConfiguration Cfg, IHostEnvironment Env) GenerateEnvCfg()
        {
            IHostEnvironment env = new HostingEnvironment() { EnvironmentName = "Debug" };

            IConfigurationBuilder builder = new ConfigurationBuilder();
            // 读取配置文件
            var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data/config/");
            Directory.CreateDirectory(dir);
            var filepaths = Directory.EnumerateFiles(dir);
            filepaths = filepaths
                .Select(c => Path.GetFileName(c))
                .Where(c => c.Count(d => d == '.') == 1 || c.Contains(env.EnvironmentName))
                .OrderBy(c => c.Length);

            foreach (var fp in filepaths)
            {
                builder.AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data/config/" + fp));
            }
            builder.AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"), false, true);
            // 读取临时配置
            var json = JsonConvert.SerializeObject(AppSetting);
            builder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(json)));

            return (builder.Build(), env);
        }

        protected void TestPack(Action<IServiceCollection> configureServices, Action<IApplicationBuilder> configureApp)
        {
            var ce = GenerateEnvCfg();
            var packManager = new PackManager();
            var b = WebHost.CreateDefaultBuilder()
                .UseStartup<IntegrationTest.StartUp>()
                .ConfigureServices(s =>
                {
                    packManager.AddPacks(s, ce.Cfg, ce.Env);
                    configureServices(s);
                })
                .Configure(builder =>
                {
                    packManager.UsePack(builder);
                    var app = builder.ApplicationServices.GetService<IOptions<ApplicationInfo>>().Value;
                    app.TenantInfo = new TenantInfo() { TenantId = -1000, Title = "单元测试" };
                    configureApp(builder);
                })
                .Build();
            b.Start();
            b.StopAsync().Wait();
        }

        public class StartUp
        {
            public void Configure(IApplicationBuilder app)
            {
            }
        }
    }
}