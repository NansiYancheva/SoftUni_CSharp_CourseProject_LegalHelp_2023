namespace LegalHelp
{
    using Microsoft.EntityFrameworkCore;

    using LegalHelpSystem.Data;
    using LegalHelpSystem.Data.Models;
    using LegalHelpSystem.Web.Infrastructure.Extensions;
    using LegalHelpSystem.Services.Data.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder =
                WebApplication.CreateBuilder(args);

            string connectionString =
                builder.Configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services
                .AddDbContext<LegalHelpDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount =
                    builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
                options.Password.RequireNonAlphanumeric =
                    builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
                options.Password.RequireLowercase =
                    builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
                options.Password.RequireUppercase =
                    builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
                options.Password.RequireDigit =
                    builder.Configuration.GetValue<bool>("Identity:Password:RequireDigit");
                options.Password.RequiredLength =
                    builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
            })
                .AddEntityFrameworkStores<LegalHelpDbContext>();

            builder.Services.AddApplicationServices(typeof(IDocumentService));

            //builder.Services.ConfigureApplicationCookie(cfg =>
            //    {
            //        cfg.LoginPath = "/User/Login";
            //    });

            builder.Services
               .AddControllersWithViews()
               .AddMvcOptions(options =>
               {
                   options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
               });

            WebApplication app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(config =>
            {
                config.MapControllerRoute(
                    name: "ProtectingUrlRoute",
                    pattern: "/{controller}/{action}/{id}/{information}",
                    defaults: new
                    {
                        Controller = "Ticket",
                        Action = "Delete"
                    });
                config.MapDefaultControllerRoute();
                config.MapRazorPages();
            });

                //app.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Home}/{action=Index}/{id?}");
                //app.MapDefaultControllerRoute();
                //app.MapRazorPages();

                app.Run();
            }
    }
    }