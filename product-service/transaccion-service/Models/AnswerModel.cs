namespace transaccion_service.Models
{
    public class AnswerModel
    {
        public string Message { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int Code { get; set; }
        public object? Data { get; set; }
    }
}
