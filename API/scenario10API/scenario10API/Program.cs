
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using scenario10API.models;
using scenario10API.Repos.Reports;
using scenario10API.Repos.Speciess;
using System.Security.Claims;
using System.Text;

namespace scenario10API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.AllowAnyOrigin()
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                                  });
            });
            #endregion

            #region Connection with sql
            var connectionString = builder.Configuration.GetConnectionString("AppNest_ConString");
            builder.Services.AddDbContext<MyDBContext>(options =>
                options.UseSqlServer(connectionString));
            #endregion

            builder.Services.AddScoped<IReportRepo,ReportRepo>();
            builder.Services.AddScoped<ISpeciesRepo, SpeciesRepo>();

            #region Identity Manager

            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;

                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<MyDBContext>();

            #endregion

            #region Authentication

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Super";
                options.DefaultChallengeScheme = "Super";
            })
                .AddJwtBearer("Super", options =>
                {
                    string keyString = builder.Configuration.GetValue<string>("SecretKey") ?? string.Empty;
                    var keyInBytes = Encoding.ASCII.GetBytes(keyString);
                    var key = new SymmetricSecurityKey(keyInBytes);

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });

            #endregion

            #region Authorization

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("User", policy => policy
                    .RequireClaim(ClaimTypes.Role, "User"));
            });

            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            var staticFilesPath = Path.Combine(Environment.CurrentDirectory, "Images");
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(staticFilesPath),
                RequestPath = "/Images"
            });
            app.Run();
        }
    }
}