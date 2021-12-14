using System.Security.Claims;
using CarSharing_Client.Authentication;
using CarSharing_Client.Data;
using CarSharing_Client.Data.Impl;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace CarSharing_Client
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<IVehicleService, VehicleWebService>();
            services.AddScoped<IListingService, ListingWebService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILeaseService, LeaseWebService>();
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            services.AddScoped<IMobilePayWebService, MobilePayWebService>();
            
            
            services.AddAuthorization(option =>
            {
                option.AddPolicy("MustBeLoggedIn", a =>
                    a.RequireAuthenticatedUser());
                
                option.AddPolicy("MustBeCustomer", a =>
                    a.RequireAuthenticatedUser().RequireAssertion(context =>
                    {
                        var levelClaim = context.User.FindFirst(claim => claim.Type.Equals("AccessLevel"));
                        if (levelClaim == null) return false;
                        return int.Parse(levelClaim.Value) == 0;
                    }));
                
                option.AddPolicy("MustBeAdmin", a =>
                    a.RequireAuthenticatedUser().RequireAssertion(context =>
                    {
                        var levelClaim = context.User.FindFirst(claim => claim.Type.Equals("AccessLevel"));
                        if (levelClaim == null) return false;
                        return int.Parse(levelClaim.Value) == 3;
                    }));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}