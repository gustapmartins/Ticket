using Microsoft.EntityFrameworkCore;
using Ticket.Data;

namespace Ticket.Ioc;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder ApplyMigrations(this IApplicationBuilder app)
    {
        // Obter o escopo de serviço
        using (var scope = app.ApplicationServices.CreateScope())
        {
            // Obtém o contexto do banco de dados dentro do escopo
            var dbContext = scope.ServiceProvider.GetRequiredService<TicketContext>();

            // Aplica as migrações pendentes
            dbContext.Database.Migrate();
        }

        return app;
    }
}
