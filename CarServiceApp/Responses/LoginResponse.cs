namespace BaseLibrary.Responses
{

    public record LoginResponse(bool Success, string Message, string Token) : GeneralResponse(Success, Message, Token);
}
