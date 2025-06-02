using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Asn1.Ocsp;
using wellmanage.application.Interfaces;
using wellmanage.application.Services;
using wellmanage.data.Interfaces;
using wellmanage.hrm.client.Extensions;
using wellmanage.hrm.client.Service_Container;

namespace wellmanage.hrm.client
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Create a configuration object (reading from appsettings.json)
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Set up dependency injection container
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConfiguration>(configuration)
                .AddDatabaseContexts(configuration)
                .AddAuthenticationServices(configuration)
                .AddEmailServices(configuration)
                .AddOtherServicesWithRepositories(configuration)
                .AddAutoMapper(typeof(Form1), typeof(EmployeeService))
                .AddLogging()
                .BuildServiceProvider();

            ServiceContainer.Services = serviceProvider;

            // Get the required services from DI container
            var userService = serviceProvider.GetRequiredService<IUserService>();
            var tokenService = serviceProvider.GetRequiredService<ITokenService>();
            var emailService = serviceProvider.GetRequiredService<IEmailService>();
            var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();

            // Run the WinForms application
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new HomeForm(userService));
        }
    }
}