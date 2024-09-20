using test1.Models;
using Microsoft.EntityFrameworkCore;
using test1.Data;
using test1.InterFaces;
using test1.Validations;
using System.Net.Sockets;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace test1.Services
{
    public class UsersServices : IUsers
    {
        private readonly BaseContext _context;

        public UsersServices(BaseContext context)
        {
            _context = context;
        }

        public async Task<Response<List<User>?>> GetAllUsersAsync(ModelStateDictionary modelState)
        {
            var response = new Response<List<User>?>();
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(15)); // Timeout de 15 segundos

            // Validar el estado del modelo
            response.ValidateModelState(modelState, "Model state is invalid.");

            if (!response.Success)
            {
                return response; // Retorna si el modelo no es válido
            }

            try
            {
                // Obtener la lista de usuarios con el CancellationToken
                var users = await _context.Users.ToListAsync(cts.Token);

                // Validar si existen usuarios
                response.ValidateDataExists(users, "No users found.");

                // Validar si la operación ha excedido el tiempo
                response.ValidateOperationTimeout(cts.Token, "A connection error occurred: The operation timed out.");

                // Asignar la lista de usuarios a la respuesta
                response.Data = users;
            }
            catch (OperationCanceledException)
            {
                response.SetError("A connection error occurred: The operation timed out.");
            }
            catch (HttpRequestException)
            {
                response.SetError("A network error occurred. Please check your internet connection.");
            }
            catch (SocketException)
            {
                response.SetError("Network-related error occurred. Ensure you are connected to the internet.");
            }
            catch (Exception ex)
            {
                response.SetError($"An unexpected error occurred: {ex.Message}");
            }

            return response;
        }
    }
}
