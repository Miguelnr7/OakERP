using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OakERP.Infrastructure.Persistence;
using Testcontainers.PostgreSql;

namespace OakERP.Api.Tests;

public sealed class ApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
// WebApplicationFactory<Program>: arranca tu API real dentro del proceso de tests.
// IAsyncLifetime: interfaz de xUnit que permite preparar y limpiar cosas de forma asíncrona
//   antes y después de los tests (aquí, encender y apagar el contenedor).
{
    private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder()
        .WithImage("postgres:17")
        .Build();
    // Describe el contenedor de PostgreSQL (misma versión que en el compose.yaml).
    // Aún no está encendido. Testcontainers elige un puerto libre al azar y genera usuario
    //   y contraseña propios: no usa nada de tu .env.

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    // Aquí personalizamos cómo arranca la API en los tests.
    {
        builder.UseEnvironment("Testing");
        // MUY IMPORTANTE: un entorno distinto de "Development". En Development, .NET carga
        //   tus user-secrets, que contienen la cadena de conexión de TU base de datos real, y
        //   los tests podrían escribir (o borrar) en ella. Con "Testing" no se cargan.

        builder.UseSetting("ConnectionStrings:Default", _postgres.GetConnectionString());
        // Le damos a la API la cadena de conexión del contenedor de pruebas. Es la misma clave
        //   que lee nuestro Program.cs con GetConnectionString("Default").
    }

    public async ValueTask InitializeAsync()
    // xUnit lo llama UNA vez antes de los tests de la clase.
    {
        await _postgres.StartAsync();
        // Enciende el contenedor y espera a que PostgreSQL esté listo.

        using var scope = Services.CreateScope();
        // 'Services' arranca la API y da acceso a su contenedor de dependencias. Creamos un
        //   "ámbito" (como una petición) para pedir el DbContext, que es 'scoped'.

        var db = scope.ServiceProvider.GetRequiredService<OakErpDbContext>();
        await db.Database.MigrateAsync();
        // Aplica nuestras migraciones al PostgreSQL vacío: así los tests prueban también que
        //   las migraciones funcionan desde cero.
    }

    public override async ValueTask DisposeAsync()
    // xUnit lo llama al terminar. Apagamos primero la API y después el contenedor.
    {
        await base.DisposeAsync();
        await _postgres.DisposeAsync();
    }
}