using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.EntityFrameworkCore;
using TouhouData;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using TouhouData.Context;

namespace TouhouAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            services.AddCors(options => options.AddPolicy("Cors", builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                    //.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                })
            );

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("43e4dbf0-52ed-4203-895d-42b586496bd4"));

            services.AddAuthentication(options => 
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = signingKey,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = false,//dev env
                    ValidateIssuerSigningKey = true//dev env
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("user", policy => policy.RequireClaim("Store", "user"));
                options.AddPolicy("admin", policy => policy.RequireClaim("Store", "admin"));
            });

            //Uncomment to use in memmory db
            //services.AddDbContext<TouhouContext>(opt => opt.UseInMemoryDatabase("datacontext"));
            //services.AddDbContext<UserContext>(opt => opt.UseInMemoryDatabase("usercontext"));
            //services.AddDbContext<TouhouContext>(opt => opt.UseSqlServer(@"Data Source=LAPTOP-AILPR68G;Initial Catalog=Touhou;Integrated Security=True;Persist Security Info=True;User ID=sa;Password=@oisora17", b => b.MigrationsAssembly("TouhouData")));
            //services.AddDbContext<UserContext>(opt => opt.UseSqlServer(@"Data Source=LAPTOP-AILPR68G;Initial Catalog=TouhouUsers;Integrated Security=True;Persist Security Info=True;User ID=sa;Password=@oisora17", b => b.MigrationsAssembly("TouhouData")));

            services.AddDbContext<TouhouContext>();
            services.AddDbContext<UserContext>();
            services.AddIdentity<IdentityUser, IdentityRole>(options => 
                options.User.RequireUniqueEmail = true
            ).AddEntityFrameworkStores<UserContext>();            

            //services.AddMvc();
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("Cors");

            //app.UseMvc();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
