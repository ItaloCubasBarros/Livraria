using System;
using System.Diagnostics;

using app.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>() // Usar AuthDbContext para Identity
    .AddDefaultTokenProviders();



builder.Services.AddAuthorization();





builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

///// Inicia o projeto React
//StartReactProject();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseRouting();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();

//void StartReactProject()
//{
//    var startInfo = new ProcessStartInfo
//    {
//        FileName = "cmd.exe",
//        RedirectStandardInput = true,
//        UseShellExecute = false
//    };

//    using (var process = Process.Start(startInfo))
//    {
//        using (var sw = process.StandardInput)
//        {
//            if (sw.BaseStream.CanWrite)
//            {
//                // Caminho para o diret�rio do seu aplicativo React
//                sw.WriteLine("cd view");

//                // Comando para iniciar o aplicativo React
//                sw.WriteLine("npm start");
//            }
//        }
//    }
//}
