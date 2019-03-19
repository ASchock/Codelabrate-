using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using TeacherStudentEditor.Data;
using TeacherStudentEditor.Hubs;
using TeacherStudentEditor.Models;

namespace TeacherStudentEditor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.SignIn.RequireConfirmedEmail = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 1;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddSingleton<IEmailSender, NullEmailSender>();

            services.AddSingleton(
                HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.All }));

            services.AddSignalR(options => options.EnableDetailedErrors = true);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, ApplicationDbContext applicationDbContext)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDg3ODZAMzEzNjJlMzMyZTMwQzhDVTlZTHZycGpzNDlNajBNK3lpVGFROTQrOWRVd0h2MUJtc2t3RVlaMD0=");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            UpdateDatabaseMigrations(applicationDbContext);

            SeedRoles(roleManager);

            MapperInit();

            app.UseSignalR(routes =>
            {
                routes.MapHub<EditorHub>("/sessions");
                routes.MapHub<QuestionHub>("/questionupdates");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });
        }

        public void SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync(ApplicationDbContext.AdministratorRoleName).Result)
            {
                roleManager.CreateAsync(new ApplicationRole() { Name = ApplicationDbContext.AdministratorRoleName });
            }
            if (!roleManager.RoleExistsAsync(ApplicationDbContext.TeacherRoleName).Result)
            {
                roleManager.CreateAsync(new ApplicationRole() { Name = ApplicationDbContext.TeacherRoleName });
            }
            if (!roleManager.RoleExistsAsync(ApplicationDbContext.StudentRoleName).Result)
            {
                roleManager.CreateAsync(new ApplicationRole() { Name = ApplicationDbContext.StudentRoleName });
            }
        }

        public void UpdateDatabaseMigrations(ApplicationDbContext applicationDbContext)
        {
            applicationDbContext.Database.Migrate();
        }

        public void MapperInit()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Course, CreateCourseViewModel>().ReverseMap();
            });
        }
    }
}
