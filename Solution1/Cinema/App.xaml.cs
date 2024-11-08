using AutoMapper;
using Cinema.Helper;
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
        public static IMapper Mapper { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceColection = new ServiceCollection();
            //serviceColection.AddTransient<RoomManagement>();
            serviceColection.AddScoped<CinemaContext>();
            ServiceProvider = serviceColection.BuildServiceProvider();
            //ServiceProvider.GetRequiredService<RoomManagement>().Show();
            // Cấu hình AutoMapper
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();  // Thêm Profile vào cấu hình
            });

            Mapper = configuration.CreateMapper();  // Tạo đối tượng IMapper
        }
    }

}
