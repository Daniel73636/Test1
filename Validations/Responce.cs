using System.Net.Sockets;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using test1.Models;

namespace test1.Validations
{
    public class Response<T>
    {
        public T? Data { get; set; }
        public string? ErrorMessage { get; private set; }
        public bool Success { get; private set; } = true;

        // Validates if an integer is less than or equal to zero
        public void ValidateNumberGreaterThanZero(int value, string errorMessage)
        {
            if (value <= 0)
            {
                SetError(errorMessage);
            }
        }

        // Validates if a string is null or empty
        public void ValidateStringNotNullOrEmpty(string value, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                SetError(errorMessage);
            }
        }

        // General method to handle setting an error
        public void SetError(string errorMessage)
        {
            ErrorMessage = errorMessage;
            Success = false;
        }

        // Generic validation to check for null
        public void ValidateNotNull<TValue>(TValue value, string errorMessage)
        {
            if (value == null)
            {
                SetError(errorMessage);
            }
        }

        // Generic validation to check if a value matches the expected null state
        public void ValidateNull<TValue>(TValue value, string errorMessage)
        {
            if (value != null)
            {
                SetError(errorMessage);
            }
        }

        // Validates if there was a network error (assuming this check is from a catch block)
        public void ValidateNetworkError(Exception ex, string errorMessage)
        {
            if (ex is HttpRequestException || ex is SocketException)
            {
                SetError(errorMessage);
            }
        }

        // Validates if the data exists (e.g., list of users isn't empty)
        public void ValidateDataExists(List<User>? data, string errorMessage)
        {
            if (data == null || !data.Any())
            {
                SetError(errorMessage);
            }
        }

        // Validates if pagination parameters are valid (e.g., pageNumber and pageSize)
        public void ValidatePagination(int pageNumber, int pageSize, string errorMessage)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                SetError(errorMessage);
            }
        }

        // Validates a filtered result (like filtering users by a role)
        public void ValidateFilterResult(IEnumerable<T>? data, string filter, string errorMessage)
        {
            if (data == null || !data.Any())
            {
                SetError($"{errorMessage}: {filter}");
            }
        }

        public void ValidateModelState(ModelStateDictionary modelState, string errorMessage)
        {
            if (!modelState.IsValid)
            {
                SetError(errorMessage);
            }
        }

        public void ValidateOperationTimeout(CancellationToken token, string errorMessage)
        {
            if (token.IsCancellationRequested)
            {
                SetError(errorMessage);
            }
        }
    }
}
