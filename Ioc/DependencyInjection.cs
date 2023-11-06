using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ServiceStack.Redis;
using Swashbuckle.AspNetCore.Filters;
using Ticket.Data;
using Ticket.ExceptionFilter;
using Ticket.Interface;
using Ticket.Model;
using Ticket.Repository.Dao;
using Ticket.Repository.EfCore;
using Ticket.Service;

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

        services.AddIdentity<Users, IdentityRole>()
       .AddEntityFrameworkStores<TicketContext>()
       .AddDefaultTokenProviders();


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

        services.AddScoped<ICachingService, CachingService>();

        services.AddSingleton<IMessagePublisher>(sp => new MessagePublisher(configuration["RabbitMQ:Host"]));

        services.AddSingleton<IRedisClient>(c => new RedisClient(configuration["Redis:Host"], int.Parse(configuration["Redis:Port"])));

        Authentication.ConfigureAuth(services);
    }
}
