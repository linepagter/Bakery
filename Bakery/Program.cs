using Bakery.Data;
using Microsoft.EntityFrameworkCore;

namespace Bakery
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            builder.Services.AddDbContext<MyDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            app.Run();
        }
    }
}
