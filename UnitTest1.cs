using FluentAssertions;

namespace PruevasTest1
{
    public class IntegrationTests
    {
        private readonly HttpClient _client;

        public IntegrationTests()
        {
            // Inicializa el cliente HTTP con la URL base de tu API real
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5218")
            };
        }

        [Fact]
        public async Task GetAllUsers_ReturnsSuccessStatusCode()
        {
            // Envia una solicitud GET real a la API en ejecución
            var response = await _client.GetAsync("/api/Users/all");

            // Verifica que la respuesta sea exitosa (código de estado 200)
            response.EnsureSuccessStatusCode();

            // Opcional: Verifica el contenido de la respuesta si lo necesitas
            var responseData = await response.Content.ReadAsStringAsync();
            responseData.Should().NotBeNullOrEmpty();
        }
    }
}