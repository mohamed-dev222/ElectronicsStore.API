namespace ElectronicsStore.API.Middlewares
{
    public class ErrorResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? ErrorCode { get; set; }
        public string? StackTrace { get; set; }
    }
}