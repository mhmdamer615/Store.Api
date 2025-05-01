
using Domain.Contract;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Presistence.Data;
using Services.Abstraction;
using Services;
using System.Reflection.Metadata;
using Services.Mapping_Profiles;
using Store.Api.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Store.Api.Factories;
using StackExchange.Redis;
using Presistence.Identity;
using Microsoft.AspNetCore.Identity;
using Domain.Entities.Identity;
using Presistence.Repositories;
using Shared.IdentityDtos;
using Store.Api.Extensions;

namespace Store.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.




            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddCoreServices(builder.Configuration);
            builder.Services.AddPresentationServices();



            builder.Services.AddAutoMapper(x => x.AddProfile(new ProductProfile()));



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            await app.seedDbAsync();

            app.UseMiddleware<GlobalErrorHandlingMiddleware>();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();



            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

    }
}
