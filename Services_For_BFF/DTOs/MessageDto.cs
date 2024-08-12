namespace Shop_BFF.DTOs
{
    public class MessageDto
    {

        public bool Success { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public object Record { get; set; }

        public MessageDto(bool success, string message, string errorMessage)
        {
            Success = success;
            Message = message;
            ErrorMessage = errorMessage;
        }
    }
}
