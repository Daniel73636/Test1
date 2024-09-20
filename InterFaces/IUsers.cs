using Microsoft.AspNetCore.Mvc.ModelBinding;
using test1.Models;
using test1.Validations;

namespace test1.InterFaces
{
    public interface IUsers
    {
        Task<Response<List<User>?>> GetAllUsersAsync(ModelStateDictionary modelState);
    }
}

