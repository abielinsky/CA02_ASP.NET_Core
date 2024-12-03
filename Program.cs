using CA02_ASP.NET_Core.Data;
using CA02_ASP.NET_Core.Data.DTO;

using CA02_ASP.NET_Core.Data.Entity;
using CA02_ASP.NET_Core.Data.Services;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CA02_ASP.NET_Core.Middleware;

namespace CA02_ASP.NET_Core
{
    public class Program
    {
        public static void Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement{
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }});
            });

            TypeAdapterConfig<BookDTO, BookEntity>.NewConfig().Ignore("id");
            TypeAdapterConfig<RentalDTO, RentalEntity>.NewConfig().Ignore("id");
            TypeAdapterConfig<UserDTO, UserDTO>.NewConfig().Ignore("id");

            // next line is for remote db connection
            builder.Services.AddDbContext<Context>(options => options.UseMySQL(builder.Configuration.GetConnectionString("dbConnection")));

            // next line is for local db connection
            // builder.Services.AddDbContext<Context>(options => options.UseMySQL(builder.Configuration.GetConnectionString("dbConnectionLocal")));

            builder.Services.AddScoped(typeof(ILoginService), typeof(LoginService));
            builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

            #region Jwt Configurations
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]))
                };
            });
            builder.Services.AddAuthorization();
            #endregion


            // Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


                
            var app = builder.Build();

            // API Key Middleware for validating each request
            app.UseMiddleware<ApiKeyMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


// Use CORS Policy
app.UseCors("AllowAll");

            

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
