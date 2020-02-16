
using FootballLeagueMvcProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FootballLeagueMvcProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {

                    var roleManager = serviceProvider.
        GetRequiredService<RoleManager<ApplicationRole>>();
                    MyIdentityInitializer.SeedRoles(roleManager);
                    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    MyIdentityInitializer.SeedUsers(userManager);
                }
                catch
                {

                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
