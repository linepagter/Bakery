using Bakery.Data;
using Bakery.Swagger;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bakery
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.
            builder.Services.AddControllers();
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            builder.Services.AddDbContext<MyDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"))
            );
            
            var app = builder.Build();
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            if (app.Configuration.GetValue<bool>("UseDeveloperExceptionPage"))
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/error");

            app.UseHttpsRedirection();

            //app.UseCors();

            //app.UseAuthorization();

            // Minimal API
            // app.MapGet("/error",
            //     [EnableCors("AnyOrigin")]
            //     [ResponseCache(NoStore = true)] (HttpContext context) =>
            //     {
            //         var exceptionHandler =
            //             context.Features.Get<IExceptionHandlerPathFeature>();
            //
            //         var details = new ProblemDetails();
            //         details.Detail = exceptionHandler?.Error.Message;
            //         details.Extensions["traceId"] =
            //             System.Diagnostics.Activity.Current?.Id
            //             ?? context.TraceIdentifier;
            //         details.Type =
            //             "https://tools.ietf.org/html/rfc7231#section-6.6.1";
            //         details.Status = StatusCodes.Status500InternalServerError;
            //         return Results.Problem(details);
            //     });
            //
            // app.MapGet("/error/test",
            //     [EnableCors("AnyOrigin")]
            //     [ResponseCache(NoStore = true)] () =>
            //     { throw new Exception("test"); });
            //
            // app.MapGet("/cod/test",
            //     [EnableCors("AnyOrigin")]
            //     [ResponseCache(NoStore = true)] () =>
            //         Results.Text("<script>" +
            //                      "window.alert('Your client supports JavaScript!" +
            //                      "\\r\\n\\r\\n" +
            //                      $"Server time (UTC): {DateTime.UtcNow.ToString("o")}" +
            //                      "\\r\\n" +
            //                      "Client time (UTC): ' + new Date().toISOString());" +
            //                      "</script>" +
            //                      "<noscript>Your client does not support JavaScript</noscript>",
            //             "text/html")); //CHANGE

            // Controllers
            //app.MapControllers().RequireCors("AnyOrigin");


            app.Run();
        }
    }
}
