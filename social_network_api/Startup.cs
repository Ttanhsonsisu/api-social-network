using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using social_network_api.Data;
using social_network_api.DataAccess.WebUser;
using social_network_api.Helpers;
using social_network_api.Interfaces;
using social_network_api.Interfaces.AddFriend;
using social_network_api.Interfaces.Comment;
using social_network_api.Interfaces.Info;
using social_network_api.Interfaces.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace social_network_api
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
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
            .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddControllers();

            string key = Configuration["SecretToken"];

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key))
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("WebAdminUser", policy => policy.RequireAssertion(context =>
                                    context.User.HasClaim(c => c.Type == ClaimTypes.NameIdentifier && c.Value == "web_admin")
                ));

                options.AddPolicy("AppUser", policy => policy.RequireAssertion(context =>
                                    context.User.HasClaim(c => c.Type == ClaimTypes.NameIdentifier && c.Value == "member")
                ));
            });

            services.AddSingleton<IJwt>(new Authen(key));

            // 
            services.AddScoped<ILoggingHelpers, LoggingHelPers>();
            services.AddScoped<ICommonFunctions , CommonFunctions>();
            services.AddScoped<IComment, CommentDataAccess>();
            services.AddScoped<IFriend, FriendDataAccess>();
            services.AddScoped<IUser, UserDataAccess>(); 
            services.AddScoped<IPost, PostDataAccess>();
            services.AddScoped<IProfile, ProfileDataAccess>();
            services.AddScoped<IEducation, EducatonDataAccess>();


            // add http Client test fetch api
            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCors("AllowOrigin");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


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
