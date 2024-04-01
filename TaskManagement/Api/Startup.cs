using Api.Constants;
using Api.Filters;
using Api.Middlewares;
using Api.Validators;
using Application.Interfaces;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Persistance.Interfaces;
using Persistance.Repositories;

namespace Api
{
    public class Startup
    {
        private const string JwtPublicKey = "JwtPublicKey";
        private const string AuthTokenIssuer = "AuthTokenIssuer";
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

            string jwtPublicKey, authTokenIssuer;

            ConfigureAuthorization(services, out jwtPublicKey, out authTokenIssuer);

            services.AddSingleton(provider => Configuration);
            services.AddTransient<IJwtTokenValidator>(jwtTokenValidator => new JwtTokenValidator(jwtPublicKey, authTokenIssuer));

            ConfigureDatabaseConnection(services);

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IUserService, UserService>();
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

        private void ConfigureAuthorization(IServiceCollection services, out string jwtPublicKey, out string authTokenIssuer)
        {
            services.AddAuthorization(options => options.AddPolicy(Policy.BearerOnly, policy => policy.Requirements.Add(new BearerTokenAttribute())));

            jwtPublicKey = Configuration.GetValue<string>(JwtPublicKey);
            authTokenIssuer = Configuration.GetValue<string>(AuthTokenIssuer);
        }

        private void ConfigureDatabaseConnection(IServiceCollection services)
        {
            string conString = Configuration.GetConnectionString(dbConnection);
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer(conString);
            var dbContext = new DatabaseContext(optionsBuilder.Options);
            if (dbContext.Database.ProviderName != InMemoryDatabase)
            {
                dbContext.Database.Migrate();
            }

            dbContext.Database.EnsureCreated();
            services.AddEntityFrameworkSqlServer()
              .AddDbContext<DatabaseContext>(options => options.UseSqlServer(conString), ServiceLifetime.Transient);
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


            app.UseMiddleware<AuthorizationMiddleware>();

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseRouting();
            //app.UseAuthorization();
            app.UseEndpoints(builder => builder.MapControllers());
        }
    }
}
