using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ticket.Data;
using Ticket.ExceptionFilter;
using Ticket.Interface;
using Ticket.Model;
using Ticket.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("TicketConnection");

builder.Services.AddDbContext<TicketContext>(opts =>
    opts.UseLazyLoadingProxies().UseNpgsql(connectionString));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<TicketContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddControllers(opts =>
{
    opts.Filters.Add<NotImplExceptionFilterAttribute>();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
