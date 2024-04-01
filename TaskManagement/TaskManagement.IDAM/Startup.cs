using Authorization.Data;
using Authorization.Data.Interfaces;
using Authorization.Data.Utitlities;
using Microsoft.EntityFrameworkCore;
using TaskManagement.IDAM.Interfaces;
using TaskManagement.IDAM.Repositories;
using TaskManagement.IDAM.Services;

namespace TaskManagement.IDAM
{
    public class Startup
    {
        private const string AuthTokenIssuer = "AuthTokenIssuer";
        private const string EnableAuthorization = "EnableAuthorization";
        private const string dbConnection = "DbConnection";
        private const string InMemoryDatabase = "Microsoft.EntityFrameworkCore.InMemory";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            ConfigureBasicServices(services);

            //string jwtPublicKey, authTokenIssuer;

            //ConfigureAuthorization(services, out jwtPublicKey, out authTokenIssuer);

            //services.AddSingleton(provider => Configuration);
            //services.AddTransient<IJwtTokenValidator>(jwtTokenValidator => new JwtTokenValidator(jwtPublicKey, authTokenIssuer));

            ConfigureDatabaseConnection(services);
            services.AddHttpClient<UserCreationProxy>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7011");
                // Configure other settings as needed
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };
            });
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserCreationProxy, UserCreationProxy>();
            services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
        }

        private static void ConfigureBasicServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc(l => l.EnableEndpointRouting = false)
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Latest);
            services.AddSwaggerGen();
            services.AddControllers();
            services.AddLogging();

        }

        private void ConfigureAuthorization(IServiceCollection services, out string authTokenIssuer)
        {
            //jwtPublicKey = Configuration.GetValue<string>(JwtPublicKey);
            authTokenIssuer = Configuration.GetValue<string>(AuthTokenIssuer);
        }

        private void ConfigureDatabaseConnection(IServiceCollection services)
        {
            string conString = Configuration.GetConnectionString(dbConnection);
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer(conString);
            var dbContext = new DataContext(optionsBuilder.Options);
            if (dbContext.Database.ProviderName != InMemoryDatabase)
            {
                dbContext.Database.Migrate();
            }

            dbContext.Database.EnsureCreated();
            services.AddEntityFrameworkSqlServer()
              .AddDbContext<DataContext>(options => options.UseSqlServer(conString), ServiceLifetime.Transient);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            //if (Configuration.GetValue<bool>(EnableAuthorization))
            //{
            //    app.UseMiddleware<AuthorizationMiddleware>();
            //}

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseRouting();
            //app.UseAuthorization();
            app.UseEndpoints(builder => builder.MapControllers());
        }
    }
}