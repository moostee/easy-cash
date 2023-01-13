using System.Text.Json.Serialization;

namespace EasyCash.Shared
{
    public class Result
    {
        [JsonPropertyName("statusCode")]
        public string StatusCode { get; set; }
        [JsonPropertyName("statusMessage")]
        public string StatusMessage { get; set; }
        [JsonPropertyName("successful")]
        public bool Successful => StatusCode == StatusCodes.SUCCESSFUL || StatusCode == "0";
    }

    public class Result<T>
    {
        #region properies
        [JsonPropertyName("statusCode")]
        public string StatusCode { get; set; }
        [JsonPropertyName("statusMessage")]
        public string StatusMessage { get; set; }

        [JsonPropertyName("successful")]
        public bool Successful => StatusCode == StatusCodes.SUCCESSFUL;

        [JsonPropertyName("responseObject")]
        public T ResponseObject { get; private set; } = default;
        #endregion

        public static Result<T> Success(T instance, string message = "Successful") => new()
        {
            StatusCode = StatusCodes.SUCCESSFUL,
            StatusMessage = message,
            ResponseObject = instance
        };
        public static Result<T> Failure(string error = "Failed") => new()
        {
            StatusCode = StatusCodes.INVALID_REQUEST,
            StatusMessage = error
        };

    }


    public class StatusCodes
    {
        public const string SUCCESSFUL = "00";
        public const string INVALID_REQUEST = "99";
    }
}
