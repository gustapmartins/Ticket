using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.OpenApi.Models;
using Ticket.Repository.EfCore;
using Ticket.ExceptionFilter;
using Ticket.Repository.Dao;
using ServiceStack.Redis;
using Ticket.Interface;
using Newtonsoft.Json;
using Ticket.Service;
using Ticket.Data;
using Ticket.Model;

namespace Ticket.Configure;

public class DependencyInjection
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("TicketConnection");

        services.AddDbContext<TicketContext>(opts =>
            opts.UseLazyLoadingProxies().UseNpgsql(connectionString));

        services.AddControllers().AddNewtonsoftJson();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("v1", new OpenApiSecurityScheme
            {
                Description = "Description project",
                In = ParameterLocation.Header,
                Name = "Tickets",
                Type = SecuritySchemeType.ApiKey,
                
            });

            c.OperationFilter<SecurityRequirementsOperationFilter>();
        });

        services.AddHttpContextAccessor();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddControllers(opts =>
        {
            opts.Filters.Add<NotImplExceptionFilterAttribute>();
        });

        services.AddCors();

        services.AddIdentity<Users, IdentityRole>()
       .AddEntityFrameworkStores<TicketContext>()
       .AddDefaultTokenProviders();

        services.AddSingleton<IMessagePublisher, MessagePublisher>();

        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<IEmailService, EmailService>();

        services.AddScoped<ICachingService, CachingService>();

        services.AddScoped<IShowService, ShowService>();
        services.AddTransient<IShowDao, ShowDaoComEfCore>();

        services.AddScoped<ICategoryService, CategoryService>();
        services.AddTransient<ICategoryDao, CategoryDaoComEfCore>();

        services.AddScoped<ITicketService, TicketService>();
        services.AddTransient<ITicketDao, TicketDaoComEfCore>();

        services.AddScoped<ICartService, CartService>();
        services.AddTransient<ICartDao, CartDaoComEfCore>();

        services.AddScoped<IFeatureToggleService, FeatureToggleService>();
        services.AddTransient<IFeatureToggleDao, FeatureToggleEfCore>();

        services.AddScoped<ICachingService, CachingService>();

        services.AddSingleton<IRedisClient>(c => new RedisClient(configuration["Redis:Host"], int.Parse(configuration["Redis:Port"])));

        JsonConvert.DefaultSettings = () => new JsonSerializerSettings
        {
            Formatting = Newtonsoft.Json.Formatting.Indented,
            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        };

        Authentication.ConfigureAuth(services);
    }
}
