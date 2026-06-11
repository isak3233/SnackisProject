using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Snackis.Application.BlazorInterfaces;
using Snackis.Application.BlazorServices;
using Snackis.Domain.BlazorInterfaces;
using Snackis.Domain.Entities;
using Snackis.Infrastructure.BlazorRepositories;
using Snackis.Infrastructure.Data;
using Snackis.Presentation.Components;
using Snackis.Presentation.Components.Account;


public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();



        builder.Services.AddDbContext<SnackisDbContext>(options =>
        {
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        builder.Services
            .AddIdentity<SnackisUser, IdentityRole>(options =>
            {
                options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
            })
            .AddEntityFrameworkStores<SnackisDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        builder.Services.AddHttpClient("SnackisApi", client =>
        {
            client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"] + "/api/");
            client.DefaultRequestHeaders.Add("Api-Key", builder.Configuration["Secret-Auth-Key"]);
        });



        // Authentication / Authorization
        builder.Services.AddCascadingAuthenticationState();


        builder.Services.AddScoped<IdentityRedirectManager>();

        builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

        builder.Services.AddSingleton<IEmailSender<SnackisUser>, IdentityNoOpEmailSender>();

        //Repos
        builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
        builder.Services.AddScoped<IPostRepository, PostRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IMessageRepository, MessageRepository>();
        builder.Services.AddScoped<IAdminRepository, AdminRepository>();

        //Services
        builder.Services.AddScoped<ISubjectService, SubjectServiceB>();
        builder.Services.AddScoped<IUserService, UserServiceB>();
        builder.Services.AddScoped<IPostService, PostServiceB>();
        builder.Services.AddScoped<IMessageService, MessageServiceB>();
        builder.Services.AddScoped<IAdminService, AdminServiceB>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            app.UseHsts();
        }
        app.UseStaticFiles();
        app.UseStatusCodePagesWithReExecute(
            "/not-found",
            createScopeForStatusCodePages: true);

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseAntiforgery();

        app.MapStaticAssets();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.MapAdditionalIdentityEndpoints();;


        app.Run();
    }
}