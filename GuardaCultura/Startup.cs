using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using GuardaCultura.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GuardaCultura//linha 69 apagar
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("GuardaCulturaUserConnection")));// BDContextUsers
            
            //vai ser usado esta autorizacao  identityrole(opcoes)
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                //sig in
                options.SignIn.RequireConfirmedAccount = false;//requer autenticação

                //password
                options.Password.RequireDigit = true;//requer numeros
                options.Password.RequireLowercase = true;//requer letras minusculas
                options.Password.RequiredLength = 8;
                //options.Password.RequiredUniqueChars = 6;//6caracteres tem de ser diferentes uns dos outros
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;//requer letras maiusculas

                //lockout
                options.Lockout.AllowedForNewUsers = true;//bloquiar a conta
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);//bloquiar por 30min//aqui
                options.Lockout.MaxFailedAccessAttempts = 5;//maximo de tentativas para bloquiar

                //Utilizador
                //options.User.RequireUniqueEmail
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI();//interface

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddDbContext<GuardaCulturaContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("GuardaCulturaContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            GuardaCulturaContext dbContext,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=index}/{id?}");//pagina inicial
                    //pattern: "{controller=Fotografias}/{action=index}/{id?}");//pagina inicial fotografia
                    //pattern: "{controller=Miradouroes}/{action=index}/{id?}");//pagina inicial Miradouros
                    //pattern: "{controller=Ambiente}/{action=Miradouros}/{id?}");
            endpoints.MapRazorPages();
            });

            SeedData.SeedRolesAsync(roleManager).Wait();
            SeedData.SeedDefaultAdminAsync(userManager).Wait();//cria sempre

            if (env.IsDevelopment())//ver se está em desenvolvimento
            {
                using (var serviceScope = app.ApplicationServices.CreateScope())//cria uma area de acesso a servicos
                {
                    //var dbContext = serviceScope.ServiceProvider.GetService<GuardaCulturaContext>();
                    
                    SeedData.SeedDevData(dbContext);
                    SeedData.SeedDevUsersAsync(userManager).Wait();//so cria se tiver em desenvolvimento
                    SeedData.Populate(dbContext);
                }
            }
        }
    }
}
