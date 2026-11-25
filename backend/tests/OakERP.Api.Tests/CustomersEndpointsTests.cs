using System.Net;
using System.Net.Http.Json;
using OakERP.Api.Contracts.Customers;
using Xunit;

namespace OakERP.Api.Tests;

public sealed class CustomersEndpointsTests(ApiFactory factory) : IClassFixture<ApiFactory>
// IClassFixture<ApiFactory>: xUnit crea UNA fábrica (y un contenedor) y se la pasa a la clase.
//   Todos los tests de esta clase comparten esa API y esa base de datos, lo que es mucho
//   más rápido que arrancar un contenedor por test.
{
    private readonly HttpClient _client = factory.CreateClient();
    // Un cliente HTTP que habla directamente con la API en memoria (sin red real).

    [Fact]
    // [Fact]: marca un método como test. xUnit lo descubre y lo ejecuta.
    public async Task Create_WithValidData_ReturnsCreatedAndTheCustomer()
    // Convención de nombre: qué probamos _ en qué circunstancia _ qué esperamos.
    {
        // Arrange (preparar): los datos de entrada.
        var request = new CreateCustomerRequest("Acme S.L.", "B12345678", "info@acme.test");

        // Act (actuar): la acción que probamos.
        var response = await _client.PostAsJsonAsync("/api/customers", request);
        // Envía un POST con el objeto serializado a JSON.

        // Assert (comprobar): lo que debe cumplirse.
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var customer = await response.Content.ReadFromJsonAsync<CustomerResponse>();
        // Deserializa el JSON de la respuesta a nuestro DTO.

        Assert.NotNull(customer);
        Assert.Equal("Acme S.L.", customer.Name);
        Assert.NotEqual(Guid.Empty, customer.Id);
        // Si cualquier Assert falla, el test falla y te dice qué se esperaba y qué llegó.
    }

    [Fact]
    public async Task Create_WithInvalidData_ReturnsBadRequest()
    {
        var request = new CreateCustomerRequest("", null, "esto-no-es-un-correo");

        var response = await _client.PostAsJsonAsync("/api/customers", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        // Comprueba que la validación de DTOs sigue funcionando. Si mañana alguien rompe el
        //   atributo [Required] sin querer, este test avisará.
    }

    [Fact]
    public async Task GetById_WithUnknownId_ReturnsNotFound()
    {
        var response = await _client.GetAsync($"/api/customers/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        // Un Guid aleatorio no existe en la base de datos, así que debe dar 404.
    }
}