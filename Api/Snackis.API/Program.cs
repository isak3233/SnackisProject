using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



using Snackis.Application.Interfaces;
using Snackis.Application.Services;

using Snackis.Domain.Entities;
using Snackis.Domain.Interfaces;
using Snackis.Infrastructure.Data;
using Snackis.Infrastructure.Repositories;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();


        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();

        // DbContext
        builder.Services.AddDbContext<SnackisDbContext>(options =>
        {
            options.UseSqlServer(
                builder.Configuration.GetConnectionString(
                    "DefaultConnection"));
        });

        // Identity
        builder.Services
            .AddIdentity<SnackisUser, IdentityRole>(options =>
            {
                options.Stores.SchemaVersion =
                    IdentitySchemaVersions.Version3;
            })
            .AddEntityFrameworkStores<SnackisDbContext>()
            .AddDefaultTokenProviders();


        builder.Services.AddHttpContextAccessor();


        builder.Services.AddScoped<IAuthService, AuthService>();

        // Repositories
        builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
        builder.Services.AddScoped<IPostRepository, PostRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IMessageRepository, MessageRepository>();

        // Services
        builder.Services.AddScoped<ISubjectService, SubjectService>();
        builder.Services.AddScoped<IPostService, PostService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IMessageService, MessageService>();

        var app = builder.Build();


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