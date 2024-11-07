using Cinema.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Cinema
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; set; }
        public IConfiguration Configuration { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceColection = new ServiceCollection();
            //serviceColection.AddTransient<RoomManagement>();
            serviceColection.AddScoped<CinemaContext>();
            ServiceProvider = serviceColection.BuildServiceProvider();
            //ServiceProvider.GetRequiredService<RoomManagement>().Show();
        }
    }

}
