using ASPNETCOREAPP.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace WebApplication1
{
    public class Startup
    {
        private IConfiguration _Configuration;
        public Startup(IConfiguration configuration)
        {
            _Configuration = configuration;
        }





        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            
            services.Configure<CookiePolicyOptions>(options =>
            {

                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                
            });
            
            


            services.AddDbContextPool<DatabaseContext>(options =>
            options.UseSqlServer(_Configuration.GetConnectionString("EmployeeDBConnection")));

            //services.AddIdentity<ApplicationUserc, IdentityRole>(config =>
            //{
            //    config.SignIn.RequireConfirmedEmail = true;
            //}
            //).AddEntityFrameworkStores<DatabaseContext>().AddErrorDescriber<CustomIdentityErrorDescriber>();

            services.AddIdentity<ApplicationUserc, IdentityRole>().AddEntityFrameworkStores<DatabaseContext>().AddErrorDescriber<CustomIdentityErrorDescriber>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.User.RequireUniqueEmail = true;

            //});
            
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
