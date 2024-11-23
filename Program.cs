
using CA02_ASP.NET_Core.Data;
using CA02_ASP.NET_Core.Data.DTO;

using CA02_ASP.NET_Core.Data.Entity;
using CA02_ASP.NET_Core.Data.Services;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;

namespace CA02_ASP.NET_Core
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





            TypeAdapterConfig<BookDTO, BookEntity>.NewConfig().Ignore("id");
            TypeAdapterConfig<RentalDTO, RentalEntity>.NewConfig().Ignore("id");
            TypeAdapterConfig<UserDTO, UserDTO>.NewConfig().Ignore("id");

            builder.Services.AddDbContext<Context>(options => options.library_rental_system(builder.Configuration.GetConnectionString("dbConnection")));
            builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));





            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
